using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankManager : BaseObject
{
    public int[] MarkerNum;
    MarkerDistance markerDistance;
    private GameObject[] EnemyStats;
    public float[] Distance;
    public int[] Rank = { 1, 1, 1, 1 };
    private int rankMin;
    private int PlayerNum = 4;
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
        rankMin = 4;
        for (int i = 0; i < PlayerNum - 1; i++)
        {
            if (MarkerNum[3] < MarkerNum[i])
            {
                Rank[3] += 1;

            }
            else
            {
                Rank[3] -= 1;

            }
            if (Rank[3] <= 1)
            {
                Rank[3] = 1;

            }
            else if (Rank[3] >= 4)
            {
                Rank[3] = 4;
            }
        }//ループ
         //rankMin = 4;
        for (int j = 0; j < PlayerNum - 1; j++)
        {
            if (MarkerNum[3] == MarkerNum[j])
            {
                if (Distance[3] > Distance[j])
                {
                    Rank[3] += 1;
                    if (Rank[3] >= 4)
                    {
                        Rank[3] = 4;
                    }
                }
                else
                {
                    Rank[3] -= 1;
                    if (Rank[3] <= 1)
                    {
                        Rank[3] = 1;

                    }
                }
            }
        
    }//関数

}
    void Status()
    {
        EnemyStats = new GameObject[PlayerNum];
        MarkerNum = new int[PlayerNum];
        Distance = new float[PlayerNum];
        for (int k = 0; k < PlayerNum; k++)
        {
            EnemyStats[k] = GameObject.Find("Enemy" + k);
            Debug.Log(EnemyStats[k]);
            if (EnemyStats[k] == null)
            {
                EnemyStats[k] = GameObject.Find("Player");
            }
        }

        for (int i = 0; i < PlayerNum; i++)
        {
            markerDistance = EnemyStats[i].GetComponent<MarkerDistance>();
            MarkerNum[i] = markerDistance.markerCnt;
            Distance[i] = markerDistance.PlayerDistans;
        }

    }
}
