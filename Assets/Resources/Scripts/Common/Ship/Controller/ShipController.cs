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

    public void Start()
    {
        
    }
    public override void OnUpdate()
    {
        

        if (Input.GetKey("right"))
        {
            transform.Rotate(0, 1, 0);
        }
        if (Input.GetKey("left"))
        {
            transform.Rotate(0, -1, 0);
        }
    }
}