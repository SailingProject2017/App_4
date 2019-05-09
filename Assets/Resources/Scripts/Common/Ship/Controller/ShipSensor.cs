/***********************************************************************/
/*! @file   ShipSensor.cs
*************************************************************************
*   @brief  傾きセンサーで船を操作するコントローラ
*************************************************************************
*   @author yuta takatsu
*************************************************************************
*   Copyright © 2017 yuta takatsu All Rights Reserved.
************************************************************************/
using UnityEngine;
using System.Collections;

public class ShipSensor : PlayerBase
{

    private Vector3 acceleration; // @brief センサー情報を取得

    private GUIStyle labelStyle; // @brief フォント
    
    public override void Initialize()
    {
		base.Initialize();
        //フォント生成
        this.labelStyle = new GUIStyle();
        this.labelStyle.fontSize = Screen.height / 22;
        this.labelStyle.normal.textColor = Color.white;
    }
	public override void UpdateByFrame()
    {
        if (Singleton<SaveDataInstance>.Instance.IsGyro)
        {
            if (Singleton<GameInstance>.Instance.IsShipMove)
            {
                this.acceleration = Input.acceleration * Singleton<SaveDataInstance>.Instance.Sensitivty * 2.5f;

                gameObject.transform.Rotate(0, this.acceleration.x, 0);
            }
        }
    }
	public override void UpdateByFixed()
	{
		
	}
}
