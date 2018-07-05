/**********************************************************************************************/
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

public class TutorialManager : BaseObjectSingleton<TutorialManager>
{

    protected string fileName;
    

    protected override void AppendListConstructor()
    {
        base.AppendListConstructor();
 
        /// 全プラットフォーム対応
        /// ただしAndroidのみ 4.4以上動作
        fileName = Application.persistentDataPath + ".xml";

        // チュートリアルの情報を取得
        Singleton<TutorialState>.instance = (TutorialState)CreateSaveData.LoadFromBinaryFile(fileName);

        // 初回起動はチュートリアルモードに突入させる
        if(Singleton<TutorialState>.instance.TutorialStatus == eTutorial.eTutorial_Null)
        {
            //　チュートリアルの状態をモードセレクトのチュートリアルにして保存する
            CreateSaveData.NextTutorialState(eTutorial.eTutorial_ModeSelect);
            CreateSaveData.SaveToBinaryFile(Singleton<TutorialState>.instance, fileName);
        }   
        
        // デバッグ用
        CreateSaveData.NextTutorialState(eTutorial.eTutorial_ModeSelect);
       //CreateSaveData.SaveToBinaryFile(Singleton<TutorialState>.instance, fileName);
        
    }

    /// <summary>
    /// @brief チュートリアルステートを切り替えるメソッド
    /// </summary>
    public void NextTutorialState(eTutorial TutorialId)
    {

        // チュートリアルが切り替わることを通知
        BaseObjectSingleton<GameInstance>.Instance.IsTutorialState = true;

        // チュートリアル中は、最後に行ったチュートリアルのシーンまで飛びます。
        switch (TutorialId)
        {
            case eTutorial.eTutorial_Null:  // 初回起動

                //　チュートリアルの状態をModeSelectチュートリアルにして保存する
                CreateSaveData.NextTutorialState(eTutorial.eTutorial_ModeSelect);

                break;

            case eTutorial.eTutorial_ModeSelect: //　モードセレクト画面チュートリアル

                //　チュートリアルの状態をstraightチュートリアルにして保存する
                CreateSaveData.NextTutorialState(eTutorial.eTutorial_Straight);

                break;

            case eTutorial.eTutorial_Straight: //　straight

                //　チュートリアルの状態をcurveチュートリアルにして保存する
                CreateSaveData.NextTutorialState(eTutorial.eTutorial_Curve);

                break;

            case eTutorial.eTutorial_Curve: //　curve

                //　チュートリアルの状態をEndTextチュートリアルにして保存する
                CreateSaveData.NextTutorialState(eTutorial.eTutorial_EndText);

                break;

            case eTutorial.eTutorial_EndText: //　最後のテキスト

                //　チュートリアルの状態をEndチュートリアルにして保存する
                CreateSaveData.NextTutorialState(eTutorial.eTutorial_End);

                break;

            case eTutorial.eTutorial_End: //　チュートリアルがおわり

                break;

            default:

                break;

        }
        // 状態を保存する
        CreateSaveData.SaveToBinaryFile(Singleton<TutorialState>.instance, fileName);
     
    }
}