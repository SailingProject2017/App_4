using System;

[Serializable()]
public class ShipStates
{

    private eCameraMode cameraPerspective; // @brief 視点の状態を保持

    /// <summary>
    /// @brief  視点の状態を保持する変数のアクセサー
    /// @set    現在の状態を設定
    /// @get    現在の設定を出力
    /// </summary>
    public eCameraMode CameraMode
    {
        set { cameraPerspective = value; }
        get { return cameraPerspective; }
    }
}

#region 列挙体の宣言

// 船の状態
public enum eShipState
{
    STOP, // 止まっている
    START // 動いている
}

// 船の操作方法
public enum eShipController
{
    SWIPE, // スワイプ操作
    TILT,  // 傾き操作
}

/// <summary>
/// @brief 視点を表す列挙
/// </summary>
public enum eCameraMode
{
    FPS,
    TPS,
    GOAL
}
#endregion