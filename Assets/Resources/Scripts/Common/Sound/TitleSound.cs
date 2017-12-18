using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSound : BaseObject {

    private string audioClipName = "";
    [SerializeField]
    private float fadeTime = 0.0f;

    BGMPlayer player;

    void Start()
    {
        Singleton<SoundPlayer>.instance.playBGM("Title", fadeTime, true);
    }

}
