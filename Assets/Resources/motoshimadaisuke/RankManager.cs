using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankManager : BaseObject {
    public int[] MarkerNum;
    MarkerDistance markerDistance;
    private GameObject[] EnemyStats;
    public float[] Distance;
    public int[] Rank= { 1, 1, 1, 1 };
    private int PlayerNum;
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
        for (int i = 0; i < PlayerNum; i++)
        {

            if (MarkerNum[i] < MarkerNum[i + 1])
            {

                Rank[i] += 1;
                if (Rank[i + 1] > 1)
                {
                    Rank[i + 1] -= 1;
                }
            }
            else if (MarkerNum[i] > MarkerNum[i + 1])
            {
                Rank[i + 1] += 1;
                if (Rank[i] > 1)
                {
                    Rank[i] -= 1;
                }
            }
            else if (MarkerNum[i] == MarkerNum[i + 1])
            {
                if (Distance[i] < Distance[i + 1])
                {
                    Rank[i] += 1;
                    if (Rank[i + 1] > 1)
                    {
                        Rank[i + 1] -= 1;
                    }
                }
                else if (Distance[i] > Distance[i + 1])
                {
                    Rank[i + 1] += 1;
                    if (Rank[i] > 1)
                    {
                        Rank[i] -= 1;
                    }
                }
            }
        }
        
    }
    void Status()
    {
        EnemyStats = new GameObject[PlayerNum];
        MarkerNum = new int[PlayerNum];
        Distance = new float[PlayerNum];
        for(int k = 0; k < PlayerNum; k++)
        {
            EnemyStats[k] = GameObject.Find("Enemy");
            if (EnemyStats == null)
            {
                EnemyStats[k] = GameObject.Find("Player");
            }
        }
        
        for (int i = 0; i < PlayerNum; i++)
        {
            markerDistance = EnemyStats[i].GetComponent<MarkerDistance>();
            MarkerNum[i] = markerDistance.MarkerCnt;
            Distance[i] =markerDistance.PlayerDistans;
        }
        
    }
}
