using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scene;
public class MarkerDistance : BaseObject
{
    private float PlayerDistans;
    [SerializeField]
    private GameObject[] MarkerObject;
    private int num = 5, i;
    private float distansSet;
    private int markerNumSet;
    ///TODO: ほかの作業が終了次第privateにする
    private int markerCnt = 0;
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
    void FindObject()//マーカーを発見しMarkerObjectに入れる
    {
        MarkerObject = new GameObject[num];
        for (i = 0; i < num; i++)
        {
            MarkerObject[i] = GameObject.Find("HitMarker" + i);//+iでマーカーの番号を示して要素の数だけFindして見つける
            Debug.Log(MarkerObject[i] + " MarkerDistance");
            if (MarkerObject[i] == null)
            {
                Debug.Log("null");
                MarkerObject[i] = GameObject.Find("Center");
            }
            Debug.Log(MarkerObject[i].transform.position);
        }
    }
    void Distance()
    {
        PlayerDistans = (transform.position - MarkerObject[markerCnt].transform.position).magnitude;
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
    public float Distans
    {
        get { return PlayerDistans; }
    }
    public int MarkerCnt
    {
        get { return markerCnt; }
    }
}