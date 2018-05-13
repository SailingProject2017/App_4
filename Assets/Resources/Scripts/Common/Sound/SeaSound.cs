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
        Singleton<SoundPlayer>.instance.PlayBGM("Sea", fadeTime, true);
        Debug.Log("SeaSoundPlay!");
    }

    public void OnTap() {

        Singleton<SoundPlayer>.instance.StopBGM(fadeTime);
        Singleton<SoundPlayer>.instance.Update();
        Debug.Log("OnTap");

        if(!callOnce) {
            Singleton<SoundPlayer>.instance.PlaySE("Bottun");
            callOnce = !callOnce;
        }
    }

    public void PauseBGM() {

        if(playSound) {
            Singleton<SoundPlayer>.instance.PauseBGM();
        } else {
            Singleton<SoundPlayer>.instance.PlayBGM();
        }

        Debug.Log(playSound);
        playSound = !playSound;

    }

}