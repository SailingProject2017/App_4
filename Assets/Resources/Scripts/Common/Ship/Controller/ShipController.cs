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

public class ShipController : BaseObject
{
    [SerializeField]
    private float moveSpeed = 20.0f; // @brief プレイヤーの進むスピード

    public void Start()
    {
        Singleton<ShipStates>.instance.ShipState = eShipState.START;
        Singleton<GameInstance>.instance.IsShipMove = false;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();


        if(Singleton<ShipStates>.instance.ShipState == eShipState.STOP)
        {
            transform.position -= transform.forward * moveSpeed * Time.deltaTime;
        }

        if (Singleton<GameInstance>.instance.IsShipMove)
        {
            Singleton<ShipStates>.instance.ShipState = eShipState.START;
            // 移動
            if (Input.GetKey("right"))
            {
                transform.Rotate(0, 1, 0);
            }
            if (Input.GetKey("left"))
            {
                transform.Rotate(0, -1, 0);
            }
            transform.position -= transform.forward * moveSpeed * Time.deltaTime;

           
        }
    }
}