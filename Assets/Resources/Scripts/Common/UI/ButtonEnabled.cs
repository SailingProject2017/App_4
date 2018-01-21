﻿/**********************************************************************************************/
/*! @file     ButtonEnabled.cs
*********************************************************************************************
* @brief      取得したボタンの機能を消す
*********************************************************************************************
* @author     yuta takatsu
*********************************************************************************************
* Copyright © 2017 yuta takatsu All Rights Reserved.
**********************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEnabled : BaseObject
{

    [SerializeField]
    private Canvas obj;  // @brief キャンバスのオブジェクト
    [SerializeField]
    private Button[] button; // @brief ボタンを登録する配列

    private void Start()
    {

        // 子のボタンコンポーネントを取得
        button = obj.GetComponentsInChildren<Button>();
    }

    /// <summary>
    /// @brief ボタンがタップされたときに呼ばれる指定したボタンのみを機能させる
    /// </summary>
    public void OnButtonTap()
    {

        if (Singleton<TutorialState>.instance.TutorialStatus == eTutorial.eTutorial_End)
        {
            // 配列内のボタンすべての機能を停止する
            for (int i = 0; i < button.Length; i++)
                button[i].enabled = false;
        }
        else
        {
            // 配列内のボタンすべての機能を停止する
            for (int i = 0; i < button.Length; i++)
                button[i].interactable = false;
        }
    }
}