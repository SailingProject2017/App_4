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
    private float timeWind; //@brief 風向きを変えるタイマー
    

    float minAngle = 0.0f;
    float maxAngle = 180.0f;

   
    public override void OnUpdate()
      {
          base.OnUpdate();
          //-180から180に数字を制限
          maxAngle += UnityEngine.Random.Range(-180, 180);

          timeWind -= Time.deltaTime;



        if (timeWind <= 0.0)
         {
             //5秒ごとに処理を行う
            timeWind = 5.0f;
            MoveStart();
           
          }
      }

    /// <summary>
    /// 矢印を動かす関数
    /// </summary>
    void MoveStart()
    {
            float angle = Mathf.LerpAngle(minAngle, maxAngle, Time.time);
            //y軸に回転
            transform.eulerAngles = new Vector3(0, angle, 0);
        }

    //Playerの傾きを取得して矢印もそれにそって角度を変える
}
