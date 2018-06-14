/***********************************************************************/
/*! @file   NextScene.cs
*************************************************************************
*   @brief  次のシーンを登録するスクリプト
*************************************************************************
*   @author yuta takatsu
*************************************************************************
*   Copyright © 2017 yuta takatsu All Rights Reserved.
************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Scene;

public class NextScene : BaseObject
{

    private int tutorialId;


    protected override void AppendListConstructor()
    {
        base.AppendListConstructor();
    }

    [SerializeField]
    private SCENES nextScene; // @brief 次のシーン格納用

    /// <summary>
    /// @brief 次のシーンを判断するアクセサー
    /// </summary>
    public SCENES AccessNextScene { set; get; }

    /// <summary>
    /// @brief タップされたときに呼ばれる関数
    /// </summary>
    public void OnTap()
    {

        //シーン破棄時に実行される関数をオブジェクト分だけ実行
        foreach (var obj in CurrentSceneObjectList)
        {
            obj.OnEnd();
        }

        SceneManager.SceneMove(nextScene); // SceneManagerを呼び出す 引数は次のシーン
    }

    /// <summary>
    /// @brief 次のチュートリアルに変える
    /// </summary>
    public void NextTutorialState()
    {
        switch (Singleton<TutorialState>.instance.TutorialStatus)
        {
            case eTutorial.eTutorial_ModeSelect: // 1　モードセレクト画面チュートリアル
                TutorialManager.Instance.NextTutorialState(1);

                break;

            case eTutorial.eTutorial_Straight: // 2　Straight
                TutorialManager.Instance.NextTutorialState(2);

                break;

            case eTutorial.eTutorial_Curve: // 3　Curve
                TutorialManager.Instance.NextTutorialState(3);

                break;

            case eTutorial.eTutorial_EndText: // 4　最後のテキスト
                TutorialManager.Instance.NextTutorialState(4);

                break;

            default:

                break;
        }
    }
}