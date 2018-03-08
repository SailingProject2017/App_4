/**********************************************************************************************/
/*@file       MarkerJuge.cs
*********************************************************************************************
* @brief      markerの当たった時の処理を扱う
*********************************************************************************************
* @author     Yuta Takatsu
*********************************************************************************************
* Copyright © 2018 Yuta Takatsu All Rights Reserved.
**********************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MarkerColliderTrigger : BaseObject
{

    private GameObject objName;  // @brief markerのオブジェクトの名前を取得
    private int nuwListNumver;  // @brief 現在の配列番号
    List<GameObject> markerList; // @brief markerのリスト

    [SerializeField]
    private GameObject markerSign; // @brief　今目指すべきマーカーを示す矢印
    
    
    private void Start()
    {
        // 初期化
        nuwListNumver = 0;
       
        // 親オブジェクトを取得し
        objName = GameObject.Find("MarkerObj");
        // 取得した親オブジェクトの子も取得する
        markerList = GameObjectExtension.GetGameObject(objName);

        Debug.Log("なう:"+markerList[nuwListNumver].name);

        foreach (GameObject obj in markerList)
        {
            Debug.Log(obj.name);
        }

        // ポイントの移動
        markerSign.transform.position = new Vector3(markerList[nuwListNumver].transform.position.x,
                                                    markerList[nuwListNumver].transform.position.y + 11,
                                                    markerList[nuwListNumver].transform.position.z);
       
    }

    /// <summary>
    /// @brief あたり判定用メソッド
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter(Collider other)
    {


        // goalタグのオブジェクトに接触したときに走る命令
        if (markerList[nuwListNumver].name == other.name && other.tag == "goal")
        {
            Singleton<GameInstance>.instance.IsGoal = true;
        }
        // markerに当たったとき次のmarkerを指すようにする
        else if (markerList[nuwListNumver].name == other.name && other.tag != "goal")
        {
            nuwListNumver++;
            Debug.Log("マーカー:" + nuwListNumver);

            // ポイントの移動
            markerSign.transform.position = new Vector3(markerList[nuwListNumver].transform.position.x,
                                                    markerList[nuwListNumver].transform.position.y + 11,
                                                    markerList[nuwListNumver].transform.position.z);

        }
        
    }
}