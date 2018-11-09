/*********************************************************************************************
* @brief      yajorusiを動かす仮プログラム
*********************************************************************************************
* @author     Reina Sawai
*********************************************************************************************
* Copyright © 2018 Reina Sawai All Rights Reserved.
**********************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YajirusiRotate : BaseObject
{
    [SerializeField]
    private GameObject shipObj; // @brief 自分の船の情報

    [SerializeField]
    private GetWindParam windParam; // @brief 風の情報

    public override void OnUpdate()
    {
        base.OnUpdate();

        Debug.Log("回転");
        // valueWindの値分回転
        transform.eulerAngles = new Vector3(0, windParam.ValueWind - shipObj.transform.eulerAngles.y, 0);
    }
}
   
