﻿/**********************************************************************************************/
/*@file       GameInstance.cs
*********************************************************************************************
* @brief      保持する必要のあるすべてのクラスインスタンスを管理するためのクラス
*********************************************************************************************
* @author     Ryo Sugiyama
*********************************************************************************************
* Copyright © 2017 Ryo Sugiyama All Rights Reserved.
**********************************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance : BaseObjectSingleton<GameInstance>
{

    /* inGame関連 */
    private eStageType stageType; // @brief ステージタイプを格納する変数
    private bool isShipMove;   // @brief 船が動けるかどうかの状態を格納する変数
    private bool isPorse;      // @brief ポーズ中かどうか判別する変数
    private bool isCountDown;  // @brief カウントダウンを始めるかどうか判別する変数
    private bool isGoal;       // @brief ゴールしたかの確認

    /* Tutorial関連 */
    private bool isTutorialState; // @brief チュートリアルが変わったかどうかの確認

    /* サウンド関連 */
    private float maxBGMVolume;
    private float maxSEVolume;

    protected override void OnAwake()
    {
        base.OnAwake();
        stageType = eStageType.Null;
        isShipMove = false;
        isPorse = false;
        isCountDown = false;
        isGoal = false;
        isTutorialState = false;
        maxBGMVolume = 1.0f;
        maxSEVolume = 1.0f;

        Application.targetFrameRate = 30;

    }

    #region 読み込むステージの判定
    /********************************************************************************************/

    /// <summary>
    /// @brief  読み込むステージを判断する変数のアクセサー
    /// @set    none
    /// @get    入力されたデータを渡す
    /// </summary>
    public eStageType StageType
    {
        set { stageType = value; }
        get { return stageType; }
    }
    #endregion

    #region 船が動けるかの判定
    /********************************************************************************************/
   
    /// <summary>
    /// @brief isShipMoveの変数アクセサー
    /// </summary>
    public bool IsShipMove
    {
        set { isShipMove = value; }
        get { return isShipMove; }
    }
    #endregion

    #region ポーズ判定
    /********************************************************************************************/
 
    /// <summary>
    /// @brief isPorseの変数アクセサー
    /// </summary>
    public bool IsPorse
    {
        set { isPorse = value; }
        get { return isPorse; }
    }
    #endregion

    #region カウントダウンの判定
    /********************************************************************************************/

    /// <summary>
    /// @brief isCountDownの変数アクセサー
    /// </summary>
    public bool IsCountDown
    {
        set { isCountDown = value; }
        get { return isCountDown; }
    }

    #endregion

    #region ゴールの判定
    /********************************************************************************************/

    /// <summary>
    /// @brief isGoalの変数アクセサー
    /// </summary>
    public bool IsGoal
    {
        set { isGoal = value; }
        get { return isGoal; }
    }

    #endregion

    #region チュートリアルが変わったかどうかの確認
    /********************************************************************************************/

    /// <summary>
    /// @brief isTutorialStateの変数アクセサー
    /// </summary>
    public bool IsTutorialState
    {
        set { isTutorialState = value; }
        get { return isTutorialState; }
    }

    #endregion

    #region サウンド関連
    /********************************************************************************************/

    /// <summary>
    /// @brief maxBGMVolumeの変数アクセサー
    /// </summary>
    public float MaxBGMVolume
    {
        set
        {
            if (value < 0.0f || value > 1.0f)         
                return;
            maxBGMVolume = value;           
        }
        get { return maxBGMVolume; }
    }

    /// <summary>
    /// @brief maxSEVolumeの変数アクセサー
    /// </summary>
    public float MaxSEVolume
    {
        set
        {
            if (value < 0.0f || value > 1.0f)
                return;
            maxSEVolume = value;
        }
        get { return maxSEVolume; }
    }

    #endregion
}

public class Selectable<T>
{
    private T _value; // 選択中の値

    /// <summary>
    /// @brief 値を取得または設定をする
    /// @none  値の設定後にcahgedイベントが呼び出される
    /// </summary>
    /// <param></param>
    public T Value
    {
        get { return _value; }
        set
        {
            _value = value;
            OnChanged(_value);
        }
    }

    /// <summary>
    /// @brief 値が変更されたときに呼び出されます
    /// @none  イベントを使用しなかった時のコンパイル時の警告を抑制する
    /// </summary>
#pragma warning disable 0067
    public Action<T> changed;
#pragma warning restore 0067

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public Selectable()
    {
        _value = default(T);
    }

    /// <summary>
    /// @brief 値を設定
    /// @none  値の設定後にchangedイベントは呼び出されない
    /// </summary>
    public void SetValueWithoutCallback(T value)
    {
        _value = value;
    }

    /// <summary>
    /// @brief 値を設定
    /// @none 値が変更された場合のみchangedイベントを呼ぶ
    /// </summary>
    public void SetValueIfNotEqual(T value)
    {
        if (_value.Equals(value))
        {
            return;
        }
        _value = value;
        OnChanged(_value);
    }

    /// <summary>
    /// @brief 値が変更されたときに呼び出されます
    /// </summary>
    private void OnChanged(T value)
    {
        var onChanged = changed;
        if(onChanged == null)
        {
            return;
        }
        onChanged(value);
    }
}


