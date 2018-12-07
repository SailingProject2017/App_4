/*********************************************************************************************/
/*@file       AIScript.cs
*********************************************************************************************
* @brief      AIの挙動を制御するクラス
*********************************************************************************************
* @note       歴代屈指のクソース。　改善するならステートごとにクラス作って管理した方がいい
*********************************************************************************************
* @author     Ryo Sugiyama
*********************************************************************************************
* Copyright © 2018 Ryo Sugiyama All Rights Reserved.
**********************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class AIScript : MarkerBase
{

    #region 変数宣言
        
    private Vector2 myPos;                  // @brief 自身の場所
    private Vector3 myZIGUZAGUPos;          // @brief ジグザグ開始時の自身の場所
    
    private List<Vector2> markerPos;        // @brief マーカーの場所
	private List<GameObject> AIMarkerList;  // @brief AIが通るマーカーのコンポーネント

	private GameObject me;                  // @brief 自分のコンポーネント
	private GameObject mySail;              // @brief 自分のセールコンポーネント
    private GameObject myHuman;             // @brief 自分のヒトコンポーネント
	private GetWindParam getWindParam;      // @brief 風のベクトル

	private Vector3 rotateL;                // @brief 左旋回用変数    
	private Vector3 rotateR;                // @brief 右旋回用変数

    private float AISpeed;              	// @brief 現在のスピード
	private float AITopSpeed;               // @brief 出せる最高速度
	private float sailRotate;               // @brief セールの角度
	private float turnSpeed;                // @brief 旋回速度
       
	private float myRadius;                 // @brief 船が向いてる角度
	private float turnRadius;               // @brief 次のマーカーまでのラジアン
	private float turnDegree;               // @brief 次のマーカーまでの度数

    private float markerDistance;           // @brief ブイから次のブイまでの距離
    
	private readonly float ableMoveDegree = 10f; // @brief 自身が進める角度

    /// <summary>
    /// @beief AIがどの状態で進んでいるか
    /// </summary>
	private enum eAIStatus
	{
        eZIGUZAGU,
        eTurn,
        eGoal,
        NULL

	}
    
    /// <summary>
    /// @beief 曲がる時の状態
    /// </summary>
    private enum eAIMoveStatus
    {
        eRight,
        eLeft,
		eTurnEnd,
        NULL
    }
    
    /// <summary>
    /// @beief ジグザグに進む時のシーケンス
    /// </summary>
    private enum eAISequence
    {
        eSetUp,
        eFirst,
        eSecond,
        eThird,
        eLast,
        NULL
    }

	private eAIMoveStatus AIMoveStatus; // @brief AIの動作状態
	private eAIMoveStatus tempStatus;   // @brief テンポラリステータス　
	private eAIStatus AIStatus;         // @beief 現在のAIの全体の状態
    private eAISequence AISequence;     // @brief 現在のジグザグのシーケンス

    #endregion
    
    #region 初期化
       
    /// <summary>
	/// @brief MarkerBaseの実装
	/// </summary>
    protected override void MarkerInitialize()
	{
		base.MarkerInitialize();

        // リストの初期化
		markerPos = new List<Vector2>();
		AIMarkerList = new List<GameObject>();
		AIMarkerList = GameObjectExtension.GetGameObject(markerObjName, DecideAIStrength());

        // コンポーネントの取得
		me = this.gameObject;
		mySail = me.transform.Find("Sail").gameObject;
		getWindParam = GameObjectExtension.Find("UIWind").GetComponent<GetWindParam>();
        
        // 変数の初期化
		myRadius = transform.localEulerAngles.y;
		currentMarker = 0;
		currentHitMarker = 1;

        // ステートの初期化
		AIStatus = eAIStatus.NULL;
        AISequence = eAISequence.NULL;
		AIMoveStatus = eAIMoveStatus.NULL;
        
        // 角度の取得
		GetMarkerVec2();

        // 取得した角度の初期化
		NextPointDeg();

        // セールの角度の初期化
		SailRotate(getWindParam.ValueWind, me.transform.localEulerAngles.y);

		// 旋回速度の初期化
		ChangeShipTurnSpeed();

    }
    
    #endregion

    #region 更新関数
    
    /// <summary>
    /// @brief BaseObjectの実装
    /// </summary>
    public override void OnUpdate()
	{
        base.OnUpdate();

        // セールをまげつつ速度の計算
        SailRotate(getWindParam.ValueWind, me.transform.localEulerAngles.y);

        // 旋回速度の更新
		ChangeShipTurnSpeed();
    }

    /// <summary>
    /// @brief BaseObjectの実装
    /// </summary>
	public override void OnFixedUpdate()
	{
		base.OnFixedUpdate();

        // ステート管理
		switch (AIStatus)
		{
			case eAIStatus.eZIGUZAGU:
				MoveTypeZIGUZAGU(NextPointDeg());
				break;

			case eAIStatus.eTurn:
				ShipRotate(NextPointDeg());
				break;

			case eAIStatus.eGoal:

                transform.Rotate(rotateR);        
				break;

			case eAIStatus.NULL:
                break;
		}
        // 船の移動処理
        ShipMove();
	}

    #endregion


    #region 関数の実装

    /// <summary>
    /// @brief AIの強さの決定
    /// </summary>
    /// <return> 通るオブジェクトの名前 </returns>
    private string DecideAIStrength()
    {
        string temp;

        switch ((int)Random.Range(0.0f, 3.0f))
        {
            case 0:
                temp = "AIFast";
                break;

            case 1:
                temp = "AINormal";
                break;

            case 2:
                temp = "AILate";
                break;

            default:
                temp = "GameObject";
				Debug.LogWarning("<color=red>StageがAIに対応してません</color>");
				break;
        }

        return temp;
    }

    
    /// <summary>
	/// @brief 使うコンポーネントの座標をVector2型にしてリスト化
	/// </summary>
    private void GetMarkerVec2()
	{
		// 取得
        myPos = new Vector2(me.transform.position.x, me.transform.position.z);
        
		for (int i = 0; i < AIMarkerList.Count; i++)
		{
			markerPos.Add(new Vector2(AIMarkerList[i].transform.position.x,
									  AIMarkerList[i].transform.position.z));
		}
    }



    /// <summary>
    /// @brief 次のブイの角度を求める
    /// </summary>
    /// <returns> 次のブイのラジアン値 </returns>
    private float NextPointDeg()
	{
		myRadius = me.transform.localEulerAngles.y;

        turnRadius = Mathf.Atan2(markerPos[currentHitMarker].y - me.transform.position.z,
								 markerPos[currentHitMarker].x - me.transform.position.x);
        
        // ラジアンから度数へ変換
		turnDegree = (turnRadius * Mathf.Rad2Deg + 87) * -1;

		return turnDegree;
	}
    
    /// <summary>
    /// @brief 次のブイがどっち回りかを求める
    /// </summary>
    /// <returns> 曲がる方向 </returns>
    /// <param name="turnDegree"> 次のブイの角度 </param>
    private eAIMoveStatus NextTurnDirection(float turnDegree)
    {
		// 左
		if (Mathf.DeltaAngle(myRadius, turnDegree) < 0)
		{
			return eAIMoveStatus.eRight;
		}
        // 右
		else if (Mathf.DeltaAngle(myRadius, turnDegree) > 1)
		{
			return eAIMoveStatus.eLeft;
		}
        // それ以外
		else
		{
			return eAIMoveStatus.NULL;
		}
	}

    /// <summary>
    /// @brief ジグザグの時、どっちに曲がったほうが早いのか判断する関数
    /// </summary>
    /// <returns> 進む方向 </returns>
    private eAIMoveStatus SwitchTurning()
	{

		float windVector = getWindParam.ValueWind;


		float rightRotate = Mathf.Abs(10 + (((mySail.transform.localEulerAngles.y + 15) - ableMoveDegree) * (50 - 10) / 180));
		float leftRotate = Mathf.Abs(-10 + (((mySail.transform.localEulerAngles.y - 15) - ableMoveDegree) * (50 - 10) / 180));

		if (leftRotate > rightRotate)
		{
			return eAIMoveStatus.eLeft;
		}
		if (leftRotate < rightRotate)
		{
			return eAIMoveStatus.eRight;
		}

		return eAIMoveStatus.NULL;
	}

	#endregion
    
    #region Ship and Sail Moving

    /// <summary>
    /// @brief 船の速度に応じて旋回速度を変える
    /// </summary>
    private void ChangeShipTurnSpeed()
	{
		turnSpeed = (0.05f / 35) * (AISpeed - 15) + 0.05f;
		rotateL = new Vector3(0, -turnSpeed, 0);
		rotateR = new Vector3(0, turnSpeed, 0);
	}

    /// <summary>
	/// @brief 船の直進処理
	/// </summary>
    private void ShipMove()
	{
		if (Singleton<ShipStates>.Instance.ShipState == eShipState.STOP)
			me.transform.position += me.transform.forward * -AISpeed * Time.deltaTime;
		
		if (AISpeed < AITopSpeed)
		{
			AISpeed += 3 * Time.deltaTime;

            // TODO
            // AISPeed += getWindParam.WindForce が本当は使いたい
            // 実装されたら変えてくれ
        }
        if (AISpeed > AITopSpeed)
		{
			AISpeed -= 3 * Time.deltaTime;
		}

        // カウントダウン中だったらスピード固定
        if (!Singleton<GameInstance>.Instance.IsShipMove)
		{
			AISpeed = 20;
			return;
		}
        
		// 進む
		me.transform.position += me.transform.forward * -AISpeed * Time.deltaTime;

		// 速度が遅かったらジグザグに走らせる
		if (AITopSpeed < 31 && AIStatus != eAIStatus.eTurn && AISequence == eAISequence.NULL)
		{
			if (currentHitMarker > 1)
				AIStatus = eAIStatus.eZIGUZAGU;
		}
	}

    /// <summary>
    /// @brief 現在の船の角度と風のベクトルからセールの角度を求める
    /// </summary>
    /// <param name="windVector"> 風のベクトル </param>
    /// <param name="rotate"> 自身の角度 </param>
    private void SailRotate(float windVector, float rotate)
	{
		// 最低速度
		float temp = 15;
        
        // -180 ~ 180 の範囲にする
		rotate -= 180;

        // 船の角度が正の数の時
        if (rotate >= windVector + ableMoveDegree)
		{
			sailRotate = 10 + ((rotate - ableMoveDegree) * 80 / (180 - ableMoveDegree));
			mySail.transform.localEulerAngles = new Vector3(0, sailRotate, 0);

            // 絶対値
			temp = Mathf.Abs(10 + ((rotate - ableMoveDegree) * (50 - 10) / 180));

            // 多分こっちの方がいいけどこのままで行くわ
			//temp = Mathf.Abs(15 + ((mySail.transform.localEulerAngles.y - 180 - ableMoveDegree) * 35 / 80));
		}
		// 船の角度が負の数の時
		if (rotate <= windVector - ableMoveDegree)
		{
			sailRotate = -10 + ((rotate + ableMoveDegree) * 80 / (180 - ableMoveDegree));
			mySail.transform.localEulerAngles = new Vector3(0, sailRotate, 0);

			// 絶対値
			temp = Mathf.Abs(-10 + ((rotate - ableMoveDegree) * (50 - 10) / 180));

			// 多分こっちの方がいいけどこのままで行くわ
			//temp = Mathf.Abs(-15 + ((mySail.transform.localEulerAngles.y - 180 - ableMoveDegree) * 35 / 80));
		}

		mySail.transform.localEulerAngles = new Vector3(0, sailRotate, 0);

        // 出せる最大の速度に代入
		AITopSpeed = temp;
	}

    /// <summary>
    /// @brief 船の回転
    /// </summary>
	private bool ShipRotate(float turnDeg)
	{
		AIMoveStatus = eAIMoveStatus.NULL;
		// 左
        if (Mathf.DeltaAngle(myRadius, turnDeg) < 0)
        {
			me.transform.Rotate(rotateL);
			AIMoveStatus = eAIMoveStatus.eLeft;
			return false;
        }
        // 左
        else if (Mathf.DeltaAngle(myRadius, turnDeg) > 1)
        {
			me.transform.Rotate(rotateR);
			AIMoveStatus = eAIMoveStatus.eRight;
			return false;
        }
        // まっすぐ
		else
		{
			AIStatus = eAIStatus.NULL;
			AIMoveStatus = eAIMoveStatus.eTurnEnd;
			return true;
		}
	}
    
	/// <summary>
	/// @brief ジグザグに動く処理
	/// </summary>
	/// <param name="turnDegree"> 次のブイの角度 </param>
	private void MoveTypeZIGUZAGU(float turnDegree)
	{
		switch (AISequence)
		{
			case eAISequence.NULL:
				// ジグザグの準備
				AISequence = eAISequence.eSetUp;
				markerDistance = Vector3.Distance(me.transform.position, markerPos[currentHitMarker]);
				myZIGUZAGUPos = me.transform.position;
				break;

			case eAISequence.eSetUp:

				// 最高速度が上がるまで船に角度をつける
				if (Mathf.Abs(me.transform.localEulerAngles.y - 180) < 30)
				{
					// 一度曲がる方向が決まったら入らないようにする。
					if (tempStatus == eAIMoveStatus.NULL)
					{
						if (SwitchTurning() == eAIMoveStatus.eLeft)
						{
							tempStatus = eAIMoveStatus.eLeft;

						}
						else if (SwitchTurning() == eAIMoveStatus.eRight)
						{
							tempStatus = eAIMoveStatus.eRight;

						}
						else
						{
							tempStatus = eAIMoveStatus.NULL;
						}
					}

                    // 実際に曲げる
					if(tempStatus == eAIMoveStatus.eLeft)
					{
						me.transform.Rotate(rotateL);
					}
					else if(tempStatus == eAIMoveStatus.eRight)
					{
						me.transform.Rotate(rotateR);
					}
				}
                // 次のシーケンスへ
				else
				{
					AISequence = eAISequence.eFirst;
				}

				break;

			case eAISequence.eFirst:

				// マーカーまで半分走ったら次のシークエンス
				if (Vector3.Distance(me.transform.position, myZIGUZAGUPos) > markerDistance * 0.5)
				{
					AISequence = eAISequence.eSecond;
					AIMoveStatus = eAIMoveStatus.NULL;
				}
                
				break;

			case eAISequence.eSecond:
                       
                // 左
				if (Mathf.DeltaAngle(myRadius, turnDegree) < 0)
                {
                    me.transform.Rotate(rotateL);
                }
                // 右
				else if (Mathf.DeltaAngle(myRadius, turnDegree) > 1)
                {
                    me.transform.Rotate(rotateR);
                }
                // ブイに来たらそのまままっすぐ
                else
                {
					AISequence = eAISequence.eThird;
                }
      
				break;

			case eAISequence.eThird:
                // 次のカーブまでこのままのシーケンスを保つ
                // カーブが来たら次のシーケンスへ
				break;

			case eAISequence.eLast:
				// 状態を初期化
				tempStatus = eAIMoveStatus.NULL;
				AISequence = eAISequence.NULL;
				break;
		}
	}

    
    #endregion

    /// <summary>
    /// @brief 次のブイまでの距離を求める
    /// </summary>
    /// <returns> 距離 </returns>
    /// <param name="meP"> 自身の場所 </param>
    /// <param name="youP"> 次のブイの場所 </param>
    private float Distance(Vector2 meP, Vector2 youP)
    {
        return (meP.x - youP.x) * (meP.x - youP.x) + (meP.y - youP.y) * (meP.y - youP.y);
    }

    /// <summary>
    /// @brief 当たった時に実行される処理
    /// </summary>
    /// <param name="other"> アタッチされているオブジェクト以外 </param>
    private void OnTriggerEnter(Collider other)
    {
        // 当たったゲームオブジェクトが、目的のマーカーの場所と一致した場合
        if (other.gameObject == hitMarkerList[currentHitMarker].gameObject)
        {
            // スタートとゴールが同じ場所にあった時にゴール判定にならないようにする処理
            if (other.tag == "goal")
            {
                isGoal = true;
                currentHitMarker = 1;
			    AIStatus = eAIStatus.eGoal;
            }
            else
            {
                // 現在通ったマーカーの総数を計算
                currentHitMarker += 1;
			    AIStatus = eAIStatus.eTurn;
				if (currentHitMarker % 2 == 1 && AISequence != eAISequence.NULL)
					AISequence = eAISequence.NULL;
            }
        }

        // 当たったゲームオブジェクトが、目的のブイの場所と一致した場合
		if (other.gameObject == hitMarkerList[currentMarker].gameObject && other.tag != "goal")
        {
            // 現在通ったブイの総数を計算
            currentMarker++;
        }
    }    
}