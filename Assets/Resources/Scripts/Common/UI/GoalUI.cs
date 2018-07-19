/**********************************************************************************************/
/*@file       CountDown.cs
*********************************************************************************************
* @brief      すべてのオブジェクトを管理するための基底クラス
*********************************************************************************************
* @author     Ryo Sugiyama
*********************************************************************************************
* Copyright © 2018 Ryo Sugiyama All Rights Reserved.
**********************************************************************************************/
using UnityEngine;

public class GoalUI : BaseObject
{

    [SerializeField]
    private GameObject goalUI;  // @brief ゴール時のUI用変数

    // Use this for initialization
    void Start()
    {
        goalUI.gameObject.SetActive(false);
    }

    // Update is called once per frame
    public override void OnUpdate()
    {
        base.OnUpdate();


        if (Singleton<GameInstance>.Instance.IsGoal)
        {
            goalUI.gameObject.SetActive(true);
        }
    }

    public override void OnEnd()
    {
        base.OnEnd();
        if (goalUI.gameObject == null) return;
        goalUI.gameObject.SetActive(false);

    }
}
