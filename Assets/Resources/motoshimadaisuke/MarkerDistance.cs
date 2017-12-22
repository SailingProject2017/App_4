/***********************************************************************/
/*! @file   MarkerDistans.cs
*************************************************************************
*   @brief  マーカーまでの距離を求めるクラス
*************************************************************************
*   @author daisuke motoshima
*************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerDistance : BaseObject {
    public float playerDistans;
    [SerializeField]
    private GameObject[] markerObject;
    private int num, i;
    public int markerCnt;
    
	// Use this for initialization
	void Start () {
        FindObject();

    }

    // Update is called once per frame
    public override void OnUpdate()
    {
        base.OnUpdate();
        Distance();
    }
    void FindObject()//マーカーを発見しmarkerObjectに入れる
    {
        markerObject = new GameObject[num];
        for (i = 0; i < num; i++)
        {
            markerObject[i] = GameObject.Find("HitMarker" + i);//+iでマーカーの番号を示して要素の数だけFindして見つける

            if (markerObject[i] == null)
            {
                Debug.Log("null");
                markerObject[i] = GameObject.Find("Center");
            }
            Debug.Log(markerObject[i].transform.position);
        }
    }
    void Distance()
    {
        playerDistans = ((transform.position.x - markerObject[markerCnt].transform.position.x) + (transform.position.z - markerObject[markerCnt].transform.position.z));
    }
}
