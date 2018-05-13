/**********************************************************************************************/
/*@file       BGMPlayer.cs
*********************************************************************************************
* @brief      BGMを再生するためするためのクラス
*********************************************************************************************
* @author     Ryo Sugiyama
*********************************************************************************************
* Copyright © 2017 Ryo Sugiyama All Rights Reserved.
**********************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class BGMPlayer{

    #region オブジェクトの宣言
    /****************************************************************************************/
    GameObject obj;             // @brief 再生するBGMがコンポーネントされているゲームオブジェクト
    AudioSource source;         // @brief Unity.Engine空間のオーディオ関連
    State state;                // @brief 状態を格納するStateクラスインスタンス
    float fadeInTime = 0.0f;    // @brief フェードインタイム
    float fadeOutTime = 0.0f;   // @brief フェードアウトタイム
    float maxBGMVolume = 0.0f;  // @brief ボリューム

    #endregion

    #region  状態遷移（State）を内部に持って管理するもの
    /****************************************************************************************/
    class State {
        protected BGMPlayer bgmPlayer;  // @brief 子クラスで使用可能なインスタンス

        public State(BGMPlayer bgmPlayer) {
            this.bgmPlayer = bgmPlayer;
        }

        public virtual void PlayBGM() { }
        public virtual void PauseBGM() { }
        public virtual void StopBGM() { }
        public virtual void Update() { }
    }

    #endregion

    #region 一時停止の実装
    /****************************************************************************************/
    class Wait : State {
        //停止
        public Wait(BGMPlayer bgmPlayer) : base(bgmPlayer) {

            Debug.Log("now state Wait");

        }

        //再生
        public override void PlayBGM() {
            Debug.Log("fadeInTime = " + bgmPlayer.fadeInTime);

            if(bgmPlayer.fadeInTime > 0.0f) {
                bgmPlayer.state = new FadeIn(bgmPlayer);
            } else {
                bgmPlayer.state = new Playing(bgmPlayer);
            }
        }
    }

    #endregion

    #region ポーズの実装
    /****************************************************************************************/
    class Pause : State {

        State preState;

        public Pause(BGMPlayer bgmPlayer, State argPreState) : base(bgmPlayer) {

            Debug.Log("now state Pause");

            preState = argPreState;
            bgmPlayer.source.Pause();
            
        }

        public override void StopBGM() {
            bgmPlayer.source.Stop();
            bgmPlayer.state = new Wait(bgmPlayer);
        }

        public override void PlayBGM() {
            bgmPlayer.state = preState;
            bgmPlayer.source.Play();
        }

    }

    #endregion

    #region    再生の実装
    /****************************************************************************************/
    class Playing : State {

        public Playing(BGMPlayer bgmPlayer) : base(bgmPlayer) {
            Debug.Log("now state Playing");

            if(bgmPlayer.source.isPlaying == false) {
                bgmPlayer.source.volume = bgmPlayer.maxBGMVolume;
                bgmPlayer.source.Play();
                Debug.Log("bgmPlaeyr.Play");
            }
            
        }

        public override void PauseBGM() {
            bgmPlayer.state = new Pause(bgmPlayer, this);
        }

        public override void StopBGM() {
            Debug.Log("new FadeOut");
            bgmPlayer.state = new FadeOut(bgmPlayer);
        }
    }

    #endregion　

    #region    フェードインの実装
    /****************************************************************************************/
    class FadeIn : State {

        private float time = 0.0f;

        public FadeIn(BGMPlayer bgmPlayer) : base(bgmPlayer) {

            Debug.Log("now state FadeIn");

            bgmPlayer.source.Play();
            bgmPlayer.source.volume = 0.0f;
           
        }

        public override void PauseBGM() {
            bgmPlayer.state = new Pause(bgmPlayer, this);
        }

        public override void StopBGM() {
            bgmPlayer.state = new FadeOut(bgmPlayer);
        }

        public override void Update() {
            Debug.Log("フェードイン");

            time += Time.deltaTime;

            bgmPlayer.source.volume = time / bgmPlayer.fadeInTime;

            if(time >= bgmPlayer.fadeInTime) {
                bgmPlayer.source.volume = bgmPlayer.maxBGMVolume;
                bgmPlayer.state = new Playing(bgmPlayer);
                Debug.Log("new Playing");
            }
        }
    }
    #endregion

    #region    フェードアウトの実装
    /****************************************************************************************/
    class FadeOut : State {
        private float initVolume;
        private float time = 0.0f;

        public FadeOut(BGMPlayer bgmPlayer) : base(bgmPlayer) {

            Debug.Log("now state FadeOut");

            initVolume = bgmPlayer.source.volume;
            
        }

        public override void PauseBGM() {
            bgmPlayer.state = new Pause(bgmPlayer, this);
        }

        public override void Update() {
            time += Time.deltaTime;

            bgmPlayer.source.volume = initVolume * (bgmPlayer.maxBGMVolume - time / bgmPlayer.fadeOutTime);
            Debug.Log("volume = " + bgmPlayer.source.volume);

            if(time >= bgmPlayer.fadeOutTime) {
                bgmPlayer.source.volume = 0.0f;
                bgmPlayer.source.Stop();
                bgmPlayer.state = new Wait(bgmPlayer);
            }
        }
    }
    #endregion

    #region 静的関数の実装
    /****************************************************************************************/
    public BGMPlayer() { }

    public BGMPlayer(string bgmFileName) {
        AudioClip clip = (AudioClip)Resources.Load(bgmFileName);

        if(clip != null) {
            //BaseObjectを継承していたせいでnullになっていた
            obj = new GameObject("BGMPlayer");
            source = obj.AddComponent<AudioSource>();
            source.clip = clip;
            state = new Wait(this);
        } else {
            Debug.Log("BGM " + bgmFileName + " is not found.");
        }
    }

    public void destory() {
        if(source != null) {
            GameObject.Destroy(obj);
            Debug.Log("Destroy BGMObject");
        }
    }

    public void PlayBGM() {
        if(source != null) {
            state.PlayBGM();
        }
    }

    public void PlayBGM(float fadeTime, bool toLoop) {
        if(source != null) {
            this.fadeInTime = fadeTime;
            Debug.Log("set fadeInTime = " + fadeInTime);
            this.maxBGMVolume = BaseObjectSingleton<GameInstance>.Instance.MaxBGMVolume;
            source.volume = this.maxBGMVolume;
            Debug.Log("set Volume = " + source.volume);
            source.loop = toLoop;
            state.PlayBGM();
        }
    }

    public void PauseBGM() {
        if(source != null) {
            state.PauseBGM();
        }
    }

    public void StopBGM(float fadeTime) {
        if(source != null) {
            fadeOutTime = fadeTime;
            state.StopBGM();
        }
    }

    public void Update() {
        if(source != null) {
            state.Update();
        }
    }

    #endregion
}