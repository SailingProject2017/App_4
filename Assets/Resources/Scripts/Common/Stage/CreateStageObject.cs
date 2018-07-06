/***********************************************************************/
/*! @file   CreateStageObject.cs
*************************************************************************
*   @brief  ステージを生成するスクリプト
*************************************************************************
*   @author yuta takatsu
*************************************************************************
*   Copyright © 2017 yuta takatsu All Rights Reserved.
************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateStageObject : BaseObject
{

    [SerializeField]
    private GameObject[] StageObject; // 生成をしたいステージを格納する配列

    /// <summary>
    /// @brief ステージの初期化関数
    /// </summary>
    private void StageInitialize()
    {
        GameObject[] StageObject = new GameObject[8];
    }

    /// <summary>
    /// @brief ステージの生成関数
    /// </summary>
    /// <param name="eStageType"></param>
    private void CreateStage(eStageType eStageType)
    {
        if(New(StageObject[(int)eStageType]) == null)
        {
            New(StageObject[(int)eStageType.eTutorialStage_Straight]);
        }
    }

    protected override void OnAwake()
    {
        base.OnAwake();
        StageInitialize();
        CreateStage(GameInstance.Instance.StageType);
    }
}