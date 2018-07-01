/**********************************************************************************************/
/*@file       CountDown.cs
*********************************************************************************************
* @brief      すべてのオブジェクトを管理するための基底クラス
*********************************************************************************************
* @author     Yuta Takatsu and Ryo Sugiyama
*********************************************************************************************
* Copyright © 2017 Yuta Takatsu and Ryo Sugiyama All Rights Reserved.
**********************************************************************************************/
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : BaseObject
{

    [SerializeField]
    private Text countDownText;      // @brief Textのインスタンス 

    private bool isCallOnce = false; // @brief フラグ格納

    void Start()
    {
        countDownText.text = "";
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (BaseObjectSingleton<GameInstance>.Instance.IsCountDown)
        {
            // 一番最初だけカウントダウンを行う
            if (!isCallOnce)
            {
                StartCoroutine(CountDownCoroutine());
                BaseObjectSingleton<GameInstance>.Instance.IsCountDown = false;
                isCallOnce = !isCallOnce;
            }
            else
            {
                // 船の移動許可
                Singleton<GameInstance>.instance.IsShipMove = true;
                BaseObjectSingleton<GameInstance>.Instance.IsCountDown = false;
            }
        }

    }
    
    /// <summary>
    /// @brief カウントダウンの処理
    /// @note コルーチンを使用
    /// </summary>
    public IEnumerator CountDownCoroutine()
    {

        //　カウントダウンテキストの表示
        countDownText.gameObject.SetActive(true);

        // 3
        countDownText.text = "3";
        Singleton<SoundPlayer>.instance.PlaySE("0");
        yield return new WaitForSeconds(1.0f);

        // 2
        countDownText.text = "2";
        Singleton<SoundPlayer>.instance.PlaySE("0");
        yield return new WaitForSeconds(1.0f);

        // 1
        countDownText.text = "1";
        Singleton<SoundPlayer>.instance.PlaySE("0");
        yield return new WaitForSeconds(1.0f);

        // GO
        countDownText.text = "GO!";
        Singleton<SoundPlayer>.instance.PlaySE("4");

        // 船の移動許可
        Singleton<GameInstance>.instance.IsShipMove = true;
        BaseObjectSingleton<GameInstance>.Instance.IsCountDown = false;
        yield return new WaitForSeconds(1.0f);

        //　非表示にする
        countDownText.text = "";
        countDownText.gameObject.SetActive(false);

    }
}