using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSound : BaseObject {

    void Start()
    {
        Singleton<SoundPlayer>.instance.playBGM("Water", 0.0f, true);
        Singleton<SoundPlayer>.instance.playBGM("Wind", 0.0f, true);
    }
	
}
