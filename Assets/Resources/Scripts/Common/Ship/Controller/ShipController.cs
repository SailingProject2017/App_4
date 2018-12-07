/***********************************************************************/
/*! @file   ShipController.cs
*************************************************************************
*   @brief  キーボードで船を操作するコントローラ(デバッグ用)
*************************************************************************
*   @author yuta takatsu
*************************************************************************
*   Copyright © 2017 yuta takatsu All Rights Reserved.
************************************************************************/
using UnityEngine;

public class ShipController : PlayerBase
{
    [SerializeField]
    private float moveSpeed; // @brief プレイヤーの進むスピード

    public float Speed
	{
		set { moveSpeed = value; }
		get { return moveSpeed; }
	}


	public override void Initialize()
	{
		base.Initialize();
        Singleton<ShipStates>.Instance.ShipState = eShipState.START;
        Singleton<GameInstance>.Instance.IsShipMove = false;
    }

	public override void UpdateByFrame()
	{
		base.UpdateByFrame();
	}

	public override void UpdateByFixed()
    {
		base.UpdateByFixed();


        if(Singleton<ShipStates>.Instance.ShipState == eShipState.STOP)
        {
			gameObject.transform.position -= gameObject.transform.forward * moveSpeed * Time.deltaTime;
        }

        if (Singleton<GameInstance>.Instance.IsShipMove)
        {
            Singleton<ShipStates>.Instance.ShipState = eShipState.START;
            // 移動
            if (Input.GetKey("right"))
            {
				gameObject.transform.Rotate(0, 0.1f, 0);
            }
            if (Input.GetKey("left"))
            {
				gameObject.transform.Rotate(0, -0.1f, 0);
            }
			gameObject.transform.position -= gameObject.transform.forward * moveSpeed * Time.deltaTime;

           
        }
    }
}