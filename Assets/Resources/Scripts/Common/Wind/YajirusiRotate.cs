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

    float minAngle = -10.0f;
    float maxAngle = 180.0f;
	public override void OnUpdate()
    {
        base.OnUpdate();
        float angle = Mathf.LerpAngle(minAngle, maxAngle, Time.time);
        //y軸に回転
        transform.eulerAngles = new Vector3(0, angle, 0);
    }
	
	
}
