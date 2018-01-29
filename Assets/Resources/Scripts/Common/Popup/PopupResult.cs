/**************************************************************************************/
/*! @file   PopupResult.cs
***************************************************************************************
@brief      PopupWindowのボタンを制御するクラス
***************************************************************************************
@author     yuta takatsu
***************************************************************************************
* Copyright © 2018 yuta takatsu All Rights Reserved.
***************************************************************************************/
using System.Collections;
using UnityEngine;

public class PopupResult : PopupBase
{

    [SerializeField]
    private GameObject contents; //@brief リザルト画面を格納


    /// <summary>
    /// @brief ポップアップを開く
    /// </summary>
    public void Open()
    {
        base.ButtonSet = EButtonSet.SetNone; // 基底クラスのButtonSetに対応するボタンを指定
        base.Open(null, null, PopupOpenEnd); // 基底クラスのOpenメソッドを呼び出す
    }

    /// <summary>
    /// @brief チュートリアル終了時、リザルト用ポップアップを表示させる
    /// </summary>
    private void PopupOpenEnd()
    {
        contents.SetActive(true); // 非表示だったリザルト画面を表示
    }
    
}