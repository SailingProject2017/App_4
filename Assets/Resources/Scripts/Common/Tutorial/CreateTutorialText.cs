﻿/***********************************************************************/
/*! @file   CreateTutorialText.cs
*************************************************************************
*   @brief  説明用テキストをポップアップ内に生成するスクリプト
*************************************************************************
*   @author yuta takatsu
*************************************************************************
*   Copyright © 2018 yuta takatsu All Rights Reserved.
************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTutorialText : CanvasBase {

    [SerializeField]
    private List<GameObject> tutorialText = new List<GameObject>(); // @brief テキストを配列内に格納する
    private bool isCallOnce;             // @brief 一度だけ呼ばれることを保証する
    
    public void Start()
    {

        isCallOnce = false;
    }

    public override void OnUpdate()
    {
        if (Singleton<TutorialState>.instance.TutorialStatus != eTutorial.eTutorial_End)
        {
            if (!isCallOnce)
            {
                // チュートリアルのステートに対応したテキストをCanvas内に生成する -1で調整
                NewCanvasInGameObject(tutorialText[(int)Singleton<TutorialState>.instance.TutorialStatus - 1]);
                isCallOnce = !isCallOnce;
            }
            if (BaseObjectSingleton<GameInstance>.Instance.IsTutorialState)
            {
                // 前のテキストを消す -2で調整
                Delete(prefab);
                // チュートリアルが変わったときもう一度テキストを生成できる状態にする
                BaseObjectSingleton<GameInstance>.Instance.IsTutorialState = false;
                isCallOnce = !isCallOnce;
            }
        }

    }
}
