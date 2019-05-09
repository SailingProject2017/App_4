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
    private bool isPopup;      // @brief ポップアップを開くか閉じるか
    private bool isGyro;       // @brief ジャイロ操作のフラグ
    private bool isSwipe;      // @brief スワイプ操作のフラグ
    private int rank;          // @brief プレイヤーのランク
    private float sensitivty;  // @brief 加速度

    /* Tutorial関連 */
    private bool isTutorialState; // @brief チュートリアルが変わったかどうかの確認

    /* サウンド関連 */
    private float maxBGMVolume;  // @brief BGMの音量
    private float maxSEVolume;   // @brief SEの音量

    /// <summary>
    /// @brief BaseObjectの実装
    /// @note 初期化処理
    /// </summary>
    protected override void OnAwake()
    {
        base.OnAwake();
        stageType = eStageType.Null;
        isShipMove = false;
        isPorse = false;
        isCountDown = false;
        isGoal = false;
        isTutorialState = false;
        isPopup = false;
        rank = 0;


        // 解像度の変更　端末のアス比からアス比を計算
        float screenRate = (float)1024 / Screen.height;
        if (screenRate > 1) screenRate = 1;
        int width = (int)(Screen.width * screenRate);
        int height = (int)(Screen.height * screenRate);
        Screen.SetResolution(width, height, true, 15);

    }

    #region 読み込むステージの判定
    /********************************************************************************************/

    /// <summary>
    /// @brief  読み込むステージを判断する変数のアクセサー
    /// @set    読み込むステージタイプの更新
    /// @get    読み込むステージタイプの取得
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
    /// @set 船の動作状態を更新する
    /// @get 船の動作状態を取得する
    /// </summary>
    /// <value>
	/// <c> true  </c> 動ける
	/// <c> false </c> 動けない 
	/// </value>
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
    /// @set ポーズ状況を更新する
    /// @get ポーズ状況を取得する
    /// </summary>
    /// <value>
	/// <c> true  </c> ポーズしている
	/// <c> false </c> ポーズしていない 
	/// </value>
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
    /// @set カウントダウンの開始状況を更新する
    /// @get カウントダウンの開始状況を取得する
    /// </summary>
    /// <value>
	/// <c> true  </c> カウントダウンを始める
	/// <c> false </c> カウントダウンを始めない
	/// </value>
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
    /// @set ゴール状況の更新
    /// @get ゴール状況の取得
    /// </summary>
    /// <value>
	/// <c> true  </c> すでにゴールした
	/// <c> false </c> まだゴールしてない  
	/// </value>
    public bool IsGoal
    {
        set { isGoal = value; }
        get { return isGoal; }
    }

    #endregion

    #region ポップアップの判定
    /********************************************************************************************/

    /// <summary>
    /// @brief isPopupの変数アクセサー
    /// @set ポップアップの開閉状況を更新する
    /// @get ポップアップの開閉状況を取得する
    /// </summary>
    /// <value>
	/// <c> true  </c> ポップアップを開く
	/// <c> false </c> ポップアップを閉じる
	/// </value>
    public bool IsPopup
    {
        set { isPopup = value; }
        get { return isPopup; }
    }

    #endregion

    #region ジャイロ操作の判定
    /********************************************************************************************

    /// <summary>
    /// @brief isGyroの変数アクセサー
    /// </summary>
    public bool IsGyro
    {
        set { isGyro = value; }
        get { return isGyro; }
    }

    #endregion

    #region スワイプ操作の判定
    /********************************************************************************************

    /// <summary>
    /// @brief isSwipeの変数アクセサー
    /// </summary>
    public bool IsSwipe
    {
        set { isSwipe = value; }
        get { return isSwipe; }
    }

    #endregion

    #region ゴール時のランク
    /********************************************************************************************/

    /// <summary>
    /// @brief  プレイヤーのランクのアクセサ
    /// @set    プレイヤーのランクの更新
    /// @get    プレイヤーのランクの取得
    /// </summary>
    public int Rank
    {
        set { rank = value; }
        get { return rank; }
    }

    #endregion

    #region チュートリアルが変わったかどうかの確認
    /********************************************************************************************/

    /// <summary>
    /// @brief isTutorialStateの変数アクセサー
    /// @set チュートリアルの変更状況を更新する
    /// @get チュートリアルの変更状況を取得する
    /// </summary>
    /// <value>
	/// <c> true  </c> 変わった
	/// <c> false </c> 変わってない  
	/// </value>
    public bool IsTutorialState
    {
        set { isTutorialState = value; }
        get { return isTutorialState; }
    }

    #endregion


    #region 加速度
    /********************************************************************************************

    /// <summary>
    /// @brief isTutorialStateの変数アクセサー
    /// </summary>
    public float Sensitivty
    {
        set { sensitivty = value; }
        get { return sensitivty; }
    }

    #endregion
    #region サウンド関連
    /********************************************************************************************

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
    /********************************************************************************************/
    #endregion
}


public class Selectable<T>
{
    private T _value; // @brief 選択中の値

    /// <summary>
    /// @brief 値を取得または設定をする
    /// @note  値の設定後にcahgedイベントが呼び出される
    /// @set 値の設定
    /// @get 値の更新
    /// </summary>
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
    /// @note  イベントを使用しなかった時のコンパイル時の警告を抑制する
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
    /// @note  値の設定後にchangedイベントは呼び出されない
    /// </summary>
    public void SetValueWithoutCallback(T value)
    {
        _value = value;
    }

    /// <summary>
    /// @brief 値を設定
    /// @note 値が変更された場合のみchangedイベントを呼ぶ
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
        if (onChanged == null)
        {
            return;
        }
        onChanged(value);
    }

}
