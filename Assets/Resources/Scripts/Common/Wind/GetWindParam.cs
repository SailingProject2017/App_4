/**********************************************************************************************/
/*@file       GetWindParam.cs
*********************************************************************************************
* @brief      風のベクトルを制御するクラス
*********************************************************************************************
* @author     Reina Sawai
*********************************************************************************************
* Copyright © 2017 Reina Sawai All Rights Reserved.
**********************************************************************************************/
using System;
using UnityEngine;

public class GetWindParam : BaseObject
{
    
    private int wind;  //@brief 風の乱数入れる箱

    // -180~180まで
    [Range(-180, 180)]
    private float valuewind = 0;　   //@brief 風の方向

    /// <summary>
    /// @brief 例外処理
    /// </summary>
    private int random(object wind)
    {
        throw new NotImplementedException();
    }

    void Start()
    {
        Random();
    }

    /// <summary>
    /// @brief 風ランダムで一つ表示させるための関数
    /// </summary>
    public void Random()
    {
        //-180~180までの値をランダムで出す
        wind = UnityEngine.Random.Range(-180, 180);
        Debug.Log(wind);
        
        // 値を制限
        
        //180度以上にしない
        if (wind >= 180)
        {
            valuewind = wind - 180;
        }
        //-180度以下にしない
        else if (wind < -180)
        {
            valuewind = 180 + wind;
        }
        //そのまま
        else
            valuewind = wind;
    }

   /// <summary>
   /// @brief 変数アクセサ
   /// </summary>
    public float Valuewind
    {
        get { return valuewind; }
        
    }
}
