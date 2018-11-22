/*******************************************************************************************/
/*@file       ChangeWindArrow.cs
*********************************************************************************************
* @brief      マーカーを通ったら風向きを変更
*********************************************************************************************
* @author     Reina Sawai and Tsuyoshi Takaguchi
*********************************************************************************************
* Copyright © 2018 Reina Sawai All Rights Reserved.
********************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWindArrow : MarkerBase
{
    // マーカーを格納するリストについての宣言
    private List<Vector3> tmpPos = new List<Vector3>(); // @brief Markerオブジェクトの座標を格納するリスト
    private int previousMarkerNo;                       // @brief Marker(GameObject)の数値を入れる変数
    MarkerColliderTrigger markerJudg;                   // @brief マーカーの数を取得する変数
    GetWindParam windDirection;                         // @brief 風向きの数値入力する変数

    /// <summary>
    /// @brief 初期化
    /// </summary>
    protected override void MarkerInitialize()
    {
        base.MarkerInitialize();

        // UIWind(風向き用の矢印)の向きを変更させる
        windDirection = GameObject.Find("UIWind").GetComponent<GetWindParam>();

        // プレイヤーがマーカーを通った判定をする
        markerJudg = GameObject.Find("Player").GetComponent<MarkerColliderTrigger>();

        // マーカーオブジェクトの個数を取得する
        for (int i = 0; i < markerList.Count; i++)
        {
           // Debug.Log("名前：" + markerList[i].name);
            tmpPos.Add(markerList[i].gameObject.transform.position);
          //  Debug.Log("座標:" + tmpPos[i]);
        }


        currentMarker = 0;    // マーカーの個数の初期化
        currentHitMarker = 1; // スタートがあるときは2で初期化

    }
    

    public override void OnUpdate()
    {
        Debug.Log("個数:" + markerJudg.CurrentMarker);
        PreviousMarker();
    }

    /// <summary>
    /// @brief 通ったブイの数と現在のブイの数を比較する
    /// </summary>
    private void PreviousMarker()
    {
        // 奇数か判定
        if (markerJudg.CurrentMarker % 2 == 1)
        {
            // マーカーのラインの数によって風向きの角度を変える
            if (previousMarkerNo != markerJudg.CurrentMarker)
            {
                previousMarkerNo = markerJudg.CurrentMarker;
                if (previousMarkerNo == 3)
                {
                   /* 数値と矢印の関係性(スタート地点から見た矢印の向き)
                      -180°     -90°      0°       90°     180°
                       ↓　　　　←　　　  ↑　　　　→　　　　↓ */
                    windDirection.ValueWind = -90;
                }
                if (previousMarkerNo == 5)
                {
                    windDirection.ValueWind = 180;
                }
                if (previousMarkerNo == 7)
                {
                    windDirection.ValueWind = 90;
                }

            }
        }
        // 偶数の場合
        else
        {
            previousMarkerNo = markerJudg.CurrentMarker;
        }
    }
}