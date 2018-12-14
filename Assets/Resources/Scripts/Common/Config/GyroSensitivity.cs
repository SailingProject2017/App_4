/**********************************************************************************************/
/*@file   GyroSensitivity.cs
***********************************************************************************************
* @brief  感度のスライダーを実装するクラス
***********************************************************************************************
* @author Yuta Takatsu
***********************************************************************************************
* Copyright © 2018 Yuta Takatsu All Rights Reserved.
***********************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GyroSensitivity : BaseObject {

    private Slider slider;  // @brief Sliderを格納する

    /// <summary>
    /// @brief 初期化
    /// </summary>
    private void Start()
    {
        // Sliderを使用する準備
        slider = GetComponent<Slider>();

        Singleton<SaveDataInstance>.Instance = (SaveDataInstance)CreateSaveData.LoadFromBinaryFile();
        // 感度の初期化
        slider.value = Singleton<SaveDataInstance>.Instance.Sensitivty * 10;
    }

    /// <summary>
    /// @brief 感度が変化した際に呼ぶコールバック
    /// </summary>
    public void GyroSensitivityChange()
    {
        Singleton<SaveDataInstance>.Instance.Sensitivty = slider.value * 0.1f;
        CreateSaveData.SaveToBinaryFile(Singleton<SaveDataInstance>.Instance);
    }
}
