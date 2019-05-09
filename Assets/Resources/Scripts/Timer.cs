/**************************************************************************************/
/*! @file    Timer.cs
***************************************************************************************
* @brief     デバッグ用クラス。関数処理の計測に使います。
***************************************************************************************
* @author    Ryo Sugiyama
***************************************************************************************
* Copyright © 2018 Ryo Sugiyama All Rights Reserved.
***************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class Timer : BaseObject
{
    // 処理時間を測定する準備
    private static Stopwatch stopwatch = new Stopwatch();  // @brief Stopwatchクラスの格納

    /// <summary>
    /// @brief 現在の処理時間の合計をfloatでミリ秒単位に変換したもの
    /// @get   現在の処理時間(ミリ秒単位)をfloatで取得する
    /// </summary>
    public static float TotalMilliseconds
    {
        get
        {
            return (float)stopwatch.Elapsed.TotalMilliseconds;
        }
    }
    /// <summary>
    /// @brief 現在の処理時間の合計をfloatで秒単位に変換したもの
    /// @get   現在の処理時間(秒単位)をfloatで取得する
    /// </summary>
    public static float TotalSeconds
    {
        get
        {
            return (float)stopwatch.Elapsed.TotalSeconds;
        }
    }

    /// <summary>
    /// @brief 計測中かどうかのフラグ
    /// @get   フラグを取得する
    /// </summary>
    public static bool IsRunning 
    {
        get
        { 
            return stopwatch.IsRunning; 
        } 
    }

    /// <summary>
    /// @brief 現在の処理時間のミリ秒単位だけをintで抜き出したもの
    /// @get   現在の処理時間(ミリ秒単位)をintで取得する
    /// </summary>
    public static int Milliseconds {
        get 
        { 
            return stopwatch.Elapsed.Milliseconds; 
        }
    }

    /// <summary>
    /// @brief 現在の処理時間の秒単位だけをintで抜き出したもの
    /// @get   現在の処理時間(秒単位)をintで取得する
    /// </summary>
    public static int Seconds 
    { 
        get
        { 
            return stopwatch.Elapsed.Seconds; 
        } 
    }

    /// <summary>
    /// @brief 計測開始
    /// </summary>
    public static void Start()
    {
        stopwatch.Start();
    }

    /// <summary>
    /// @brief 計測終了
    /// </summary>
    /// <returns>計測された秒数</returns>
    public static float Stop()
    {
        stopwatch.Stop();
        return TotalMilliseconds;
    }

    /// <summary>
    /// @brief タイマーのリセット
    /// </summary>
    public static void Reset()
    {
        stopwatch.Reset();
    }

    /// <summary>
    /// @brief タイマーのリスタート
    /// </summary>
    public static void ReStart()
    {
        Reset();

        Start();
    }

}