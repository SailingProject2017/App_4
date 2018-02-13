/***********************************************************************/
/*! @file   MarkerDistans.cs
*************************************************************************
*   @brief  マーカーまでの距離を求めるクラス
*************************************************************************
*   @author daisuke motoshima
*************************************************************************/

using UnityEngine;

public class MarkerDistance : BaseObject
{
    public float playerDistans;　       // @brief マーカーからプレイヤーの位置までの距離が入ったスクリプト
    [SerializeField]
    private GameObject[] markerObject;  // @brief マーカー発見用の配列のオブジェクト
    private int num = 5;                // @brief 配列の要素数 
    private int i;                      // @brief ループ用
    public int markerCnt = 0;           // @brief マーカーが通った数を格納する変数
                                        // Use this for initialization
    void Start()
    {
        FindObject();

    }

   
    public override void OnUpdate()
    {
        base.OnUpdate();
        Distance();
    }
    /// <summary>
    /// @brief マーカーを見つけて配列に格納する関数
    /// </summary>
    void FindObject()
    {
        markerObject = new GameObject[num];
        for (i = 0; i < num; i++)
        {
            markerObject[i] = GameObject.Find("HitMarker" + i);// +iでマーカーの番号を示して要素の数だけFindして見つける

            if (markerObject[i] == null)
            {
                Debug.Log("null");
                markerObject[i] = GameObject.Find("Center");
            }
            Debug.Log(markerObject[i].transform.position);
        }
    }
    /// <summary>
    /// @brief マーカーまでの距離を求める
    /// </summary>
    void Distance()
    {
        playerDistans = ((transform.position.x - markerObject[markerCnt].transform.position.x) + (transform.position.z - markerObject[markerCnt].transform.position.z));
    }
}
