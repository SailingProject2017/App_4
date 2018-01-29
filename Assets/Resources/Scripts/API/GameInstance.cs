/**********************************************************************************************/
/*@file       GameInstance.cs
*********************************************************************************************
* @brief      すべてのオブジェクトを管理するための基底クラス
*********************************************************************************************
* @author     Ryo Sugiyama
*********************************************************************************************
* Copyright © 2017 Ryo Sugiyama All Rights Reserved.
**********************************************************************************************/

public class GameInstance : BaseObjectSingleton<GameInstance> {

    private EStageType stageType;   // @brief ステージタイプを格納する変数
    private bool isShipMove;        // @brief 船が動けるかどうかの状態を格納する変数

    protected override void AppendListConstructor()
    {
        base.AppendListConstructor();
        StageType = EStageType.Null;
        isShipMove = false;
    }

    /// <summary>
    /// @brief 船が動けるかどうかの状態を変化させる関数
    /// </summary>
    /// <param name="active"></param>
    public void ShipSetActive(bool active)
    {
        isShipMove = active;
    }

    /// <summary>
    /// @brief 変数アクセサー
    /// </summary>
    public bool IsShipMove
    {
        get { return isShipMove; }
    }


    /// <summary>
    /// @brief  読み込むステージを判断する変数のアクセサー
    /// @set    none
    /// @get    入力されたデータを渡す
    /// </summary>
    public EStageType StageType
    {
        set { stageType = value; }
        get { return stageType; }
    }

}
