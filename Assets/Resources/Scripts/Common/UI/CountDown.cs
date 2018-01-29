/**********************************************************************************************/
/*@file   BaseObject.cs
*********************************************************************************************
* @brief      すべてのオブジェクトを管理するための基底クラス
*********************************************************************************************
* @author     Yuta Takatsu and Ryo Sugiyama
*********************************************************************************************
* Copyright © 2017 Yuta Takatsu and Ryo Sugiyama All Rights Reserved.
**********************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : BaseObject {

    [SerializeField]
    private Text countDownText;                // @brief 

    private ClosePopupAnimation closeAmimation; // @brief 

    public bool shipMoveFlag;                   // @brief 

    private bool isCallOnce;                    // @brief 一度だけ呼ばれたか調べるフラグ


    void Start () {
        closeAmimation = GetComponent<ClosePopupAnimation>();
        countDownText.text = "";
        isCallOnce = false;
    }

    public override void OnUpdate()
    {
        //　直前のポップアップが閉じられていてかつまだ一度も呼ばれていない時
        if (!isCallOnce && closeAmimation.ClosePop)
        {
            // 船を動かないようにし、カウントダウン開始
            shipMoveFlag = false;
            StartCountDown();
            isCallOnce = true;
        }
    }

    public void StartCountDown()
    {
        StartCoroutine(CountDownCoroutine());
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
        Singleton<SoundPlayer>.instance.playSE("0", 0.8f);
        yield return new WaitForSeconds(1.0f);

        // 2
        countDownText.text = "2";
        Singleton<SoundPlayer>.instance.playSE("0", 0.8f);
        yield return new WaitForSeconds(1.0f);

        // 1
        countDownText.text = "1";
        Singleton<SoundPlayer>.instance.playSE("0", 0.8f);
        yield return new WaitForSeconds(1.0f);

        // GO
        countDownText.text = "GO!";
        Singleton<SoundPlayer>.instance.playSE("4", 0.8f);
        Singleton<SoundPlayer>.instance.playBGM("Wind", 0.0f, true);
        Singleton<SoundPlayer>.instance.playBGM("Water", 0.0f, true);

        //船が動けるようになる
        shipMoveFlag = true;

        yield return new WaitForSeconds(1.0f);

        //　非表示にする
        countDownText.text = "";
        countDownText.gameObject.SetActive(false);

    }
}
