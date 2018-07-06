/**********************************************************************************************/
/*! @file     ShipStartupMove.cs
*********************************************************************************************
* @brief      InGameでの船の発進時のセットアップ
*********************************************************************************************
* @author     Yuta Takatsu
*********************************************************************************************
* Copyright © 2018 yuta takatsu All Rights Reserved.
**********************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStartupMove : BaseObject {

    private bool isCallOnce = false; // @brief 一度だけ呼ばれることを保証する

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (!isCallOnce)
        {
            StartCoroutine(ShipSetup());
            isCallOnce = !isCallOnce;
        }

    }


    /// <summary>
    /// @brief 読み込み中の空白時間を埋めるコルーチン
    /// </summary>
    public IEnumerator ShipSetup()
    {

        yield return new WaitForSeconds(5.0f);
        BaseObjectSingleton<GameInstance>.Instance.IsCountDown = true;

    }
}
