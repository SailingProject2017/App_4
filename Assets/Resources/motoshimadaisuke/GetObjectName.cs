/***********************************************************************/
/*! @file   GetObjectName.cs
*************************************************************************
*   @brief  マーカの名前を当たったオブジェクトに渡す
*************************************************************************
*   @author daisuke motoshima
*************************************************************************/

using UnityEngine;

public class GetObjectName :BaseObject{
    EnemyAI enemyAI;//@brief EnemyAI型のオブジェクト取得
    GoalJudge goalJudge;//@brief GoalJuge型のオブジェクト取得
    private GameObject markerObjject;//@brief取得に必要なオブジェクト宣言
    private GameObject playerMarker;//@brief取得に必要なオブジェクト宣言
    private int i=0;//@briefループ用の変数
    // Use this for initialization
    void Start () {
        FindObject();
    }

    // Update is called once per frame
    public override void OnUpdate()
    {

    }
    /// <summary>
    /// 当たったオブジェクトに名前を渡す
    /// </summary>
    /// <param name="name"></param>
    void OnTriggerEnter(Collider name)
    {
        
            if (name.tag == "Enemy" )
        {
                enemyAI.markerObjectName = transform.name;
                Debug.Log(enemyAI.markerObjectName);
        }
        if (name.tag == "Player")
        {
            goalJudge.markerName = transform.name;
            Debug.Log(enemyAI.markerObjectName);
        }
    }
    /// <summary>
    /// オブジェクトを見つける
    /// </summary>
    void FindObject()
    {
        markerObjject = GameObject.Find("Enemy");
        playerMarker = GameObject.Find("Player");
        enemyAI = markerObjject.GetComponent<EnemyAI>();
        goalJudge = markerObjject.GetComponent<GoalJudge>();

    }
}

