/******************************************************
*! @file PopUpBackground.cs
*******************************************************
* @brief ポップアップが表示、非表示するスクリプト
*******************************************************
* @author Reina Sawai
*******************************************************
* Copyright © 2017 Reina Sawai All Rights Reserved.
*******************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PopUpBackground : BaseObject
{

    private bool isScreenTap; // @brief ポップアップの開くボタンが押されたかどうか

    void Start()
    {
        isScreenTap = true; //@true 閉じている状態
        OnTap(); // @brief ボタンが押された時の処理 

    }

    /// <summary>
    /// @brief ボタンが押された時の処理 
    /// </summary>
    public void OnTap()
    {
        
        if (isScreenTap == false) //@false 開いている状態かどうか
        {
            BackgroundLog(); //@brief ポップアップが表示
            isScreenTap = true;
        }
        else if (isScreenTap == true) //@true 閉じている状態かどうか
        {
            BackgroundLogClose(); //@brief ポップアップが非表示
            isScreenTap = false;

        }
    }
    /// <summary>
    /// @brief ポップアップが表示
    /// </summary>
    public void BackgroundLog()
    {
        transform.DOLocalMove(new Vector3(340, 0, 0), 0.3f).SetEase(Ease.InOutQuart);//指定された座標まで移動
       
    }
    /// <summary>
    /// @brief ポップアップが非表示
    /// </summary>
    public void BackgroundLogClose()
     {    
          transform.DOLocalMove(new Vector3(740, 0, 0), 0.3f).SetEase(Ease.InOutQuart);//指定された座標まで移動
    }

}
