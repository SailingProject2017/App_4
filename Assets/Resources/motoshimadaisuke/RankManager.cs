using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class RankManager : BaseObject
{
    private List<GameObject> shipObject = new List<GameObject>();             //@brief 船を格納するgameobject型のリスト
    private List<MarkerDistance> shipDistance = new List<MarkerDistance>();   //@brief 変数を取得する用のMarkerDistance型のリスト
    [SerializeField]
    private int[] rank;                                                     //@brief ランク判別用の配列
    private int[] rankImage;                                                 //@brief 判別したランクをRankImageへ渡すための配列
    private int arrayNum;                                                  //@brief rankの配列の中にかぶりがないか判別する用の変数
    private int exceptionNum;                                               //@brief rankの配列に同じ数字がはいっていたときにそれを正す時に使用する変数

    // Use this for initializatio
    void Start()
    {
        //初期化
        
        rankImage = new int[4];
        rank = new int[4];
        Status();
        //ほかのスクリプトから変数を取得を可能にする関数
    }
    // Update is called once per frame
    public override void OnUpdate()
    {
        base.OnUpdate();
        Judgment();

    }
    void Status()
    {
        for (int i = 0; i < 4; i++)
        {
            if (GameObject.Find("Enemy" + i) != null)
            {
                shipObject.Add(GameObject.Find("Enemy" + i));// enemyの取得
            }
            else
            {
                shipObject.Add(GameObject.Find("Player"));// Playerの取得
            }

        }
        for (int j = 0; j < 4; j++)
            shipDistance.Add(shipObject[j].GetComponent<MarkerDistance>());// 取得したものを使ってMarkerDistanceにアクセスする
    }

    /// <summary>
    /// 順位判定
    /// </summary>
    void Judgment()
    {
        for(int i=0;i<4; i++)
        {
            rank[i] = 1;
        }
        for (int i = 0; i < 4; i++)
        {
            for (int j =i+ 1; j < 4; j++)
            {
                if (shipDistance[i].MarkerCnt > shipDistance[j].MarkerCnt)//マーカーの数の判別
                {
                    rank[j] += 1;
                }
                else if (shipDistance[i].MarkerCnt < shipDistance[j].MarkerCnt)//マーカーの数の判別
                {
                    rank[i] += 1;

                }
                else if (shipDistance[i].MarkerCnt == shipDistance[j].MarkerCnt)//マーカーの数の判別
                {
                    if (shipDistance[i].Distans > shipDistance[j].Distans)//距離の判別
                    {
                        rank[i] += 1;
                    }
                    else if (shipDistance[i].Distans < shipDistance[j].Distans)//距離の判別
                    {
                        rank[j] += 1;
                    }
                }
            }
        }
       
        ExceptionRank();
        for (int i = 0; i < 4; i++)
        {
            rankImage[i] = rank[i];
        }
    }/// <summary>
    /// 描画用の変数を渡すアクセサー
    /// </summary>
   
    public int imageRank
    {
        get { return rankImage[3]; }
    }
    /// <summary>
    /// 例外処理
    /// </summary>
    void ExceptionRank()
    {
        exceptionNum = 4;
        for (int i = 0; i < 4; i++)
        {
            if (rank[i] > exceptionNum)// 一定の数以上で突入
            {
                rank[i] = exceptionNum;
                arrayNum = Array.IndexOf(rank, exceptionNum);// 配列に同じ要素がないか検索　要素があった場合その要素の配列番号　なかった場合負の数を返す
                if (arrayNum > 0)
                {
                    exceptionNum--;
                    rank[arrayNum] = exceptionNum;//見つけた値に重なった数の１個すくない値を入れる

                }
            }
        }
    }
}