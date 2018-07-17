using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUBattleSound : BaseObject {

    private string audioClipName = "";
    [SerializeField]
    private float fadeTime = 0.0f;


    private bool callOnce;


    void Start()
    {
        callOnce = false;

        Singleton<SoundPlayer>.instance.PlayBGM("TT", fadeTime, true);
    }

    public void OnTap()
    {
        if (!callOnce)
        {
            Singleton<SoundPlayer>.instance.PlaySE("Bottun2");
            callOnce = !callOnce;
        }
    }
    public void OnTapBack()
    {
        if (!callOnce)
        {
            Singleton<SoundPlayer>.instance.PlaySE("0");
            callOnce = !callOnce;
        }
    }
}
