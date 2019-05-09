/**********************************************************************************************
*! @file     TutorialEventManager.cs
***********************************************************************************************
* @brief      チュートリアル中のイベントを管理します
***********************************************************************************************
* @author     Yuta Takatsu
***********************************************************************************************
* Copyright © 2017 Yuta Takatsu All Rights Reserved.
***********************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEventManager : BaseObject{

    [SerializeField]
    InGameTutorialPopup popup; // @brief 生成するポップアップを格納
    [SerializeField]
    List<TutorialEvent> eventList = new List<TutorialEvent>(); // @brief ポップアップ内に表示をさせるイベントをリストで格納
    [SerializeField]
    RectTransform contentRoot; // @brief 生成場所

    GameObject Content; // @brief 表示させるイベントを格納


    /// <summary>
    /// @brief BaseObjectの実装
    /// @note  初期化
    /// </summary>
    protected override void OnAwake()
    {
        base.OnAwake();
        
        foreach (var index in eventList)
        {
            StartEvent(index);
        }
    }

    /// <summary>
    /// @brief イベント設定用。ラムダ式での設定の為重複回避の為に関数分けを行う
    /// </summary>
    /// <param name="tutorialEvent">TutorialEvent.</param>
    private void StartEvent(TutorialEvent tutorialEvent)
    {

        tutorialEvent.EventCallback = id =>
        {

            popup.ButtonCallback = buttonType => DefaultPageCallback(buttonType, tutorialEvent);
            tutorialEvent.AnimationId = 0;

            // 最初に表示させるイベントを格納
            var first = New(tutorialEvent.Animations[tutorialEvent.AnimationId]);
            // 重複して表示をさせないために表示をfalseにする
            first.transform.SetParent(contentRoot.transform, false); 
            Content = first;
            popup.ButtonSet = EButtonSet.Set2;
            // ボタンのテキストを変更
            popup.SetButtonText(EButtonId.OK, "次へ");
            popup.SetButtonText(EButtonId.Cancel, "前へ");
            // ポップアップ表示と同時に最初のイベントも再生
            popup.Open(contentRoot.gameObject, tutorialEvent.BeginEvent);
        };
    }

    /// <summary>
    /// @brief ポップアップの前のページ表示用
    /// </summary>
    /// <param name="tutorialEvent">TutorialEvent.</param>
    private void EventPrevPage(TutorialEvent tutorialEvent)
    {
        // エラー対策
        if (tutorialEvent.AnimationId < 0)
            tutorialEvent.AnimationId = 0;
        
        popup.SetButtonText(EButtonId.OK, "次へ");

        // 前のイベントを格納
        var prev = New(tutorialEvent.Animations[tutorialEvent.AnimationId]);
        prev.transform.SetParent(contentRoot.transform, false);
        Content = prev;
    }

    /// <summary>
    /// @brief 通常のページ用コールバック設定
    /// </summary>
    /// <param name="id">アイテムID</param>
    /// <param name="tutorialEvent">TutorialEvent.</param>
    private void DefaultPageCallback(EButtonId id, TutorialEvent tutorialEvent)
    {
        // 前のイベントを削除してきれいな状態にする
        contentRoot.SetActive(false);
        Delete(Content);

        switch (id)
        {
            // OKボタンの処理
            case EButtonId.OK:
                tutorialEvent.AnimationId += 1;

                // 次のイベントがなければポップアップを閉じる
                if(tutorialEvent.AnimationId> tutorialEvent.Animations.Count - 1)
                {
                    popup.Close(() => { tutorialEvent.ExitEvent(); contentRoot.SetActive(false); });
                    return;
                }

                // 次があるならOKボタンの設置
                if(tutorialEvent.AnimationId >= tutorialEvent.Animations.Count - 1)
                {
                    popup.SetButtonText(EButtonId.OK, "OK");
                }

                var next = New(tutorialEvent.Animations[tutorialEvent.AnimationId]);
                next.transform.SetParent(contentRoot.transform, false);

                Content = next;

                break;

            // キャンセル(戻る)ボタンの処理
            case EButtonId.Cancel:

                // イベントの状態を1つ戻す
                tutorialEvent.AnimationId -= 1;
                EventPrevPage(tutorialEvent);

                break;
        }

        // 表示
        contentRoot.SetActive(true);
    }
}