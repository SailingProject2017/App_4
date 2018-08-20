/**************************************************************************************/
/*! @file   ControllerSetting.cs
***************************************************************************************
@brief      設定画面のコントローラー関係の設定を行うクラス
***************************************************************************************
@author     yuta takatsu
***************************************************************************************
* Copyright © 2018 yuta takatsu All Rights Reserved.
***************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ControllerSetting : BaseObject
{

    /// <summary>
    /// @brief ジャイロのフラグを切り替える
    /// </summary>
    public void GyroChanged()
    {
        BaseObjectSingleton<GameInstance>.Instance.IsGyro = !BaseObjectSingleton<GameInstance>.Instance.IsGyro;
    }

    /// <summary>
    /// @brief スワイプのフラグを切り替える
    /// </summary>
    public void SwipeChanged()
    {
        BaseObjectSingleton<GameInstance>.Instance.IsSwipe = !BaseObjectSingleton<GameInstance>.Instance.IsSwipe;
    }
}
