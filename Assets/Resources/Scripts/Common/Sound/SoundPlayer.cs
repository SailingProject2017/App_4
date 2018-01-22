/**********************************************************************************************/
/*@file   BaseObject.cs
*********************************************************************************************
* @brief      サウンドを出力するクラス
*********************************************************************************************
* @author     Ryo Sugiyama
*********************************************************************************************
* Copyright © 2017 Ryo Sugiyama All Rights Reserved.
**********************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer
{

    GameObject soundPlayerObj;  // @brief サウンドプレイヤーオブジェクトを格納する変数
    AudioSource audioSource;    // @brief Unityのオーディオ設定関連のクラスインスタンス
    BGMPlayer curBGMPlayer;     // @brief 現在再生しているBGMのオブジェクトを格納する変数
    BGMPlayer fadeOutBGMPlayer; // @brief フェードアウト中のBGMのオブジェクトを格納する変数

    Dictionary<string, AudioClipInfo> audioClips = new Dictionary<string, AudioClipInfo>();
    
    /// <summary>
    /// オーディオクリップ関連の宣言とアクセサー
    /// </summary>
    class AudioClipInfo
    {
        public string resourceName;
        public string name;
        public AudioClip clip;

        public AudioClipInfo(string resourceName, string name)
        {
            this.resourceName = resourceName;
            this.name = name;
        }
    }

    /// <summary>
    /// @brief      指定サウンドデータをオーディオクリップに追加する。
    /// @note       サウンドデータはResourceフォルダ直下においてください。
    /// @note       audioClips.Add("呼び出し時の名前", new AudioClipInfo("サウンドデータのフォルダ名", "管理名"));
    /// @return     none
    /// </summary>
    public SoundPlayer()
    {
        audioClips.Add("Sea", new AudioClipInfo("Sound/Sea", "BGM001"));
        audioClips.Add("ModeSelect", new AudioClipInfo("Sound/Title", "BGM002"));
        audioClips.Add("Wind", new AudioClipInfo("Sound/wind", "BGM003"));
        audioClips.Add("Water", new AudioClipInfo("Sound/Water", "BGM004"));
        audioClips.Add("TT", new AudioClipInfo("Sound/TT", "BGM005"));
        audioClips.Add("Bottun", new AudioClipInfo("Sound/TitleBottun", "SE001"));
        audioClips.Add("Bottun2", new AudioClipInfo("Sound/Bottun2", "SE002"));
        audioClips.Add("0", new AudioClipInfo("Sound/0", "SE003"));
        audioClips.Add("4", new AudioClipInfo("Sound/4", "SE004"));

    }

    /// <summary>
    /// @brief      追加したサウンドデータを再生する
    /// </summary>
    /// <param name="seName"></param>
    /// <param name="volume"></param>
    /// <returns>指定のSE名がなければfalse / あれば再生してtrue</returns>
    public bool playSE(string seName, float volume)
    {

        if (audioClips.ContainsKey(seName) == false)
            return false;

        AudioClipInfo info = audioClips[seName];

        //なければロード
        if (info.clip == null)
            info.clip = (AudioClip)Resources.Load(info.resourceName);

        if (soundPlayerObj == null)
        {
            soundPlayerObj = new GameObject("SoundPlayer");
            audioSource = soundPlayerObj.AddComponent<AudioSource>();
        }

        //ボリュームの設定
        audioSource.volume = volume;
        audioSource.loop = false;
        //再生
        audioSource.PlayOneShot(info.clip);

        return true;
    }

    /// <summary>
    /// @brief 名前、フェードタイム、ループするか、ボリュームを決めてBGMを再生する
    /// </summary>
    /// <param name="bgmName"></param>
    /// <param name="fadeTime"></param>
    /// <param name="isLoop"></param>
    /// <param name="volume"></param>
    public void playBGM(string bgmName, float fadeTime, bool isLoop, float volume = 1.0f)
    {
        //　フェードアウト中のBGMがあったら
        if (fadeOutBGMPlayer != null)

            // もともと再生されていたBGMを消去
            fadeOutBGMPlayer.destory();

        //　再生中なら
        if (curBGMPlayer != null)
        {
            //　フェードアウトしながら、新しいBGMを入れる
            curBGMPlayer.stopBGM(fadeTime);
            fadeOutBGMPlayer = curBGMPlayer;
        }

        // サウンドのリストに登録されていなかったら
        if (audioClips.ContainsKey(bgmName) == false)
        {
            // 空のオブジェクトを生成
            curBGMPlayer = new BGMPlayer();
        }
        else
        {
            //指定された名前のBGMの実態を生成して転送
            curBGMPlayer = new BGMPlayer(audioClips[bgmName].resourceName);
            curBGMPlayer.playBGM(fadeTime, isLoop, volume);
        }
    }

    //　再生
    public void playBGM()
    {
        if (curBGMPlayer != null)
            curBGMPlayer.playBGM();
        if (fadeOutBGMPlayer != null)
            fadeOutBGMPlayer.playBGM();
    }

    //ポーズ
    public void pauseBGM()
    {
        if (curBGMPlayer != null)
            curBGMPlayer.pauseBGM();
        if (fadeOutBGMPlayer != null)
            fadeOutBGMPlayer.pauseBGM();
    }

    //　停止
    public void stopBGM(float fadeTime)
    {
        if (curBGMPlayer != null)
            curBGMPlayer.stopBGM(fadeTime);
        if (fadeOutBGMPlayer != null)
            fadeOutBGMPlayer.stopBGM(fadeTime);
    }
}