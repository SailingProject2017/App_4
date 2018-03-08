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

    private float moveSpeed = 20.0f; // @brief プレイヤーの進むスピード
    Ray ray; // @brief レイの宣言
   

    private void Start()
    {
        ray = new Ray(transform.position, transform.forward);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (Singleton<GameInstance>.instance.IsShipMove)
        {

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

            RaycastHit hitObject; // @brief レイが当たったオブジェクトを格納
            // レイがオブジェクトに当たっていなければまっすぐ進む
            if (Physics.Raycast(ray,out hitObject, 10.0f))
            {
                Debug.Log("hit!!");
                moveSpeed = 0.0f;
            }
            else
            {
                
            }
        }
    }
}