/**********************************************************************************************/
/*@file       GetWindParam.cs
*********************************************************************************************
* @brief      風のベクトルを制御するクラス
*********************************************************************************************
* @author     Reina Sawai and Ryo Sugiyama
*********************************************************************************************
* Copyright © 2017 Reina Sawai All Rights Reserved.
**********************************************************************************************/
using System;
using UnityEngine;

public class GetWindParam : BaseObject
{
    
    // 0~360まで
    [Range(0, 360)]
    private float valueWind = 0;   //@brief 風の方向

    /// <summary>
    /// @brief 例外処理
    /// </summary>
    private float SetWindValue(object wind)
    {
		return UnityEngine.Random.Range(-180, 180);
    }

    /// <summary>
    /// @brief 風向きを出すアクセサー
    /// </summary>
    public float ValueWind
    {
        get { return valueWind; }
        set
        {
            // 風向きを0~360の中に指定する
            if(value > 360)
            {
                valueWind = value - 360;
            }
            else if (value < 0)
            {
                valueWind = 360 - value;
            }
            else
            {
                valueWind = value;
            }
        }
    }
}
