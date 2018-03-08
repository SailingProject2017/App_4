/**********************************************************************************************/
/*@file       ChildeColliderTrigger.cs
*********************************************************************************************
* @brief      子オブジェクトのあたり判定を親オブジェクトへと通知する
*********************************************************************************************
* @author     Yuta Takatsu
*********************************************************************************************
* Copyright © 2018 Yuta Takatsu All Rights Reserved.
**********************************************************************************************/
using UnityEngine;
using System.Collections;

public class ChildeColliderTrigger : BaseObject
{
    private MarkerColliderTrigger markerCollider; // 親のあたり判定
   
    void Start()
    {
        // プレイヤーを取得
        GameObject objColliderTriggerParent = GameObject.Find("Player");
        markerCollider = objColliderTriggerParent.GetComponent<MarkerColliderTrigger>();

    }

    /// <summary>
    /// @brief  あたり判定用メソッド
    /// </summary>
    /// <param name="collider"></param>
    void OnTriggerEnter(Collider collider)
    {
       
        //markerCollider.RelayOnTriggerEnter(collider);
    }

}