/***********************************************************************/
/*! @file   RankManager.cs
*************************************************************************
*   @brief  順位を制御するクラス
*************************************************************************
*   @author daisuke motoshima
*************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankManager : BaseObject {
    public int[] markerNum;
    MarkerDistance markerDistance;
    private GameObject[] enemyStats;
    public float[] distance;
    public int[] rank= { 1, 1, 1, 1 };
    private int playerNum;
    // Use this for initialization
    void Start () {
       
        
    }

    // Update is called once per frame
    public override void OnUpdate()
    {
        base.OnUpdate();       
        Status();
        Judgment();
    }
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
    void Status()
    {
        enemyStats = new GameObject[playerNum];
        markerNum = new int[playerNum];
        distance = new float[playerNum];
        for(int k = 0; k < playerNum; k++)
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
            distance[i] =markerDistance.playerDistans;
        }
        
    }
}
