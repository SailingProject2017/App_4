/**********************************************************************************************/
/*@file       MarkerBase.cs
*********************************************************************************************
* @brief      Marker系の基底クラス
*********************************************************************************************
* @author     Ryo Sugiyama
*********************************************************************************************
* Copyright © 2018 Ryo Sugiyama All Rights Reserved.
**********************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerBase : BaseObject {
   
	protected GameObject markerObjName;     // @brief markerのオブジェクトの名前を取得
	protected int currentListNum;           // @brief 現在の配列番号
    protected List<GameObject> hitMarkerList = new List<GameObject>();  // @brief markerのリスト



	public virtual void Start()
	{
		MarkerInitialize();
    }


    /// <summary>
    /// @brief 継承先で個別に実装する初期化
    /// </summary>
	protected virtual void MarkerInitialize() 
	{
		// 初期化
        currentListNum = 0;

        // 親オブジェクトを取得し
        markerObjName = GameObject.Find("MarkerObj");

        // 取得した親オブジェクトの子も取得する
        hitMarkerList = GameObjectExtension.GetGameObject(markerObjName);

	}

}
