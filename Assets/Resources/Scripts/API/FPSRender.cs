/**************************************************************************************/
/*! @file   FPSRender.cs
***************************************************************************************
* @brief    現在のFPSを表示する
***************************************************************************************
* @note     インスペクターから表示のON / OFFを変えられます
***************************************************************************************
* @author   Ryo Sugiyama
***************************************************************************************
* Copyright © 2017 Ryo Sugiyama All Rights Reserved.
***************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSRender : BaseObject
{
    [SerializeField]
    private float updateInterval = 0.5f; // @brief デバッグ用

    private float accum;    // @brief 経過時間
    private int frames;     // @brief 現在のフレーム数
    private float timeleft; // @brief 計測する時間
    private float fps;      // @brief 現在のFPS数
    private int allObj;     // @brief 現在のオブジェクト総数

    /// <summary>
    /// @brief 初期化処理
    /// </summary>
    public void Start()
    {
        allObj = 0;
    }

    /// <summary>
    /// @brief 更新処理
    /// </summary>
    public override void OnUpdate()
    {
        // 計測時間の更新
        timeleft -= Time.deltaTime;
        // 経過時間の更新
        accum += Time.timeScale / Time.deltaTime;
        // フレーム数を加算
        frames++;

        if (0 < timeleft) return;
        // FPSの更新
        fps = accum / frames;
        // 初期化
        timeleft = updateInterval;
        accum = 0;
        frames = 0;
        // オブジェクト総数の更新
        allObj = BaseObjectList.Count;
    }

    /// <summary>
    /// @brief 終了処理
    /// </summary>
    public override void OnEnd()
    {
        base.OnEnd();
        allObj = 0;

    }

    /// <summary>
    /// @brief 描画処理
    /// </summary>
    private void OnGUI()
    {
        // FPSとオブジェクト総数の表示
        GUILayout.Label("FPS: " + fps.ToString("f2")); // 表示は小数点第2位まで
        GUILayout.Label("Obj: " + allObj.ToString());
    }
}