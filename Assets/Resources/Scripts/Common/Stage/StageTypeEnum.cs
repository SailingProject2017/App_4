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
    TUTORIALSTAGE_SAILING,
    TUTORIALSTAGE_UI,
    TUTORIALSTAGE_ACCEL,
    TUTORIALSTAGE_CURVE,
    TUTORIALSTAGE_CPU,
    STAGE_EAGY,
    STAGE_NORMAL,
    STAGE_HARD,
    NULL
}
