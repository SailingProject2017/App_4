using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerDistance : BaseObject {
    public float PlayerDistans;
    [SerializeField]
    private GameObject[] MarkerObject;
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
    void FindObject()//マーカーを発見しMarkerObjectに入れる
    {
        MarkerObject = new GameObject[num];
        for (i = 0; i < num; i++)
        {
            MarkerObject[i] = GameObject.Find("HitMarker" + i);//+iでマーカーの番号を示して要素の数だけFindして見つける

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
        PlayerDistans = ((transform.position.x - MarkerObject[markerCnt].transform.position.x) + (transform.position.z - MarkerObject[markerCnt].transform.position.z));
    }
}
