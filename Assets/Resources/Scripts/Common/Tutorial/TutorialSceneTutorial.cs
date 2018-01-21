/**************************************************************************************/
/*! @file   TutorialSceneTutorial.cs
***************************************************************************************
@brief      モードセレクトのチュートリアルで使うポップアップを制御
***************************************************************************************
@author     yuta takatsu
***************************************************************************************
* Copyright © 2017 yuta takatsu All Rights Reserved.
***************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSceneTutorial : PopupBase {

    [SerializeField]
    private GameObject contens; // @brief 生成場所
    [SerializeField]
    private eTutorial type; // @brief チュートリアルの状態を判断する
    [SerializeField]
    private GameObject[] tutorialType; // @brief 各チュートリアルを格納するための配列

    /// <summary>
    /// @brief 開き終わったらコンテンツをtrueにして表示する
    /// </summary>
    public void OpenEnd()
    {
        contens.SetActive(true);
        switch (type) // 特定のチュートリアルのみを表示にする
        {
            case eTutorial.eTutorial_Sailing:
                tutorialType[0].SetActive(true);
                break;
            case eTutorial.eTutorial_InGameUI:
                tutorialType[1].SetActive(true);
                break;
            case eTutorial.eTutorial_Accel:
                tutorialType[2].SetActive(true);
                break;
            case eTutorial.eTutorial_Curve:
                tutorialType[3].SetActive(true);
                break;
            case eTutorial.eTutorial_CPU:
                tutorialType[4].SetActive(true);
                break;
        }
    }
    // 閉じ終わったらfalseにし非表示にする
    public void CloseEnd()
    {
        contens.SetActive(false);
        switch (type) // 特定のチュートリアルのみを非表示にする
        {
            case eTutorial.eTutorial_Sailing:
                tutorialType[0].SetActive(false);
                break;
            case eTutorial.eTutorial_InGameUI:
                tutorialType[1].SetActive(false);
                break;
            case eTutorial.eTutorial_Accel:
                tutorialType[2].SetActive(false);
                break;
            case eTutorial.eTutorial_Curve:
                tutorialType[3].SetActive(false);
                break;
            case eTutorial.eTutorial_CPU:
                tutorialType[4].SetActive(false);
                break;
        }
    }

    /// <summary>
    /// @brieg ポップアップ中のイベントを管理する
    /// </summary>
    /// <param name="id"></param>
    private void PopupAction(EButtonId id)
    {
        Close();
    }

    /// <summary>
    /// @brief ポップアップを開く
    /// </summary>
    public void Open()
    {
        ButtonSet = EButtonSet.Set1; // ボタンは一つ
        PopupButton.OnClickCallback = PopupAction;

        base.Open(null, null, OpenEnd);
    }
    /// <summary>
    /// @brief ポップアップを閉じる
    /// </summary>
    public void Close()
    {
        base.Close(CloseEnd, null, null);
    }

}
