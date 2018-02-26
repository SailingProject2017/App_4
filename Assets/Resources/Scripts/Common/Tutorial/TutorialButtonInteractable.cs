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

    private eTutorial state; // @brief チュートリアルの状態を表す
   
    void Start()
    {

        // 配列にいるすべてのボタンの機能を無効化する
        for(int i = 0; i < setButton.Length; i++)
        {
            setButton[i].interactable = false;
        }

        // 現在の状態を取得し値をSetButtonに渡す
        state = Singleton<TutorialState>.instance.TutorialStatus;
        SetButton();
    }

    /// <summary>
    /// @brief 取得した値によって配列内のボタンのinteractableを有効化する
    /// </summary>
    void SetButton()
    {
        // 状態が切り替わっていた場合stateに現在の状態を代入
        if(state != Singleton<TutorialState>.instance.TutorialStatus)
        {
            state = Singleton<TutorialState>.instance.TutorialStatus;
        }

        if (setButton.Length >=(int) state)
        {
            // 指定したボタンを有効化
            setButton[(int)state - 1].interactable = true;
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