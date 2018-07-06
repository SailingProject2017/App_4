/**********************************************************************************************/
/*@file       ResultOpen.cs
*********************************************************************************************
* @brief      船のカメラ視点を変えるクラス
*********************************************************************************************
* @author     Yuta Takatsu
*********************************************************************************************
* Copyright © 2018 Yuta Takatsu All Rights Reserved.
**********************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCamera : BaseObject
{

    /// <summary>
    ///  @brief 視点変更ボタンに入れる
    ///</summary>
    /// <param name="cameraMode">変更したい視点</param>
    public void OnTap(int cameraMode)
    {
        // FPS
        if (cameraMode == 0)
            Singleton<ShipStates>.instance.CameraMode = eCameraMode.FPS;
        // TPS
        else if (cameraMode == 1)
            Singleton<ShipStates>.instance.CameraMode = eCameraMode.TPS;

    }

}
