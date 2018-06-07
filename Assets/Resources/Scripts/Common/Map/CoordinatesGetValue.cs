/***************************************************************/
/*!@file CoordinatesGetValue.cs
 ***************************************************************
 * @briefミニマップでマーカーのアイコン表示と座標の格納
 * *************************************************************
 * @author Reina Sawai
****************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinatesGetValue : MarkerBase
{
    /* マーカーを格納するリストについての宣言 */
    private List<GameObject> markerList;                //@brief Markerオブジェクトのリスト GameObject(取得したい型)
    private List<Vector3> tmpPos = new List<Vector3>(); //@brief Markerオブジェクトの座標を格納するリスト

    /* マーカー参照位置についての宣言 */
    private int currentMarker; 
    private int currentHitMarker;

    [SerializeField]
    private GameObject icon; //@brief アイコンオブジェクトを取得

    void Start()
    {
        MarkerInitialize();
    }

    /* マーカーオブジェクトの個数を取得する */
    protected override void MarkerInitialize()
    {
        base.MarkerInitialize();
        markerList = GameObjectExtension.GetGameObject(markerObjName,"Marker"); // 子のオブジェクト取得

        for (int i = 0; i < markerList.Count; i++)
        {
            Debug.Log(markerList[i].name);
            tmpPos.Add(markerList[i].gameObject.transform.position);
            Debug.Log(tmpPos[i]);
        }

        currentMarker = 0;    // マーカーの個数の初期化
        currentHitMarker = 2; // スタートがあるときは2で初期化
        ChangeIconPosition(); // アイコンの位置を初期化
    }

    /* アイコンをマーカー上に表示 */
    private void ChangeIconPosition()
    {
        Debug.Log(icon.gameObject.transform.position);
        // アイコンが現在のマーカーの上に表示される
        icon.transform.position = new Vector3( markerList[currentMarker].transform.position.x,
                                               markerList[currentMarker].transform.position.y + 10,
                                               markerList[currentMarker].transform.position.z);

       // TransformExtension.SetPosY(icon.gameObject.transform, 10.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        // マーカーが配列参照外だったらのエラー処理
        // Countで最後まで参照してくれる
        if (currentMarker == markerList.Count)
        {
            icon.SetActive(false);
            return;
        } 
        if (other.gameObject == hitMarkerList[currentHitMarker].gameObject)
        {
            currentMarker++;           // 次のマーカーを参照
            currentHitMarker += 2;     // 次のマーカーの出口のラインを参照 
            ChangeIconPosition();
            Debug.Log("hit!"); 
        }
    }
}
