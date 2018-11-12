/**********************************************************************************************/
/*@file       RankingRecord.cs
*********************************************************************************************
* @brief      ランキングデータの出力と読込を行う
*********************************************************************************************
* @author     Yuta Nagashima
*********************************************************************************************
* Copyright © 2018 Yuta Nagashima All Rights Reserved.
**********************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using UnityEngine.UI;       //テスト

public class RankingRecord : BaseObject
{

    //テスト用
    [SerializeField]
    private List<Text> testText;
    [SerializeField]
    private List<int> testRecodeList;

    private const string fileDirectory = "Assets/Resources/Scripts/Common/Ranking";                //@brief 読み取るファイルのフォルダ名
    private const string fileName = "RankingData";          //@brief 読み取るファイル名
    private const string fileExtension = "csv";             //@brief ファイルの拡張子
    private const string charaCode = "UTF-8";               //@brief ファイルの文字コード
    private const int outputRecodeNum = 4;                  //@brief ファイルに出力する記録数

    private Sort rankingSort;                               //@brief ソートを行うための変数
    private List<int> recodeList;                         //@brief 読み込んだタイムを格納するリスト
    private string inputFileName;                           //@brief 読み込むファイルの階層と名前


    #region ソートを行うクラス
    public class Sort
    {

        /// <summary>
        /// @brief リストを受け取り挿入ソートを行う
        /// </summary>
        /// <param name="list">要素を持つリスト</param>
        public void InsertSort(List<int> list) 
        {
            
            for(int i = 1; i < list.Count; i++) 
            {

                int temp = list[i];

                if(list[i - 1] > temp) 
                {

                    int j = i;

                    do 
                    {
                        list[j] = list[j - 1];
                        j--;
                    } while(j > 0 && list[j - 1] > temp);

                    list[j] = temp;

                }

            }

        }

    }
    #endregion

    private void Start()
    {
        rankingSort = new Sort();
        recodeList = new List<int>();
        inputFileName = fileDirectory  + "/" + fileName + "." + fileExtension;

        //テスト
        OutRecord(testRecodeList, true);

        for(int i = 0; i < recodeList.Count; i++)
        {

            if(i > testText.Count - 1) {
                break;
            }
            testText[i].text = recodeList[i].ToString();

        }

    }

    /// <summary>
    /// @brief タイムのリストを受け取りcsvに出力する
    /// </summary>
    /// <param name="recode">タイムが格納されたリスト</param>
    /// <param name="isSort">trueでソートを実行。falseでは実行しない</param>
    public void OutRecord(List<int> recode, bool isSort) 
    {

        if(isSort) 
        {         
            //出力する前に昇順ソートを行う
            RankingCreate(recode);
        }

        StreamWriter sw = new StreamWriter(@inputFileName, false, Encoding.GetEncoding(charaCode));

        for(int i = 0; i < recodeList.Count; i++) 
        {

            //上位4位までを記録
            if(i > outputRecodeNum - 1) {
                break;
            }

            string[] tempStr = { recodeList[i].ToString() };
            string wrStr = string.Join(",", tempStr);
            sw.WriteLine(wrStr);

            Debug.Log("Out " + i + ":" + recodeList[i]);

        }

        sw.Close();

    }

    /// <summary>
    /// @brief csvを読み取りタイムを格納する
    /// </summary>
    public void InRecord()
    {

        //ファイルが存在しなかった場合の処理
        if(!System.IO.File.Exists(inputFileName)) 
        {
            Debug.Log("File Not Found");
            Debug.Log("Create New File");
            InitRecode();
            return;
        }

        StreamReader sr = new StreamReader(@inputFileName, Encoding.GetEncoding(charaCode));
        string tempLine;

        while((tempLine = sr.ReadLine()) != null)
        {
            
            recodeList.Add(int.Parse(tempLine));
            
        }

        sr.Close();

    }

    /// <summary>
    /// @brief 新規csvファイルを作成するための仮データを作成する
    /// </summary>
    private void InitRecode()
    {

        List<int> temp = new List<int>() { 300, 360, 420, 480 };
        OutRecord(temp, false);

    }

    /// <summary>
    /// @brief 受けとった記録と読み込んだ記録を合わせランキングデータを作成
    /// </summary>
    /// <param name="recode">タイムを格納したリスト</param>
    private void RankingCreate(List<int> recode)
    {
        
        //記録を読み込む
        InRecord();

        //受け取った記録を読み込んだ記録に合わせる
        for(int i = 0; i < recode.Count; i++)
        {

            recodeList.Add(recode[i]);

        }

        //ソートを行う
        rankingSort.InsertSort(recodeList);

    }

}
