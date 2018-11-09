/**********************************************************************************************/
/*@file       ArrowRotate.cs
*********************************************************************************************
* @brief      風向きを表すUIを回転させるスクリプト
*********************************************************************************************
* @author     Yuta Takatsu & Reina Sawai
*********************************************************************************************
* Copyright © 2018 Yuta Takatsu & Reina Sawai All Rights Reserved.
**********************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRotate : BaseObject
{
    [SerializeField]
    private GameObject shipObj; // @brief 自分の船の情報

    [SerializeField]
    private GetWindParam windParam; // @brief 風の情報

    public override void OnUpdate()
    {
        base.OnUpdate();

        // valueWindの値分回転
        transform.eulerAngles = new Vector3(0, windParam.ValueWind - shipObj.transform.eulerAngles.y, 0);
    }
}
   
