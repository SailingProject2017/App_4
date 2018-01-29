/***********************************************************************/
/*! @file   StageTypeEnum.cs
*************************************************************************
*   @brief  ステージのタイプを判別するための列挙体を宣言
*************************************************************************
*   @author yuta takatsu
*************************************************************************
*   Copyright © 2017 yuta takatsu All Rights Reserved.
************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// @brief ステージの列挙体
/// @note  選ばれたステージを判断する用
/// </summary>
public enum EStageType
{
    TutorialStage_Sailing,
    TutorialStage_UI,
    TutorialStage_Accel,
    TutorialStage_Curve,
    Stage_Easy,
    Stage_Normal,
    Stage_Hard,
    Null
}
