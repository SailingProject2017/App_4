using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaSound : BaseObject
{


    private string audioClipName = "";
    [SerializeField]
    private float fadeTime = 0.0f;

    [SerializeField]
    private float volume;


    private bool callOnce = false;

    void Start()
    {
        Singleton<SoundPlayer>.instance.playBGM("Sea", fadeTime, true, volume);
    }

    public void OnTap()
    {
        if (!callOnce)
        {
            Singleton<SoundPlayer>.instance.playSE("Bottun", 0.8f);
            callOnce = !callOnce;
        }
    }

}