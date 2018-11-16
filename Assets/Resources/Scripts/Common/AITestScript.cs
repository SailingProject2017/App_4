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

	private List<Vector2> markerPos;
	private List<GameObject> AIMarkerList;

	private GameObject me;
	private GameObject mySail;
	private GameObject myHuman;
	private GetWindParam getWindParam;
    
	private float AISpeed;
	private float AITopSpeed;
	private float sailRotate;

	private float myRadius;
	private float turnRadius;
	private float turnDegree;

	private const float ableMoveDegree = 15f;


	private enum eAIStatus
	{
		eStright,
        eZIGUZAGU,
        eGoal,
        NULL

	}

	private eAIStatus AIStatus;

	/// <summary>
    /// Markers the initialize.
    /// </summary>
	protected override void MarkerInitialize()
	{
		base.MarkerInitialize();

		markerPos = new List<Vector2>();
		AIMarkerList = new List<GameObject>();
		AIMarkerList = GameObjectExtension.GetGameObject(markerObjName, DecAIStrength());


		me = this.gameObject;
		mySail = me.transform.Find("Sail").gameObject;
		myHuman = me.transform.Find("Human").gameObject;
		getWindParam = GameObjectExtension.Find("UIWind").GetComponent<GetWindParam>();

		myRadius = transform.localEulerAngles.y;

		currentMarker = 0;
		currentHitMarker = 1;

		GetMarkerVec2();

		SailRotate(getWindParam.ValueWind, me.transform.localEulerAngles.y);

        if(AISpeed < 20)
		{
			AIStatus = eAIStatus.eZIGUZAGU;
		}
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

    /// <summary>
    /// Decide the AI Strength.
    /// </summary>
    /// <returns>The AIS trength.</returns>
	private string DecAIStrength()
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
                break;
        }

		return temp;
	}

    /// <summary>
    /// Ships the move.
    /// </summary>
    private void ShipMove()
	{
		if(AISpeed < AITopSpeed)
		{
			AISpeed += 3;

            // TODO
			// AISPeed += getWindParam.WindForce が本当は使いたい
            // 実装されたら変えてくれ
		}
        if(AISpeed > AITopSpeed)
		{
			AISpeed -= 3;
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

    private void ShipRotate()
	{
		
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
            }
            else
            {
                currentHitMarker += 1;
            }
        }

		if (other.gameObject == AIMarkerList[currentMarker].gameObject && other.tag != "goal")
        {
            // 現在通ったマーカーの総数を計算
            currentMarker++;
        }
    }
}

