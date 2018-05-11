using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaSound : BaseObject {


    private string audioClipName = "";
    [SerializeField]
    private float fadeTime = 0.5f;


    private bool callOnce = false;

    void Start() {
        Singleton<SoundPlayer>.instance.PlayBGM("Sea", fadeTime, true);
        Debug.Log("SeaSoundPlay!");
    }

    public void OnTap() {

        Singleton<SoundPlayer>.instance.StopBGM(fadeTime);
        Debug.Log("OnTap");

        if(!callOnce) {
            Singleton<SoundPlayer>.instance.PlaySE("Bottun");
            callOnce = !callOnce;
        }
    }

}