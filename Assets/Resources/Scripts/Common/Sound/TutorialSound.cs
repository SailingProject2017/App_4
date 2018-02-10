using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSound : BaseObject
{
    private bool callOnce = false;

    void Start()
    {
        Singleton<SoundPlayer>.instance.playBGM("TT", 0.0f, true);
    }
	
    public void OnTap()
    {
        if (!callOnce)
        {
            Singleton<SoundPlayer>.instance.playSE("Bottun2");
            callOnce = !callOnce;
        }
    }
}
