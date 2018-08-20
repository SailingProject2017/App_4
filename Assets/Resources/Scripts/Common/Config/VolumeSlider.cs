/**********************************************************************************************/
/*@file   VolumeSlider.cs
*********************************************************************************************
* @brief      音量調整のスライダーを実装するクラス
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
        BaseObjectSingleton<GameInstance>.Instance.MaxBGMVolume = BaseObjectSingleton<GameInstance>.Instance.MaxSEVolume = slider.value * 0.1f;
        Singleton<SoundPlayer>.instance.PauseBGM();
        Singleton<SoundPlayer>.instance.PlayBGM();
    }
    /// <summary>
    /// @brief SE値が変化した際に呼ばれる関数
    /// </summary>
    public void SEValueChanged()
    {
        Singleton<SoundPlayer>.instance.PlaySE("PassedMarker");
    }


}
