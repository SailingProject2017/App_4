/**********************************************************************************************/
/*! @file     TutorialManager.cs
*********************************************************************************************
* @brief      チュートリアルの初期状態を設定してます
*********************************************************************************************
* @author     Ryo Sugiyama
*********************************************************************************************
* Copyright © 2017 Ryo Sugiyama All Rights Reserved.
**********************************************************************************************/
using System;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : BaseObject
{
    private NextScene nextScene;
    protected string fileName = "C:\\Users\\nwuser.DA\\Desktop\\Sailing2017\\App4\\SailingProject_2017\\obj\\TutorialState.obj";

    protected override void AppendListConstructor()
    {
        base.AppendListConstructor();

        // チュートリアルの情報を取得
        Singleton<TutorialState>.instance = (TutorialState)CreateSaveData.LoadFromBinaryFile(fileName);

        // @brief チュートリアル中は、最後に行ったチュートリアルのシーンまで飛びます。
        switch (Singleton<TutorialState>.instance.TutorialStatus)
        {
            case eTutorial.eTutorial_Null:  // 初回起動

                //　チュートリアルの状態をモードセレクトのチュートリアルにして保存する
                CreateSaveData.NextTutorialState(eTutorial.eTutorial_ModeSelect);
                CreateSaveData.SaveToBinaryFile(Singleton<TutorialState>.instance, fileName);

                //次のシーンをModeSelectにする
                nextScene.AccessNextScene = SCENES.MODESELECT;
                break;

            case eTutorial.eTutorial_ModeSelect: //　モードセレクト画面チュートリアル
                nextScene.AccessNextScene = SCENES.MODESELECT;
                break;

            case eTutorial.eTutorial_Sailing: //　セーリングについて
                nextScene.AccessNextScene = SCENES.INTUTORIAL;
                break;

            case eTutorial.eTutorial_InGameUI: //　InGameのUI説明のチュートリアル
                nextScene.AccessNextScene = SCENES.INTUTORIAL;
                break;

            case eTutorial.eTutorial_Accel: //　前に進もうのチュートリアルです
                nextScene.AccessNextScene = SCENES.INTUTORIAL;
                break;

            case eTutorial.eTutorial_Curve: //　曲がろうのチュートリアルです
                nextScene.AccessNextScene = SCENES.INTUTORIAL;
                break;

            case eTutorial.eTutorial_CPU: //　CPUと戦おうのチュートリアルです
                nextScene.AccessNextScene = SCENES.INTUTORIAL;
                break;

            case eTutorial.eTutorial_InputUserName: //　ユーザーネームを入力してもらうチュートリアルです。

                break;

            case eTutorial.eTutorial_End: //　チュートリアルがおわり

                break;

            default:

                break;
        }


        if (CreateSaveData.DoTutorial(fileName, eTutorial.eTutorial_ModeSelect))
            Debug.Log("チュートリアルはModeSelectです。");

        CreateSaveData.NextTutorialState(eTutorial.eTutorial_End);   
        CreateSaveData.SaveToBinaryFile(Singleton<TutorialState>.instance, fileName); 
    }

    
}