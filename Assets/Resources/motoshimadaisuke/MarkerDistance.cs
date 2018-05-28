/***********************************************************************/
/*! @file  MarkerDistance.cs
*************************************************************************
*   @brief  プレイヤーからマーカーまでの距離と通ったマーカーの数をRankManagerに渡すクラス
*************************************************************************
*   @author daisuke motoshima
*************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scene;
public class MarkerDistance : BaseObject
{
    private float playerDistans;                //@briefマーカーまでの距離を格納する変数
    [SerializeField]
    private List<GameObject> markerObject;      //@briefマーカーを格納するリスト
    private int num = 5;                        //@briefマーカーの数                 
    ///TODO: ほかの作業が終了次第privateにする
    private int markerCnt = 0;                  //@briefマーカーを通った数を格納するクラス
    [SerializeField]
    private SCENES nextScene; // @brief 次のシーン格納用
                              // Use this for initialization
    void Start()
    {
        FindObject();
    }

    // Update is called once per frame
    public override void OnUpdate()
    {
        base.OnUpdate();
        Distance();
    }
    /// <summary>
    /// マーカーを発見しmarkerObjectに入れる
    /// </summary>
    void FindObject()
    {
       
        for (int i = 0; i < num; i++)
        {
            markerObject.Add ( GameObject.Find("HitMarker" + i));//+iでマーカーの番号を示して要素の数だけFindして見つける
            Debug.Log(markerObject[i] + " MarkerDistance");
            if (markerObject[i] == null)
            {
                Debug.Log("null");
                markerObject.Add(GameObject.Find("Center"));
            }
            Debug.Log(markerObject[i].transform.position);
        }
    }
    /// <summary>
    /// マーカーまでの距離を求める関数
    /// </summary>
    void Distance()
    {
        playerDistans = (transform.position - markerObject[markerCnt].transform.position).magnitude;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "first")
        {
            markerCnt++;
        }

        if (other.tag == "goal" && markerCnt == 4 && this.tag == "Ship")
        {
            SceneManager.SceneMove(nextScene); // SceneManagerを呼び出す 引数は次のシーン
        }
    }
    /// <summary>
    /// RankMnagerに渡す用のアクセサー
    /// </summary>
    public float Distans
    {
        get { return playerDistans; }
    }
    public int MarkerCnt
    {
        get { return markerCnt; }
    }
}