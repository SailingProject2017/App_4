﻿/***********************************************************************/
/*! @file   AooendStageObject.cs
*************************************************************************
*   @brief  読み込みたいステージを格納する
*************************************************************************
*   @author yuta takatsu
*************************************************************************
*   Copyright © 2017 yuta takatsu All Rights Reserved.
************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppendStageObject : BaseObject
{
   
    [SerializeField]
    private StageType stageType; // @brief 設定された列挙体を格納する
    
    protected override void AppendListConstructor()
    {
        base.AppendListConstructor();
        GameInstance.Instance.StageType = StageType.NULL;
    }

    /// <summary>
    ///  @brief タップされたときに呼ばれるオブジェクト生成関数
    ///</summary>
    public void OnTap()
    {
        GameInstance.Instance.StageType = stageType;
    }
}
