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

    private Slider slider;  //@brief Sliderを格納する

	void Start()
    {
        // 初期化
        slider = GetComponent<Slider>();

        Singleton<SaveDataInstance>.Instance = (SaveDataInstance)CreateSaveData.LoadFromBinaryFile();

        if(gameObject.name == "SESlider")
		{
			slider.value = Singleton<SaveDataInstance>.Instance.MaxSEVolume * 10;
		}
		else
		{
			slider.value = Singleton<SaveDataInstance>.Instance.MaxBGMVolume * 10;
		}
	}

    /// <summary>
    /// @brief BGM値が変化した際に呼ばれる関数
    /// </summary>
    public void BGMValueChanged()
    {
        // 音量を変更
        Singleton<SaveDataInstance>.Instance.MaxBGMVolume = slider.value * 0.1f;
        // BGMの再生を再開
        Singleton<SoundPlayer>.Instance.PauseBGM();        
        Singleton<SoundPlayer>.Instance.PlayBGM();
        // 変更の保存
        CreateSaveData.SaveToBinaryFile(Singleton<SaveDataInstance>.Instance);
    }
    /// <summary>
    /// @brief SE値が変化した際に呼ばれる関数
    /// </summary>
    public void SEValueChanged()
    {
        // 音量を変更
        Singleton<SaveDataInstance>.Instance.MaxSEVolume = slider.value * 0.1f;
        // SEを鳴らす
        Singleton<SoundPlayer>.Instance.PlaySE("PassedMarker");
        // 変更の保存
        CreateSaveData.SaveToBinaryFile(Singleton<SaveDataInstance>.Instance);
    }


}
