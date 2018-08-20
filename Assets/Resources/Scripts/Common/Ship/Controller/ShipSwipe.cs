/***********************************************************************/
/*! @file   ShipSwipe.cs
*************************************************************************
*   @brief  スワイプでで船を操作するコントローラ
*************************************************************************
*   @author yuta takatsu
*************************************************************************
*   Copyright © 2017 yuta takatsu All Rights Reserved.
************************************************************************/
using UnityEngine;
using System.Collections;

public class ShipSwipe : BaseObject
{

    [SerializeField]
    private GameObject obj;


    private Vector2 pos;   // @brief タッチした座標
    private Quaternion rot;// @brief タッチしたときの回転
    private float wid, hei, diag;  // @brief スクリーンサイズ
    private float touchX, touchY; // @brief タッチされた座標を格納

    protected override void OnAwake()
    {
        base.OnAwake();
        wid = Screen.width;
        hei = Screen.height;
        diag = Mathf.Sqrt(Mathf.Pow(wid, 2) + Mathf.Pow(hei, 2));
    }


    public override void OnUpdate()
    {
        if (BaseObjectSingleton<GameInstance>.Instance.IsSwipe)
        {
            if (Singleton<GameInstance>.Instance.IsShipMove)
            {
                if (Input.touchCount == 1)
                {
                    //回転
                    Touch t1 = Input.GetTouch(0);
                    if (t1.phase == TouchPhase.Began)
                    {
                        pos = t1.position;
                        rot = obj.transform.rotation;
                    }
                    else if (t1.phase == TouchPhase.Moved || t1.phase == TouchPhase.Stationary)
                    {
                        touchX = (t1.position.x - pos.x) / wid; //横移動量(-1<tx<1)

                        obj.transform.rotation = rot;
                        obj.transform.Rotate(new Vector3(45 * touchY, 45 * touchX, 0));

                    }
                }
            }
        }
    }
}