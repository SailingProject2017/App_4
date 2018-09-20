/***********************************************************************/
/*! @file   ClearTime.cs
*************************************************************************
*   @brief  クリア時間を表示する
*************************************************************************
*   @author Tsuyoshi Takaguchi
*************************************************************************
*   Copyright © 2018 Tsuyoshi Takaguchi All Rights Reserved.
************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearTime : BaseObject {

    [SerializeField]
    private Sprite[] numSprite; // @brief 画像を格納する配列

    [SerializeField]
    private GameObject clearTimeImage; // @brief 画像を表示するオブジェクトを格納する

    private TimeManager timeManager; // @brief timeManagerObjectのスクリプトを格納する
    private int[] imageNum; // @brief 表示する画像の番号
    private int ii; // @brief 配列を参照するための変数
    private int clearTime; // @brief クリア時間を格納する変数

    /// <summary>
    /// @brief 初期化処理
    /// </summary>
    private void ClearTimeInitialize()
    {
        timeManager = GameObject.Find("TimeManager").GetComponent<TimeManager>();
        imageNum = new int[7];
        clearTime = (int)(timeManager.MillTime * 1000);
        ii = 0;
        Debug.Log((int)timeManager.MillTime);
        Debug.Log(clearTime);
    }

    /// <summary>
    /// @brief クリア時間の格納
    /// </summary>
    private void SetClearTime()
    {
        // 6桁の秒単位の記録と固定位置に「：」をセット
        for(ii=0;ii<7;ii++)
        {
            if (ii == 3)
            {
                imageNum[ii] = 10;
                continue;
            }
            imageNum[ii] = (clearTime % 10);
            clearTime = (clearTime / 10);
            Debug.Log(clearTime);
            Debug.Log("clearTime");
            if (clearTime == 0) break;
        }
    }

    /// <summary>
    /// @brief クリア時間の描画
    /// </summary>
    private void ClearTimeRender()
    {
        for (ii = 0; ii < 7; ii++)
        {
            //複製
            RectTransform timeImage = (RectTransform)New(clearTimeImage).transform;
            timeImage.SetParent(this.transform, false);
            timeImage.localPosition = new Vector2(
                timeImage.localPosition.x - timeImage.sizeDelta.x * ii,
                timeImage.localPosition.y);
            timeImage.GetComponent<Image>().sprite = numSprite[imageNum[ii]];
            Debug.Log(timeManager.MillTime);
        }
    }

    private void Start()
    {
        ClearTimeInitialize();
        SetClearTime();
        ClearTimeRender();
    }
}
