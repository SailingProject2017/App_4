/***********************************************************************/
/*! @file   RankManager.cs
*************************************************************************
*   @brief  順位を制御するクラス
*************************************************************************
*   @author daisuke motoshima
*************************************************************************/
using UnityEngine;

public class RankManager : BaseObject
{
    MarkerDistance markerDistance;                  //@brief MarkerDistance型の取得
    private GameObject[] enemyStats;                //@brief MarkerDistance型の取得に必要なオブジェクト
    public float[] distance;                        //@brief マーカーからプレイヤーまでの距離を求めたものが入った配列
    public int[] rank = { 1, 1, 1, 1 };             //@brief 順位の判定をする用の配列
    public int[] markerNum;                         //@brief 通ったマーカーの数が入った配列
    private int playerNum=4;                        //@brief プレイヤーの数
    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    public override void OnUpdate()
    {
        base.OnUpdate();
        Status();
        Judgment();
    }
    /// <summary>
    /// 順位の判定
    /// </summary>
    void Judgment()
    {
        for (int i = 0; i < playerNum; i++)
        {

            if (markerNum[i] < markerNum[i + 1])
            {

                rank[i] += 1;
                if (rank[i + 1] > 1)
                {
                    rank[i + 1] -= 1;
                }
            }
            else if (markerNum[i] > markerNum[i + 1])
            {
                rank[i + 1] += 1;
                if (rank[i] > 1)
                {
                    rank[i] -= 1;
                }
            }
            else if (markerNum[i] == markerNum[i + 1])
            {
                if (distance[i] < distance[i + 1])
                {
                    rank[i] += 1;
                    if (rank[i + 1] > 1)
                    {
                        rank[i + 1] -= 1;
                    }
                }
                else if (distance[i] > distance[i + 1])
                {
                    rank[i + 1] += 1;
                    if (rank[i] > 1)
                    {
                        rank[i] -= 1;
                    }
                }
            }
        }

    }
    /// <summary>
    ///  値をほかのスクリプトを参照して値を取得する
    /// </summary>
    void Status()
    {
        enemyStats = new GameObject[playerNum];
        markerNum = new int[playerNum];
        distance = new float[playerNum];
        for (int k = 0; k < playerNum; k++)
        {
            enemyStats[k] = GameObject.Find("Enemy");
            if (enemyStats == null)
            {
                enemyStats[k] = GameObject.Find("Player");
            }
        }

        for (int i = 0; i < playerNum; i++)
        {
            markerDistance = enemyStats[i].GetComponent<MarkerDistance>();
            markerNum[i] = markerDistance.markerCnt;
            distance[i] = markerDistance.playerDistans;
        }

    }
}
