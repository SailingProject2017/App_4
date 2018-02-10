/**********************************************************************************************/
/*@file   BaseObject.cs
*********************************************************************************************
* @brief      すべてのオブジェクトを管理するための基底クラス
*********************************************************************************************
* @author     Ryo Sugiyama
*********************************************************************************************
* Copyright © 2017 Ryo Sugiyama All Rights Reserved.
**********************************************************************************************/
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : BaseObject {

    private Slider slider;

	void Start()
    {
        slider = GetComponent<Slider>();

        BaseObjectSingleton<GameInstance>.Instance.MaxBGMVolume = BaseObjectSingleton<GameInstance>.Instance.MaxSEVolume = slider.value;   
    }

    /// <summary>
    /// @brief BGM値が変化した際に呼ばれる関数
    /// </summary>
    public void BGMValueChanged()
    {
        BaseObjectSingleton<GameInstance>.Instance.MaxBGMVolume = slider.value / 6;
        Singleton<SoundPlayer>.instance.playBGM("0", 0.0f, false);
    }

    /// <summary>
    /// @brief SE値が変化した際に呼ばれる関数
    /// </summary>
    public void SEValueChanged()
    {
            BaseObjectSingleton<GameInstance>.Instance.MaxSEVolume = slider.value / 6;
            Singleton<SoundPlayer>.instance.playSE("0");       
    }
}
