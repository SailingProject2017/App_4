/***********************************************************************/
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
   
    // 設定された列挙体を格納する
    [SerializeField]
    private EStageType stageType;

 
    protected override void AppendListConstructor()
    {
        base.AppendListConstructor();
        GameInstance.Instance.StageType = EStageType.NULL;
    }

    /// <summary>
    ///  タップされたときに呼ばれる
    ///</summary>
    public void OnTap()
    {
        GameInstance.Instance.StageType = stageType;
        Debug.Log(GameInstance.Instance.StageType);
     
    }
}
