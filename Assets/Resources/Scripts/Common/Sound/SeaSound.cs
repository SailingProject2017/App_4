using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaSound : BaseObject {


    private string audioClipName = "";
    [SerializeField]
    private float fadeTime = 0.0f;


    private bool callOnce = false;

    void Start() {
        Singleton<SoundPlayer>.instance.playBGM("Sea", fadeTime, true);
        Debug.Log("SeaSound");
    }

    public void OnTap() {
        if(Singleton<SoundPlayer>.instance != null) {
            Singleton<SoundPlayer>.instance.stopBGM(1.0f);
            Debug.Log("OnTap");
        }

        if(!callOnce) {
            Singleton<SoundPlayer>.instance.playSE("Bottun");
            callOnce = !callOnce;
        }
    }

}