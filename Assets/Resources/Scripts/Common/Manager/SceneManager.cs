/***********************************************************************/
/*! @file   SceneManager.cs
*************************************************************************
*   @brief  シーンの制御をするマネージャークラス
*************************************************************************
*   @author yuta takatsu
*************************************************************************
*   Copyright © 2017 yuta takatsu All Rights Reserved.
************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

/// <summary>
/// @brief シーンの列挙
/// </summary>
public enum SCENES
{
    TITLE,
    MODESELECT,
    TUTORIAL,
    CPUBATTLE,
    INTUTORIAL,
    INGAME,
    ONLINEBATTLE
}

// SceneManagerがUnityの予約語として登録されているため独自のnamespaceを作成
namespace Scene
{
    public static class SceneManager
    {
        public static void SceneMove(SCENES NextScene)
        {
            // FadeManagerを呼び出す
            FadeManager.Instance.LoadScene((int)NextScene, 1.0f);
        }
    }
}