/***********************************************************************/
/*! @file   GetObjectName.cs
*************************************************************************
*   @brief  マーカの名前を当たったオブジェクトに渡す
*************************************************************************
*   @author daisuke motoshima
*************************************************************************/

using UnityEngine;

public class GetObjectName :BaseObject{
    GoalJudge goalJudge;                // @brief GoalJuge型のオブジェクト取得
    private GameObject markerObjject;   // @brief 取得に必要なオブジェクト宣言
    private GameObject playerMarker;    // @brief 取得に必要なオブジェクト宣言
    private int i = 0;                  // @brief ループ用の変数
    // Use this for initialization
    void Start () {
        FindObject();
    }

    // Update is called once per frame
    public override void OnUpdate()
    {

    }
    /// <summary>
    /// @brief当たったオブジェクトに名前を渡す
    /// </summary>
    /// <param name="name"></param>
    void OnTriggerEnter(Collider name)
    {
        if (name.tag == "Player")
        {
            goalJudge.markerName = transform.name;
            Debug.Log(goalJudge.markerName);
        }
    }
    /// <summary>
    /// @briefオブジェクトを見つける
    /// </summary>
    void FindObject()
    {
        playerMarker = GameObject.Find("Player");
        goalJudge = markerObjject.GetComponent<GoalJudge>();

    }
}

