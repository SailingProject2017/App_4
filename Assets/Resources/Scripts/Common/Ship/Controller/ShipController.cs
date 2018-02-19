﻿/***********************************************************************/
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

    bool flag;

    public void Start()
    {
        flag = false;
    }
    public override void OnUpdate()
    {
        if (flag)
        {
            transform.position += transform.forward * -0.2f;
        }
        if (Input.GetKey("a"))
        {
            flag = true;
        }


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