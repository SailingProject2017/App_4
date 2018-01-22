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
   
    public void Start()
    {

        New(StageObject[(int)GameInstance.Instance.StageType]);// 生成
        
    }
}