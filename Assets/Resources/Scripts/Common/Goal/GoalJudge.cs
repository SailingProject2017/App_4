/***********************************************************************/
/*! @file   GoalJudge.cs
*************************************************************************
*   @brief  船のゴール判定を行うスクリプト
*************************************************************************
*   @author daisuke motoshima
*************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Scene;

public class GoalJudge : BaseObject
{
    private GameObject markerdistance;  //@briefMarkerDistance型の取得用のオブジェクト
    MarkerDistance markerDistance;      //@briefMarkerDistance型の取得用の変数
    const byte makerNum = 4;            //@briefマーカーの数
    public string markerName;           //@briefもらったマーカーの名前を格納する変数
    [SerializeField]
    SCENES nextScene;
    /// <summary>
    /// ゴールの判定をするスクリプト
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        if (markerName == "HitMarker" + markerDistance.markerCnt) 
            {
            markerDistance.markerCnt++;

            }
        //Debug.Log("hit");
        if (other.tag == "goal"&& markerDistance.markerCnt == makerNum)
        {
            SceneManager.SceneMove(nextScene);
        }
    }
    /// <summary>
    /// 変数の取得
    /// </summary>
    void Start()
    {
        markerdistance = GameObject.Find("Player");
        markerDistance = markerdistance.GetComponent<MarkerDistance>();
    }
}