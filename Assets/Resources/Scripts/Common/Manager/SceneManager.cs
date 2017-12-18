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

// シーンの列挙体
public enum SCENES
{
    TITLE,
    MODESELECT,
    TUTORIAL,
    CPUBATTLE,
    ONLINEBATTLE,
    INTUTORIAL,
    INGAME,
}

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