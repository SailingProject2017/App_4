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

    void Update()
    {
        Debug.Log("現在のチュートリアルは" + Singleton<TutorialState>.instance.TutorialStatus);
    }

    /// <summary>
    /// @brief チュートリアルステートを切り替えるメソッド
    ///        切り替える条件が様々なためstatic化
    /// </summary>
    public void NextTutorialState(int TutorialId)
    {

        // チュートリアルが切り替わることを通知
        BaseObjectSingleton<GameInstance>.Instance.IsTutorialState = true;

        // チュートリアル中は、最後に行ったチュートリアルのシーンまで飛びます。
        switch (TutorialId)
        {
            case 0:  // 初回起動

                //　チュートリアルの状態をModeSelectチュートリアルにして保存する
                CreateSaveData.NextTutorialState(eTutorial.eTutorial_ModeSelect);
                CreateSaveData.SaveToBinaryFile(Singleton<TutorialState>.instance, fileName);

                break;

            case 1: //　モードセレクト画面チュートリアル

                //　チュートリアルの状態をstraightチュートリアルにして保存する
                CreateSaveData.NextTutorialState(eTutorial.eTutorial_Straight);
                CreateSaveData.SaveToBinaryFile(Singleton<TutorialState>.instance, fileName);

                break;

            case 2: //　straight

                //　チュートリアルの状態をcurveチュートリアルにして保存する
                CreateSaveData.NextTutorialState(eTutorial.eTutorial_Curve);
                CreateSaveData.SaveToBinaryFile(Singleton<TutorialState>.instance, fileName);

                break;

            case 3: //　curve

                //　チュートリアルの状態をEndTextチュートリアルにして保存する
                CreateSaveData.NextTutorialState(eTutorial.eTutorial_EndText);
                CreateSaveData.SaveToBinaryFile(Singleton<TutorialState>.instance, fileName);

                break;

            case 4: //　最後のテキスト

                //　チュートリアルの状態をEndチュートリアルにして保存する
                CreateSaveData.NextTutorialState(eTutorial.eTutorial_End);
                CreateSaveData.SaveToBinaryFile(Singleton<TutorialState>.instance, fileName);

                break;

            case 5: //　チュートリアルがおわり

                break;

            default:

                break;
        }

    }
}