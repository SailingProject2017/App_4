/***********************************************************************/
/*! @file   GetObjectName.cs
*************************************************************************
*   @brief  マーカの名前を当たったオブジェクトに渡す
*************************************************************************
*   @author daisuke motoshima
*************************************************************************/

using UnityEngine;

public class GetObjectName :BaseObject{
    EnemyAI enemyAI;                    // @brief EnemyAI型のオブジェクト取得
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
    ///  @brief 当たったオブジェクトに名前を渡す
    /// </summary>
    /// <param name="name"></param>
    void OnTriggerEnter(Collider name)
    {
        
            if (name.tag == "Enemy" )
        {
                enemyAI.markerObjectName = transform.name;
                Debug.Log(enemyAI.markerObjectName);
        }
        if (name.tag == "Ship")
        {
            goalJudge.markerName = transform.name;
            Debug.Log(enemyAI.markerObjectName);
        }
    }
    /// <summary>
    /// @brief オブジェクトを見つける s
    /// </summary>
    void FindObject()
    {
        markerObjject = GameObject.Find("Enemy");
        playerMarker = GameObject.Find("Player");
        enemyAI = markerObjject.GetComponent<EnemyAI>();
        goalJudge = playerMarker.GetComponent<GoalJudge>();

    }
}

