using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaSound : BaseObject {


    private string audioClipName = "";
    [SerializeField]
    private float fadeTime = 1.0f;
    private bool playSound = true;

    private bool callOnce = false;

    void Start() {
        //fadeTime = 1.0f;
        Singleton<SoundPlayer>.instance.PlayBGM("Sea", fadeTime, true);

    }

    public override void OnUpdate() {
        base.OnUpdate();

        Singleton<SoundPlayer>.instance.Update();

    }

    public void OnTap() {
        Singleton<SoundPlayer>.instance.StopBGM(fadeTime);

        if(!callOnce) {
            Singleton<SoundPlayer>.instance.PlaySE("Bottun");
            callOnce = !callOnce;
        }
    }

    public void PauseBGM() {
        
        Singleton<SoundPlayer>.instance.Pause(playSound);

        Debug.Log(playSound);
        playSound = !playSound;

    }

}