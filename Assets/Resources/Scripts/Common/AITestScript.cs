/*********************************************************************************************/
/*@file       AIControler.cs
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

public sealed class AITestScript : MarkerBase {


#region 変数宣言

	private List<Vector2> markerPos;
	private List<GameObject> AIMarkerList;

	private GameObject me;
	private GameObject mySail;
	private GameObject myHuman;
	private GetWindParam getWindParam;
    
	private readonly Vector3 rotateL = new Vector3(0f, -1.5f, 0f);        // @brief 左旋回用変数
    private readonly Vector3 rotateR = new Vector3(0f, 1.5f, 0f);         // @brief 右旋回用変数

	private float AISpeed;
	private float AITopSpeed;
	private float sailRotate;

	private float myRadius;
	private float turnRadius;
	private float turnDegree;

	private const float ableMoveDegree = 15f;


	private enum eAIStatus
	{
        eZIGUZAGU,
        eTurn,
        eGoal,
        NULL

	}

	private eAIStatus AIStatus;

#endregion

	/// <summary>
	/// Markers the initialize.
	/// </summary>
	protected override void MarkerInitialize()
	{
		base.MarkerInitialize();

		markerPos = new List<Vector2>();
		AIMarkerList = new List<GameObject>();
		AIMarkerList = GameObjectExtension.GetGameObject(markerObjName, DecideAIStrength());


		me = this.gameObject;
		mySail = me.transform.Find("Sail").gameObject;
		myHuman = me.transform.Find("Human").gameObject;
		getWindParam = GameObjectExtension.Find("UIWind").GetComponent<GetWindParam>();

		myRadius = transform.localEulerAngles.y;

		currentMarker = 0;
		currentHitMarker = 1;

		AIStatus = eAIStatus.NULL;

		GetMarkerVec2();

		GetNextPoint();

		SailRotate(getWindParam.ValueWind, me.transform.localEulerAngles.y);
        
	}

    /// <summary>
    /// Ons the update.
    /// </summary>
	public override void OnUpdate ()
	{
        base.OnUpdate();

        // セールをまげつつ速度の計算
		SailRotate(getWindParam.ValueWind, me.transform.localEulerAngles.y);
	
	}
    
	public override void OnFixedUpdate()
	{
		base.OnFixedUpdate();

		ShipMove();
        
        switch (AIStatus)
		{
			case eAIStatus.eZIGUZAGU:
				
				break;

			case eAIStatus.eTurn:
				
				ShipRotate(GetNextPoint());

				break;

			case eAIStatus.eGoal:

				ShipRotate(GetNextPoint());
				
				break;

			case eAIStatus.NULL:
				
				break;
		}
	}

	/// <summary>
    /// Decide the AI Strength.
    /// </summary>
    /// <returns>The AIS trength.</returns>
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
	/// Gets the marker vec2.
	/// </summary>
	private void GetMarkerVec2()
	{
		for (int i = 0; i < AIMarkerList.Count; i++)
		{
			markerPos.Add(new Vector2(AIMarkerList[i].transform.position.x,
									  AIMarkerList[i].transform.position.z));
		}     
	}

    private float GetNextPoint()
	{
		myRadius = transform.localEulerAngles.y;
        
		turnRadius = Mathf.Atan2(markerPos[currentHitMarker].y - transform.position.y,
								 markerPos[currentHitMarker].x - transform.position.x);

		turnDegree = turnRadius * Mathf.Rad2Deg + 87;

		return turnDegree;
	}

   
    /// <summary>
    /// Ships the move.
    /// </summary>
    private void ShipMove()
	{
		if(AISpeed < AITopSpeed)
		{
			AISpeed += 3 * Time.deltaTime;

            // TODO
			// AISPeed += getWindParam.WindForce が本当は使いたい
            // 実装されたら変えてくれ
		}

        if(AISpeed > AITopSpeed)
		{
			AISpeed -= 3 * Time.deltaTime;
		}

        if(!Singleton<GameInstance>.Instance.IsShipMove)
		{
			AISpeed = 20;
		}

		// 進む
		transform.position = transform.forward * -AISpeed * Time.deltaTime;
	}

    /// <summary>
    /// Sails the rotate.
    /// </summary>
    /// <returns>The rotate.</returns>
    /// <param name="windVector">Wind vector.</param>
    /// <param name="rotate">Rotate.</param>
    private void SailRotate(float windVector, float rotate)
	{
		float temp = 10;

		rotate -= 180;
        
		if(rotate >= windVector + ableMoveDegree)
		{
			sailRotate = 10 + ((rotate - ableMoveDegree) * 80 / (180 - ableMoveDegree));
			temp = Mathf.Abs(10 + ((rotate - ableMoveDegree) * (60 - 10) / 180));
		}
		if(rotate <= windVector - ableMoveDegree)
		{
			sailRotate = 10 + ((rotate + ableMoveDegree) * 80 / (180 - ableMoveDegree));
			temp = Mathf.Abs(10 + ((rotate - ableMoveDegree) * (60 - 10) / 180));
		}

		mySail.transform.localEulerAngles = new Vector3(0, sailRotate, 0);

		AITopSpeed = temp;
	}

    /// <summary>
    /// Ships the rotate.
    /// </summary>
    private void ShipRotate(float turnDeg)
	{
		// 左
		if(Mathf.DeltaAngle(myRadius, turnDeg) < 0)
		{
			transform.Rotate(rotateL);
		}

        // 右
		else if(Mathf.DeltaAngle(myRadius, turnDeg) > 1)
		{
			transform.Rotate(rotateR);
		}
	}

	/// <summary>
    /// Ons the trigger enter.
    /// </summary>
    /// <param name="other"> アタッチされているオブジェクト以外 </param>
    private void OnTriggerEnter(Collider other)
    {
        // 当たったゲームオブジェクトが、目的のマーカーの場所と一致した場合
        if (other.gameObject == AIMarkerList[currentHitMarker].gameObject)
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
                currentHitMarker += 1;
				AIStatus = eAIStatus.eTurn;
            }
        }

		if (other.gameObject == AIMarkerList[currentMarker].gameObject && other.tag != "goal")
        {
            // 現在通ったブイの総数を計算
            currentMarker++;
        }
    }
}

