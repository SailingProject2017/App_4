/**********************************************************************************************/
/*! @file     CreateSaveData.cs
*********************************************************************************************
* @brief      オブジェクトのデータをバイナリファイルにロード・セーブをする
*********************************************************************************************
* @note       あとから拡張しやすいように改良します
*********************************************************************************************
* @author     Ryo Sugiyama
*********************************************************************************************
* Copyright © 2017 Ryo Sugiyama All Rights Reserved.
**********************************************************************************************/
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class CreateSaveData
{
    #region チュートリアルの状態を管理する関数群
    /**********************************************************************************************/
    /// <summary>
    /// @brief 指定されたチュートリアルを実行するか判断する関数
    /// </summary>
    /// <param name="path"></param>
    /// <returns>true 実行可 : false 実行不可</returns>
    public static bool DoTutorial(string path, eTutorial state)
    {
        Singleton<TutorialState>.instance = (TutorialState)LoadFromBinaryFile(path);
        if (Singleton<TutorialState>.instance.TutorialStatus == state)
        {
            Debug.Log(Singleton<TutorialState>.instance.TutorialStatus);
            return true;
        }
        return false;
    }

    /// <summary>
    /// @brief チュートリアルの情報を更新する
    /// </summary>
    /// <param name="state"></param>
    public static void NextTutorialState(eTutorial state)
    {
        Singleton<TutorialState>.instance.TutorialStatus = state;
        Debug.Log(message: Singleton<TutorialState>.instance.TutorialStatus + "setNext");
    }
    #endregion

    #region バイナリファイルへのアクセサー
    /**********************************************************************************************/
    /// <summary>
    /// @brief オブジェクトの内容をファイルから読み込み復元する
    /// </summary>
    /// <param name="path">読み込むファイル名</param>
    /// <returns>復元されたオブジェクト</returns>
    public static object LoadFromBinaryFile(string path)
    {

        FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        BinaryFormatter bf = new BinaryFormatter();

        if (fs.Length == 0)
        {

#if DEBUG
            Debug.Log(message: "ファイル" + path + "が見つからないので作ります。");
            Debug.Log("初回起動です");
#endif
            fs.Close();
            CreateBineryFile(path);
            return null;
        }

        //読み込んで逆シリアル化する
        object obj = bf.Deserialize(fs);
        fs.Close();

        return obj;


    }

    /// <summary>
    /// @brief オブジェクトの内容をファイルに保存する
    /// </summary>
    /// <param name="obj">保存するオブジェクト</param>
    /// <param name="path">保存先のファイル名</param>
    public static void SaveToBinaryFile(object obj, string path)
    {
        FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
        BinaryFormatter bf = new BinaryFormatter();


        //シリアル化して書き込む
        bf.Serialize(fs, obj);
        fs.Close();
    }

    public static void CreateBineryFile(string path)
    {
        //　ファイルの生成
        FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
        NextTutorialState(eTutorial.eTutorial_Null);
        fs.Close();

        SaveToBinaryFile(Singleton<TutorialState>.instance, path);
    }
    #endregion
}

