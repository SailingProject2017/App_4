﻿/**********************************************************************************************/
/*! @file     TutorialManager.cs
*********************************************************************************************
* @brief      チュートリアルの初期状態を設定してます
*********************************************************************************************
* @author     Ryo Sugiyama　& Yuta Takatsu
*********************************************************************************************
* Copyright © 2017 Ryo Sugiyama All Rights Reserved.
**********************************************************************************************/
using System;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : BaseObject
{
    private TutorialState nextScene;
    private NextTutorial nextTutorial;
    protected string fileName = "C:\\Users\\nwuser.DA\\Desktop\\createGame's\\WindRacer\\obj\\TutorialState.obj";

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

                break;

            case eTutorial.eTutorial_ModeSelect: //　モードセレクト画面チュートリアル
                
                //　チュートリアルの状態をセーリング説明のチュートリアルにして保存する
                CreateSaveData.NextTutorialState(eTutorial.eTutorial_Sailing);
                CreateSaveData.SaveToBinaryFile(Singleton<TutorialState>.instance, fileName);

                break;

            case eTutorial.eTutorial_Sailing: //　セーリングについて

                //　チュートリアルの状態をInGameのUI説明チュートリアルにして保存する
                CreateSaveData.NextTutorialState(eTutorial.eTutorial_InGameUI);
                CreateSaveData.SaveToBinaryFile(Singleton<TutorialState>.instance, fileName);

                break;

            case eTutorial.eTutorial_InGameUI: //　InGameのUI説明のチュートリアル

                //　チュートリアルの状態を前に進もうのチュートリアルにして保存する
                CreateSaveData.NextTutorialState(eTutorial.eTutorial_Accel);
                CreateSaveData.SaveToBinaryFile(Singleton<TutorialState>.instance, fileName);

                break;

            case eTutorial.eTutorial_Accel: //　前に進もうのチュートリアルです

                //　チュートリアルの状態を曲がろうのチュートリアルにして保存する
                CreateSaveData.NextTutorialState(eTutorial.eTutorial_Curve);
                CreateSaveData.SaveToBinaryFile(Singleton<TutorialState>.instance, fileName);

                break;

            case eTutorial.eTutorial_Curve: //　曲がろうのチュートリアルです

                //　チュートリアルの状態をCPUと戦おうのチュートリアルにして保存する
                CreateSaveData.NextTutorialState(eTutorial.eTutorial_CPU);
                CreateSaveData.SaveToBinaryFile(Singleton<TutorialState>.instance, fileName);

                break;

            case eTutorial.eTutorial_CPU: //　CPUと戦おうのチュートリアルです

                //　チュートリアルの状態をユーザーネームを入力してもらうチュートリアルにして保存する
                CreateSaveData.NextTutorialState(eTutorial.eTutorial_InputUserName);
                CreateSaveData.SaveToBinaryFile(Singleton<TutorialState>.instance, fileName);

                break;

            case eTutorial.eTutorial_InputUserName: //　ユーザーネームを入力してもらうチュートリアルです。

                //　チュートリアルの状態を終わりにして保存する
                CreateSaveData.NextTutorialState(eTutorial.eTutorial_End);
                CreateSaveData.SaveToBinaryFile(Singleton<TutorialState>.instance, fileName);

                break;

            case eTutorial.eTutorial_End: //　チュートリアルがおわり

                break;

            default:

                break;
        }

        // デバッグ用
        CreateSaveData.NextTutorialState(eTutorial.eTutorial_ModeSelect);
        CreateSaveData.SaveToBinaryFile(Singleton<TutorialState>.instance, fileName);
        Debug.Log("現在のチュートリアルは" + Singleton<TutorialState>.instance.TutorialStatus);
    }
}