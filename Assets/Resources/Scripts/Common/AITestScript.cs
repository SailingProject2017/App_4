/*********************************************************************************************/
/*@file       AITestScript.cs
*********************************************************************************************
* @brief      AIの挙動を制御するクラス
*********************************************************************************************
* @note       継承不可
*********************************************************************************************
* @author     Ryo Sugiyama
*********************************************************************************************
* Copyright © 2018 Ryo Sugiyama All Rights Reserved.
**********************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class AITestScript : MarkerBase
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

    private readonly Vector3 rotateL = new Vector3(0f, -0.05f, 0f);        // @brief 左旋回用変数
    private readonly Vector3 rotateR = new Vector3(0f, 0.05f, 0f);         // @brief 右旋回用変数
	[SerializeField]
    private float AISpeed;    
	// @brief 現在のスピード
	[SerializeField]
	private float AITopSpeed;               // @brief 出せる最高速度
	private float sailRotate;               // @brief セールの角度
       
	private float myRadius;                 // @brief 船が向いてる角度
	private float turnRadius;               // @brief 次のマーカーまでのラジアン
	private float turnDegree;               // @brief 次のマーカーまでの度数

    private float markerDistance;           // @brief ブイから次のブイまでの距離
    
	private readonly float ableMoveDegree = 15f; // @brief 自身が進める角度

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

	private eAIMoveStatus AIMoveStatus;
	[SerializeField]
	private eAIStatus AIStatus;     // @beief 現在のAIの状態
	[SerializeField]
    private eAISequence AISequence; // @brief 現在のジグザグのシーケンス

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

		AIStatus = eAIStatus.NULL;
        AISequence = eAISequence.NULL;
		AIMoveStatus = eAIMoveStatus.NULL;
       
		GetMarkerVec2();

		NextPointDeg();
		SailRotate(getWindParam.ValueWind, me.transform.localEulerAngles.y);

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

    }

    /// <summary>
    /// @brief BaseObjectの実装
    /// </summary>
	public override void OnFixedUpdate()
	{
		base.OnFixedUpdate();



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
        myPos = new Vector2(me.transform.position.x, me.transform.position.z);
        
		for (int i = 0; i < AIMarkerList.Count; i++)
		{
			markerPos.Add(new Vector2(AIMarkerList[i].transform.position.x,
									  AIMarkerList[i].transform.position.z));
		}
    }

    #endregion

    /// <summary>
    /// @brief 次のブイの角度を求める
    /// </summary>
    /// <returns> 次のブイのラジアン値 </returns>
    private float NextPointDeg()
	{
		myRadius = me.transform.localEulerAngles.y;

        turnRadius = Mathf.Atan2(markerPos[currentHitMarker].y - me.transform.position.z,
								 markerPos[currentHitMarker].x - me.transform.position.x);
        
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
		else if (Mathf.DeltaAngle(myRadius, turnDegree) > 1)
		{
			return eAIMoveStatus.eLeft;
		}
		else
		{
			return eAIMoveStatus.NULL;
		}
	}

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

    #region Ship and Sail Moving

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
		float temp = 10;

		rotate -= 180;
        
        if (rotate >= windVector + ableMoveDegree)
		{
			sailRotate = 10 + ((rotate - ableMoveDegree) * 80 / (180 - ableMoveDegree));
			temp = Mathf.Abs(10 + ((rotate - ableMoveDegree) * (50 - 10) / 180));
		}
		if (rotate <= windVector - ableMoveDegree)
		{
			sailRotate = -10 + ((rotate + ableMoveDegree) * 80 / (180 - ableMoveDegree));
			temp = Mathf.Abs(-10 + ((rotate - ableMoveDegree) * (50 - 10) / 180));
		}

		mySail.transform.localEulerAngles = new Vector3(0, sailRotate, 0);

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
				if (AITopSpeed < 35)
				{
					if (SwitchTurning() == eAIMoveStatus.eLeft)
					{
						me.transform.Rotate(rotateL);
					}
					else
					{
						me.transform.Rotate(rotateR);
					}
				}

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
                       
				if (Mathf.DeltaAngle(myRadius, turnDegree) < 0)
                {
                    me.transform.Rotate(rotateL);
                }
                // 左
				else if (Mathf.DeltaAngle(myRadius, turnDegree) > 1)
                {
                    me.transform.Rotate(rotateR);
                }
                else
                {
					AISequence = eAISequence.eThird;
                }
      
				break;

			case eAISequence.eThird:

				break;

			case eAISequence.eLast:
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
					AISequence = eAISequence.eLast;
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