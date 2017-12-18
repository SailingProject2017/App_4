using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class BGMPlayer
{
    #region  状態遷移（State）を内部に持って管理するもの
    // State
    class State
    {
        protected BGMPlayer bgmPlayer;
        public State(BGMPlayer bgmPlayer)
        {
            this.bgmPlayer = bgmPlayer;
        }
        public virtual void playBGM() { }
        public virtual void pauseBGM() { }
        public virtual void stopBGM() { }
        public virtual void update() { }
    }


    #endregion

    #region 一時停止の実装
    class Wait : State
    {

        public Wait(BGMPlayer bgmPlayer) : base(bgmPlayer) { }

        public override void playBGM()
        {
            if (bgmPlayer.fadeInTime > 0.0f)
                bgmPlayer.state = new FadeIn(bgmPlayer);
            else
                bgmPlayer.state = new Playing(bgmPlayer);
        }
    }

    #endregion

    #region ポーズの実装
    class Pause : State
    {

        State preState;

        public Pause(BGMPlayer bgmPlayer, State preState) : base(bgmPlayer)
        {
            this.preState = preState;
            bgmPlayer.source.Pause();
        }

        public override void stopBGM()
        {
            bgmPlayer.source.Stop();
            bgmPlayer.state = new Wait(bgmPlayer);
        }

        public override void playBGM()
        {
            bgmPlayer.state = preState;
            bgmPlayer.source.Play();
        }
    }
    #endregion

    #region    再生の実装
    class Playing : State
    {

        public Playing(BGMPlayer bgmPlayer) : base(bgmPlayer)
        {
            if (bgmPlayer.source.isPlaying == false)
            {
                bgmPlayer.source.volume = 1.0f;
                bgmPlayer.source.Play();
            }
        }

        public override void pauseBGM()
        {
            bgmPlayer.state = new Pause(bgmPlayer, this);
        }

        public override void stopBGM()
        {
            bgmPlayer.state = new FadeOut(bgmPlayer);
        }
    }
    #endregion　

    #region    フェードインの実装
    class FadeIn : State
    {

        float t = 0.0f;

        public FadeIn(BGMPlayer bgmPlayer) : base(bgmPlayer)
        {
            bgmPlayer.source.Play();
            bgmPlayer.source.volume = 0.0f;
        }

        public override void pauseBGM()
        {
            bgmPlayer.state = new Pause(bgmPlayer, this);
        }

        public override void stopBGM()
        {
            bgmPlayer.state = new FadeOut(bgmPlayer);
        }

        public override void update()
        {
            t += Time.deltaTime;
            bgmPlayer.source.volume = t / bgmPlayer.fadeInTime;
            if (t >= bgmPlayer.fadeInTime)
            {
                bgmPlayer.source.volume = 1.0f;
                bgmPlayer.state = new Playing(bgmPlayer);
            }
        }
    }
    #endregion

    #region    フェードアウトの実装
    class FadeOut : State
    {
        float initVolume;
        float t = 0.0f;

        public FadeOut(BGMPlayer bgmPlayer) : base(bgmPlayer)
        {
            initVolume = bgmPlayer.source.volume;
        }

        public override void pauseBGM()
        {
            bgmPlayer.state = new Pause(bgmPlayer, this);
        }

        public override void update()
        {
            t += Time.deltaTime;
            bgmPlayer.source.volume = initVolume * (1.0f - t / bgmPlayer.fadeOutTime);
            if (t >= bgmPlayer.fadeOutTime)
            {
                bgmPlayer.source.volume = 0.0f;
                bgmPlayer.source.Stop();
                bgmPlayer.state = new Wait(bgmPlayer);
            }
        }
    }
    #endregion

    #region オブジェクトの宣言

    GameObject obj;
    AudioSource source;
    State state;
    float fadeInTime = 0.0f;
    float fadeOutTime = 0.0f;
    #endregion

    #region 静的関数の実装
    public BGMPlayer() { }

    public BGMPlayer(string bgmFileName)
    {
        AudioClip clip = (AudioClip)Resources.Load(bgmFileName);
        if (clip != null)
        {
            obj = new GameObject("BGMPlayer");
            source = obj.AddComponent<AudioSource>();
            source.clip = clip;
            state = new Wait(this);
        }
        else
            Debug.Log("BGM " + bgmFileName + " is not found.");
    }

    public void destory()
    {
        if (source != null)
            GameObject.Destroy(obj);
    }

    public void playBGM()
    {
        if (source != null)
        {
            state.playBGM();
        }
    }

    public void playBGM(float fadeTime, bool toLoop)
    {
        if (source != null)
        {
            this.fadeInTime = fadeTime;
            source.loop = toLoop;
            state.playBGM();
        }
    }

    public void pauseBGM()
    {
        if (source != null)
            state.pauseBGM();
    }

    public void stopBGM(float fadeTime)
    {
        if (source != null)
        {
            fadeOutTime = fadeTime;
            state.stopBGM();
        }
    }

    public void update()
    {
        if (source != null)
            state.update();
    }
    #endregion
}