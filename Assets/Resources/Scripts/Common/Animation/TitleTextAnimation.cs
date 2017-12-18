/***********************************************************************/
/*! @file   TitleTextAnimation.cs
*************************************************************************
*   @brief  タイトルのアニメーションを制御するスクリプト
*************************************************************************
*   @author yuta takatsu
*************************************************************************
*   Copyright © 2017 yuta takatsu All Rights Reserved.
************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TitleTextAnimation : BaseObject
{

    [SerializeField]
    private Image me;                // 画像登録
    private Vector3 movedPos;        // 座標用

    [SerializeField]
    private float durationSecondes;  // 点滅の周期時間
    private Ease easeType;           // SetEaseのEasingを指定

    [SerializeField]
    private EAnimeType animeType;    // enum判断用

    private CanvasGroup canvasGroup; // 子要素含め扱える

    /// <summary> どの画像かを判断する用 </summary>
    public enum EAnimeType
    {
        TITLE_WIND,
        TITLE_RASER,
        TITLE_TEXT
    }

    /// <summary> 初期座標をセット Typeに応じて最終座標も代入 </summary>
    protected override void AppendListConstructor()
    {
        base.AppendListConstructor();

        if (animeType == EAnimeType.TITLE_WIND)
        {
            movedPos.x = -150.0f;
            me.rectTransform.localPosition = new Vector3(-1200.0f, 200.0f, 0.0f);
        }
        if (animeType == EAnimeType.TITLE_RASER)
        {
            movedPos.x = 150.0f;
            me.rectTransform.localPosition = new Vector3(1200.0f, 0.0f, 0.0f);
        }
    }

    /// <summary> movedPosの位置に2.0fで移動 </summary>
    void Start()
    {
        // テキストの点滅
        if (animeType == EAnimeType.TITLE_TEXT)
        {
            DOVirtual.DelayedCall(2.5f, () => Text());
        }
        // タイトルロゴのアニメーション
        else
        {
            TitleLog();
        }
    }

    public void Text()
    {    
        this.canvasGroup = this.GetComponent<CanvasGroup>();
        this.canvasGroup.DOFade(1.0f, this.durationSecondes).SetEase(this.easeType).SetLoops(-1, LoopType.Yoyo);
    }

    public void TitleLog()
    {
        me.rectTransform.DOAnchorPosX(movedPos.x, 2.0f).SetEase(Ease.InOutBack);
    }
}