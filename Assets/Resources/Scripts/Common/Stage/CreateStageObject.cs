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
    // setされたステージをgetする
   
    public void Start()
    {
        // setされたステージを判断して生成を行う
        switch (GameInstance.Instance.StageType)
        {
            case EStageType.TUTORIALSTAGE_SAILING:
                Debug.Log("a");
                break;
            case EStageType.TUTORIALSTAGE_UI:
                break;
            case EStageType.TUTORIALSTAGE_ACCEL:
                break;
            case EStageType.TUTORIALSTAGE_CURVE:
                break;
            case EStageType.TUTORIALSTAGE_CPU:
                break;
            case EStageType.STAGE_EAGY:
                break;
            case EStageType.STAGE_NORMAL:
                break;
            case EStageType.STAGE_HARD:
                break;
        }
    }
}