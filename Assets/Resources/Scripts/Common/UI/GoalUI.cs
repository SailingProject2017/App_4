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
    private Sprite maskUI;

    [SerializeField]
    private Sprite goalUI;

    private SpriteRenderer spriteRenderer;


    // Use this for initialization
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = maskUI;
        Singleton<GameInstance>.Instance.IsGoal = false;
    }

    // Update is called once per frame
    public override void OnUpdate()
    {
        base.OnUpdate();

       if (Singleton<GameInstance>.Instance.IsGoal)
        {
            spriteRenderer.sprite = goalUI;
        } 
    }
}
