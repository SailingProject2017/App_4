/**********************************************************************************************/
/*@file       GetWindParam.cs
*********************************************************************************************
* @brief      風のベクトルを制御するクラス
*********************************************************************************************
* @author     Reina Sawai and Ryo Sugiyama
*********************************************************************************************
* Copyright © 2017 Reina Sawai All Rights Reserved.
**********************************************************************************************/
using System;
using UnityEngine;
using System.Collections;

public class GetWindParam : BaseObject
{
    private float valueWind;    //@brief 風の方向
    private float timeWind;     //@brief 風向きを変化させるタイマー
    void Start()
    {
        /*******************************************************
          数値のランダム化は完了。
          矢印との連携をさせる。
          連携が終わり次第、何秒後かに数字を変化させる
          数値で近いほうから周る
         *******************************************************/
        valueWind = 0;
        valueWind += UnityEngine.Random.Range(-180,180);
        Debug.Log(valueWind);

        //360度にあわせて条件指定
        if (valueWind==0)
        {
            transform.Rotate(new Vector3(0f, 0f, 0f)); // y軸を軸として90°回転
            Debug.Log("0か360だな！!(=ﾟωﾟ)ﾉ");
        }
        if (valueWind > 1&&valueWind < 45)
        {
            transform.Rotate(new Vector3(0f, 45f, 0f));
            Debug.Log("右上！( ﾟДﾟ)");
        }
        if (valueWind > 46 && valueWind < 90)
        {
            Debug.Log("右！！");
            transform.Rotate(new Vector3(0f, 90f, 0f));
        }
        if (valueWind > 91 && valueWind < 135)
        {
            transform.Rotate(new Vector3(0f, 135f, 0f));
            Debug.Log("右下！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！");
        }
        if (valueWind > 136 && valueWind < 180||valueWind>-180&&valueWind<-135)
        {
            transform.Rotate(new Vector3(0f, 180f, 0f));
            Debug.Log("下！！！！！！！！！！！！！！！！！(ﾟ∀ﾟ)ｱﾋｬ");
        }
        //ここから指定してない
        if (valueWind > 181 && valueWind < 225)
        {
            transform.Rotate(new Vector3(0f, 225f, 0f));
            Debug.Log("左下aaaaaaaaaaaaa_(:3」∠)_");
        }
        if (valueWind > 226 && valueWind < 270)
        {
            transform.Rotate(new Vector3(0f, 270f, 0f));
            Debug.Log("左＼(゜ロ＼)ココハドコ? (／ロ゜)／アタシハダアレ?");
        }
        if (valueWind > 271 && valueWind < 315)
        {
            transform.Rotate(new Vector3(0f, 315f, 0f));
            Debug.Log("左上(´・ω・)");
        }
        if (valueWind > 316 && valueWind < 359)
        {
            transform.Rotate(new Vector3(0f, 0f, 0f));
            Debug.Log("ｗｗｗっわああああああああああああああああああああああああ");
        }
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        timeWind -= Time.deltaTime;
        if (timeWind <= 0.0)
        {
            timeWind = 1.0f;
        }

    }

    /// <summary>
    /// @brief 風向きを出すアクセサー
    /// </summary>
    public float ValueWind
    {
        get { return valueWind; }
        set
        {
            // 風向きを0~360の中に指定する
            if (value > 180)
            {
                valueWind = value - 180;
            }
            else if (value < -180)
            {
                valueWind = 180 - value;
            }
            else
            {
                valueWind = value;
            }

        }
    }
}
