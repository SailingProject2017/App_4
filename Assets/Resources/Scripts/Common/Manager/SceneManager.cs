﻿/***********************************************************************/
/*! @file   SceneManager.cs
*************************************************************************
*   @brief  シーンの制御をするマネージャークラス
*************************************************************************
*   @author Yuta Takatsu
*************************************************************************
*   Copyright © 2017 Yuta Takatsu All Rights Reserved.
************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// @brief シーンの列挙
/// </summary>
public enum SCENES
{
    TITLE,
    MODESELECT,
    TUTORIAL,
    CPUBATTLE,
    ONLINE,
    INTUTORIAL,
    INGAME,
    ONLINEBATTLE
}

// SceneManagerがUnityの予約語として登録されているため独自のnamespaceを作成
namespace SceneManagement
{
    public static class SceneManager
    {
        

        public static void SceneMove(SCENES NextScene)
        {

            // FadeManagerを呼び出す
            FadeManager.Instance.Load((int)NextScene);
        }
    }
}