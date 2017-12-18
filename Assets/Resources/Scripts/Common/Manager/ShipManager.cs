using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : BaseObject {

    // 船の状態
    public enum EShipState
    {
        STOP, // 止まっている
        START // 動いている
    }

    // 船の操作方法
    public enum EShipController
    {
        SWIPE, // スワイプ操作
        TILT,  // 傾き操作
    }

    // カメラ視点
    public enum EShipCameraMode
    {
        FPS, // 一人称
        TPS  // 三人称
    }

    private int HitMarker;


    // 船のリストを作成
    private List<GameObject> ShipList = new List<GameObject>();

    

}
