/**********************************************************************************************/
/*! @file     TutorialButtonInteractable.cs
*********************************************************************************************
* @brief      チュートリアルボタンの有効化/無効化を行う
*********************************************************************************************
* @author     yuta takatsu
*********************************************************************************************
* Copyright © 2017 yuta takatsu All Rights Reserved.
**********************************************************************************************/
using UnityEngine;
using UnityEngine.UI;

public class TutorialButtonInteractable : BaseObject
{

    [SerializeField]
    private Button[] setButton; // @brief TutorialStateによってinteractableを切り替えるボタンを登録しておく

    void Start()
    {

        // 配列にいるすべてのボタンの機能を無効化する
        for(int i = 0; i < setButton.Length; i++)
        {
            setButton[i].interactable = false;
        }
        SetButton();
    }

    /// <summary>
    /// @brief 取得した値によって配列内のボタンのinteractableを有効化する
    /// </summary>
    void SetButton()
    {
        
        if (Singleton<TutorialState>.instance.TutorialStatus == eTutorial.eTutorial_ModeSelect)
        {
            // 指定したボタンを有効化
            setButton[0].interactable = true;
        }
        else
        {
            // 配列にいるすべてのボタンの機能を有効化する
            for (int i = 0; i < setButton.Length; i++)
            {
                setButton[i].interactable = true;
            }
        }
    }
}