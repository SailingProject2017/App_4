﻿/***********************************************************************/
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

public class ShipSensor : BaseObject
{

    private Vector3 acceleration; // @brief センサー情報を取得

    private GUIStyle labelStyle; // @brief フォント

    void Start()
    {
        //フォント生成
        this.labelStyle = new GUIStyle();
        this.labelStyle.fontSize = Screen.height / 22;
        this.labelStyle.normal.textColor = Color.white;
    }
    public override void OnUpdate()
    {
        if (BaseObjectSingleton<GameInstance>.Instance.IsGyro)
        {
            if (Singleton<GameInstance>.instance.IsShipMove)
            {
                base.OnUpdate();
                this.acceleration = Input.acceleration * 2;

                transform.Rotate(0, this.acceleration.x, 0);
            }
        }
    }
}
