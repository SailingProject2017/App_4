/***********************************************************************/
/*! @file   TitleShipAnimation.cs
*************************************************************************
*   @brief  風と進行方向に対して最適な帆の角度を算出して移動させるクラス
*************************************************************************
*   @author Ryo Sugiyama
*************************************************************************
*   Copyright © 2017 Ryo Sugiyama All Rights Reserved.
************************************************************************/
using UnityEngine;

public class SailControl : BaseObject {

    [SerializeField]
    private GameObject player;          // @brief 船オブジェクトを格納する変数
    [SerializeField]
    private GameObject sail;            // @brief 船のセールオブジェクトを格納する変数

    private GetWindParam getWindParam;  // @brief スクリプトインスタンス

    private void Start()
    {
        //　風のベクトルをランダムで取得
        getWindParam.Random();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        SailRotate(getWindParam.Valuewind, player.transform.localEulerAngles.y);     
    }

    /// <summary>
    /// @brief 風と進行方向に対して最適な帆の角度を算出して移動させる
    /// </summary>
    /// <param name="windRotate"> 風のベクトルの方向 </param>
    /// <param name="playerRotate"> プレイヤーが進行しているベクトルの方向 </param>
    private void SailRotate(float windRotate, float playerRotate)
    {
        float sailRotateY;  // @brief セールが最高速を得られる角度を格納する変数

        // 正の数のとき
        if (windRotate > 0)
            sailRotateY = ((playerRotate - windRotate) / 2) - 180;

        // 負の数のとき
        else if (windRotate < 0)
            sailRotateY = ((playerRotate - windRotate) / 2) + 180;

        // 同じとき
        else
            return;

        //　現在のセールの位置と次のセールの角度を引く
        float sailMoveDirection = sail.transform.localEulerAngles.y - sailRotateY;

        //　正の数だったら負の方向に動かし、負の数だったら正の方向に動かす。
        if (sailMoveDirection > 0)
            sail.transform.Rotate(0, -2, 0);
        else if(sailMoveDirection < 0)
            sail.transform.Rotate(0, 2, 0);

    }
}
