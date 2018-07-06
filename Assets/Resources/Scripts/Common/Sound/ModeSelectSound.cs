using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSelectSound : BaseObject
{
    private string audioClipName = "";
    [SerializeField]
    private float fadeTime = 0.0f;


    private bool callOnce;


    void Start()
    {
        callOnce = false;

        Singleton<SoundPlayer>.instance.PlayBGM("ModeSelect", fadeTime, true);
    }

    public void OnTap()
    {
        if (!callOnce)
        {
            Singleton<SoundPlayer>.instance.PlaySE("TitleButton");
            callOnce = !callOnce;
        }
    }
}
