using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSRender : BaseObject
{
    [SerializeField]
    private float updateInterval = 0.5f;

    private float accum;
    private int frames;
    private float timeleft;
    private float fps;

    public override void OnUpdate()
    {

        base.OnUpdate();

        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        frames++;

        if (0 < timeleft) return;

        fps = accum / frames;
        timeleft = updateInterval;
        accum = 0;
        frames = 0;
    }

    private void OnGUI()
    {
        GUILayout.Label("FPS: " + fps.ToString("f2"));
    }
}