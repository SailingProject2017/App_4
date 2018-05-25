/**********************************************************************************************/
/*@file       GameInstance.cs
*********************************************************************************************
* @brief      保持する必要のあるすべてのクラスインスタンスを管理するためのクラス
*********************************************************************************************
* @author     Ryo Sugiyama
*********************************************************************************************
* Copyright © 2017 Ryo Sugiyama All Rights Reserved.
**********************************************************************************************/

public class GameInstance : BaseObjectSingleton<GameInstance> {

    /* inGame関連 */
    private eStageType stageType = eStageType.Null;     // @brief ステージタイプを格納する変数
    private bool isShipMove  = false;                   // @brief 船が動けるかどうかの状態を格納する変数
    private bool isPorse     = false;                   // @brief ポーズ中かどうか判別する変数
    private bool isCountDown = false;                   // @brief カウントダウンを始めるかどうか判別する変数
    private bool isGoal      = false;                   // @brief ゴールしたかの確認
    
    /* サウンド関連 */
    private float maxBGMVolume = 1.0f;
    private float maxSEVolume = 1.0f;

    protected override void AppendListConstructor()
    {
        base.AppendListConstructor();
        StageType = eStageType.Null;

        maxBGMVolume = 1.0f;
        maxBGMVolume = 1.0f;
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
