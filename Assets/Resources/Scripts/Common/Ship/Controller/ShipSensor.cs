/***********************************************************************/
/*! @file   ShipSensor.cs
*************************************************************************
*   @brief  傾きセンサーで船を操作するコントローラ
*************************************************************************
*   @author yuta takatsu
*************************************************************************
*   Copyright © 2017 yuta takatsu All Rights Reserved.
************************************************************************/
using UnityEngine;
using System.Collections;

public class ShipSensor : BaseObject
{

    private Vector3 acceleration; // @brief センサー情報を取得

    private GUIStyle labelStyle; // @brief フォント

    void Start()
    {
        //フォント生成
        this.labelStyle = new GUIStyle();
        this.labelStyle.fontSize = Screen.height / 22;
        this.labelStyle.normal.textColor = Color.white;
    }
    public override void OnUpdate()
    {
        if (Singleton<GameInstance>.Instance.IsShipMove)
        {
            base.OnUpdate();
            this.acceleration = Input.acceleration * 2f;

            transform.Rotate(0, this.acceleration.x * 1.5f, 0);
        }
    }
    /// <summary>
    /// @brief センサー情報を取得し対象の船を傾ける
    /// </summary>
    //private void OnGUI()
    //{
    //    if (acceleration != null)
    //    {
    //        float x = Screen.width / 10;
    //        float y = 0;
    //        float w = Screen.width * 8 / 10;
    //        float h = Screen.height / 20;

    //        for (int i = 0; i < 3; i++)
    //        {
    //            y = Screen.height / 10 + h * i;
    //            string text = string.Empty;

    //            switch (i)
    //            {
    //                case 0://X
    //                    text = string.Format("accel-X:{0}", System.Math.Round(this.acceleration.x, 3));
    //                    break;
    //                case 1://Y
    //                    text = string.Format("accel-Y:{0}", System.Math.Round(this.acceleration.y, 3));
    //                    break;
    //                case 2://Z
    //                    text = string.Format("accel-Z:{0}", System.Math.Round(this.acceleration.z, 3));
    //                    break;
    //                default:
    //                    throw new System.InvalidOperationException();
    //            }

    //            /// テキストの更新
    //            GUI.Label(new Rect(x, y, w, h), text, this.labelStyle);
    //        }
    //    }
}
