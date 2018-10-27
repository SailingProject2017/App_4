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
    
    private float windRotate;   // @brief スクリプトインスタンス
    
	private float sailRotate;

    private void Start()
    {
        //　風のベクトルをランダムで取得
		windRotate = Random.Range(-180, 180);
		windRotate = 0f;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
		SailRotate(windRotate, player.transform.localEulerAngles.y);     
    }

    /// <summary>
    /// @brief 風と進行方向に対して最適な帆の角度を算出して移動させる
    /// </summary>
	/// <param name="windVector"> 風のベクトルの方向 </param>
    /// <param name="playerRotate"> プレイヤーが進行しているベクトルの方向 </param>
    private void SailRotate(float windVector, float playerRotate)
    {


		if(playerRotate >= windVector + 45)
		{
			sailRotate = 10 + ((playerRotate - 45) * 0.5925f);
		}
		if (playerRotate <= windVector - 45)
        {
			sailRotate = -10 + ((playerRotate + 45) * 0.5925f);
        }
        
		sail.transform.eulerAngles = new Vector3(0, sailRotate, 0);
    }



    /* セールの角度を算出する計算式について */

    // 風向きを0°とする
	// 船が風向きに対して進める角度は、45° ~ 180° : -45° ~ -180° である。
    // 自艇の角度が45°の時、セールの角度は10°である
	// 自艇の角度が180°の時、セールの角度は90°である
    // この時、自艇の角度1°大きくなるたびにセールの角度は0.5925大きくなる
    //
    // よって、自艇の角度から、セールの角度を求める式は、
    // 自艇の角度をx、セールの角度をyとする
    //
    // 自艇の角度xが、x < 45 ではない時
	// y =  10 + ((x - 45) * 0.5925)
	// 自艇の角度xが、x < -45 ではない時
	// y = -10 + ((x + 45) * 0.5925)
    // となる


}
