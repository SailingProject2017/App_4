/**************************************************************************************/
/*! @file   SoundPlayer.cs
***************************************************************************************
@brief      サウンドを出力するクラス
***************************************************************************************
@author     Ryo Sugiyama
***************************************************************************************/

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

    /****************************************************************************** 
    @brief      指定サウンドデータをオーディオクリップに追加する。
    @note       サウンドデータはResourceフォルダ直下においてください。
    @note       audioClips.Add("呼び出し時の名前", new AudioClipInfo("サウンドデータのフォルダ面", "呼び出し時の名前"));
    @return     none
    */
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

    /****************************************************************************** 
    @brief      追加したサウンドデータを再生する
    @return     指定のSE名がなければfalse / あれば再生してtrue
    */
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

    public void playBGM(string bgmName, float fadeTime, bool isLoop)
    {
        // destory old BGM
        if (fadeOutBGMPlayer != null)
            fadeOutBGMPlayer.destory();

        // change to fade out for current BGM
        if (curBGMPlayer != null)
        {
            curBGMPlayer.stopBGM(fadeTime);
            fadeOutBGMPlayer = curBGMPlayer;
        }

        // play new BGM
        if (audioClips.ContainsKey(bgmName) == false)
        {
            // null BGM
            curBGMPlayer = new BGMPlayer();
        }
        else
        {
            curBGMPlayer = new BGMPlayer(audioClips[bgmName].resourceName);
            curBGMPlayer.playBGM(fadeTime, isLoop);
        }
    }

    public void playBGM()
    {
        if (curBGMPlayer != null)
            curBGMPlayer.playBGM();
        if (fadeOutBGMPlayer != null)
            fadeOutBGMPlayer.playBGM();
    }

    public void pauseBGM()
    {
        if (curBGMPlayer != null)
            curBGMPlayer.pauseBGM();
        if (fadeOutBGMPlayer != null)
            fadeOutBGMPlayer.pauseBGM();
    }

    public void stopBGM(float fadeTime)
    {
        if (curBGMPlayer != null)
            curBGMPlayer.stopBGM(fadeTime);
        if (fadeOutBGMPlayer != null)
            fadeOutBGMPlayer.stopBGM(fadeTime);
    }
}