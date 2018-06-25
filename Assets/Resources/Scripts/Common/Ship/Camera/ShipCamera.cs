﻿/***********************************************************************/
/*! @file   ShipCamera.cs
*************************************************************************
*   @brief  船用のカメラ
*************************************************************************
*   @author yuta takatsu
*************************************************************************
*   Copyright © 2017 yuta takatsu All Rights Reserved.
************************************************************************/
using UnityEngine;
using System.Collections;

public class ShipCamera : BaseObject
{

    [SerializeField]
    private GameObject ship; // @brief 追跡する船
    [SerializeField]
    private Camera shipCamera;      // @brief 対象のカメラ
    
    private int layerMaskShip;  // @brief 船のレイヤー
    private float distance;     // @brief 船とカメラの距離
    private float cameraHeight; // @brief カメラの高さ
    private float followSpeed = 20; // @brief カメラのディレイスピード
    private Vector3 cameraOffset = Vector3.zero; // @brief カメラの位置の初期化
    
    void Start()
    {
        layerMaskShip = 1 << LayerMask.NameToLayer("Ship"); // レイヤー情報を取得

        if (Singleton<ShipStates>.instance.CameraMode == eCameraMode.FPS)
        {
            Camera.main.cullingMask &= ~layerMaskShip;// 非表示
            shipCamera.transform.SetPosY(1);
            shipCamera.transform.SetPosZ(0);
        }
        if (Singleton<ShipStates>.instance.CameraMode == eCameraMode.TPS)
        {
            Camera.main.cullingMask |= layerMaskShip; // 表示
            shipCamera.transform.SetPosY(7);
            shipCamera.transform.SetPosZ(9);
        }
        if (Singleton<ShipStates>.instance.CameraMode == eCameraMode.GOAL)
        {
            Camera.main.cullingMask |= layerMaskShip; // 表示
            shipCamera.transform.SetPosX(-3);
            shipCamera.transform.SetPosY(2);
            shipCamera.transform.SetPosZ(-5);
            shipCamera.transform.LookAt(ship.transform);
            
            // ゴール用カメラだけに使う固定用position
            cameraOffset = transform.position - ship.transform.position;
        }
        //平面(X,Z)での距離を取得
        distance = Vector3.Distance(
            new Vector3(ship.transform.position.x, 0, ship.transform.position.z),
            new Vector3(shipCamera.transform.position.x, 0, shipCamera.transform.position.z));

        //カメラの高さの差分を取得
        cameraHeight = shipCamera.transform.position.y - ship.transform.position.y;
    }

    public override void OnLateUpdate()
    {
        // ゴール用カメラ ディレイ無し
        if (Singleton<ShipStates>.instance.CameraMode == eCameraMode.GOAL)
        {
            Vector3 goalPosition = transform.position;
            goalPosition.x = ship.transform.position.x + cameraOffset.x;
            goalPosition.y = ship.transform.position.y + cameraOffset.y;
            goalPosition.z = ship.transform.position.z + cameraOffset.z;
            transform.position = goalPosition;
        }

        // レース中カメラ ディレイ有り
        else
        {
            //カメラの位置を高さだけ、ターゲットに合わせて作成
            var current = new Vector3(
                shipCamera.transform.position.x,
                ship.transform.position.y,
                shipCamera.transform.position.z
            );

            //チェック用の位置情報を作成(バックした時にカメラが引けるようにdistance分位置を後ろにずらす)
            var checkCurrent = current + Vector3.Normalize(current - ship.transform.position) * distance;

            //カメラが到達すべきポイントを計算（もともとのターゲットとの差分から計算します）
            var v = Vector3.MoveTowards(
                ship.transform.position,
                checkCurrent,
                distance);

            //カメラ位置移動(位置計算後にカメラの高さを修正）
            shipCamera.transform.position = Vector3.Lerp(
                current,
                v,
                Time.deltaTime * followSpeed
            ) + new Vector3(0, cameraHeight, 0);

            //カメラの角度を調整
            var newRotation = Quaternion.LookRotation(ship.transform.position - shipCamera.transform.position).eulerAngles;
            if (Singleton<ShipStates>.instance.CameraMode == eCameraMode.FPS)
            {
                newRotation.x = 0;
            }
            if (Singleton<ShipStates>.instance.CameraMode == eCameraMode.TPS)
            {
                newRotation.x = 20;
            }
            newRotation.z = 0;
            shipCamera.transform.rotation = Quaternion.Slerp(shipCamera.transform.rotation, Quaternion.Euler(newRotation), 1);
        }
    }
    /// <summary>
    ///  @brief 視点の変更時に呼ぶメソッド
    ///</summary>
    /// <param name="cameraMode">変更したい視点</param>
    private void ChangeCameraAngle()
    {

        if (Singleton<ShipStates>.instance.CameraMode == eCameraMode.FPS)
        {
            Camera.main.cullingMask &= ~layerMaskShip;// 非表示
            shipCamera.transform.SetPosY(1);
            shipCamera.transform.SetPosZ(0);
        }
        if (Singleton<ShipStates>.instance.CameraMode == eCameraMode.TPS)
        {
            Camera.main.cullingMask |= layerMaskShip; // 表示
            shipCamera.transform.SetPosY(7);
            shipCamera.transform.SetPosZ(9);
        }
        if(Singleton<ShipStates>.instance.CameraMode == eCameraMode.GOAL)
        {
            Camera.main.cullingMask |= layerMaskShip; // 表示
            shipCamera.transform.SetPosX(-3);
            shipCamera.transform.SetPosY(2);
            shipCamera.transform.SetPosZ(-5);
            shipCamera.transform.LookAt(ship.transform);
        }


    }
}