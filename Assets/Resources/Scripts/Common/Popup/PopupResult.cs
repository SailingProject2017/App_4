/**************************************************************************************/
/*! @file   PopupResult.cs
***************************************************************************************
@brief      リザルト画面を表示させるクラス
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
    private GameObject contents; // @brief リザルト画面を格納


    /// <summary>
    /// @brief ポップアップを開く
    /// </summary>
    public void Open()
    {
        Singleton<GameInstance>.instance.IsShipMove = false;
        base.ButtonSet = EButtonSet.SetNone; // 基底クラスのButtonSetに対応するボタンを指定
        base.Open(null, null, ResultPopupOpen); // 基底クラスのOpenメソッドを呼び出す
        ResultPopupOpen();
    }

    /// <summary>
    /// @brief チュートリアル終了時、リザルト用ポップアップを表示させる
    /// </summary>
    private void ResultPopupOpen()
    {
        contents.SetActive(true); // 非表示だったリザルト画面を表示
    }
    
}