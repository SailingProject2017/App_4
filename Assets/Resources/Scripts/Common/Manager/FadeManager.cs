/***********************************************************************/
/*! @file   FadeManager.cs
*************************************************************************
*   @brief  フェードの制御をするマネージャークラス
*************************************************************************
*   @author yuta takatsu
*************************************************************************
*   Copyright © 2017 yuta takatsu All Rights Reserved.
************************************************************************/
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class FadeManager : BaseObjectSingleton<FadeManager>
{


    private float fadeAlpha = 0; // フェード中の透明度

    private bool isFading = false; // フェード中かどうか

    public Color fadeColor = Color.black; // フェード色

    protected override void AppendListConstructor()
    {
        base.AppendListConstructor();
    }

    public void OnGUI()
    {

        // Fade
        if (this.isFading)
        {
            // 色と透明度を更新して白テクスチャを描画 .
            this.fadeColor.a = this.fadeAlpha;
            GUI.color = this.fadeColor;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
        }
    }

    /// <summary> 画面遷移 </summary>
    /// <param name="scene"> シーン名 </param>
    /// <param name="interval"> 暗転にかかる時間 </param>
    public void LoadScene(int scene, float interval)
    {
        StartCoroutine(TransScene(scene, interval));
    }

    /// <summary> シーン繊維用コルーチン </summary>
    /// <param name="scene"> シーン名 </param>
    /// <param name="interval"> 暗転にかかる時間 </param>
    private IEnumerator TransScene(int scene, float interval)
    {
        // だんだん暗く .
        this.isFading = true;
        float time = 0;
        while (time <= interval)
        {
            this.fadeAlpha = Mathf.Lerp(0f, 1f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }

        // シーン切替 .
        SceneManager.LoadScene(scene);

        // だんだん明るく .
        time = 0;
        while (time <= interval)
        {
            this.fadeAlpha = Mathf.Lerp(1f, 0f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }
        this.isFading = false;
    }
}