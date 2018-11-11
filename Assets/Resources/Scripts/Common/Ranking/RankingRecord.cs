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
    private List<float> testRecodeList;

    //定数
    private const string fileDirectory = "Assets/Resources/Scripts/Common/Ranking";                //@brief 読み取るファイルのフォルダ名
    private const string fileName = "RankingData";          //@brief 読み取るファイル名
    private const string fileExtension = "csv";             //@brief ファイルの拡張子
    private const string charaCode = "UTF-8";               //@brief ファイルの文字コード
    private const int outputRecodeNum = 4;                  //@brief ファイルに出力する記録数

    //変数
    private Sort rankingSort;                               //@brief ソートを行うための変数
    private List<float> recodeList;                         //@brief 読み込んだタイムを格納するリスト
    private string inputFileName;                           //@brief 読み込むファイルの階層と名前


    #region ソートを行うクラス
    public class Sort
    {

        /// <summary>
        /// @brief リストを受け取り挿入ソートを行う
        /// </summary>
        /// <param name="first">左端の要素番号</param>
        /// <param name="last">右端の要素番号</param>
        /// <param name="list">要素を持つリスト</param>
        public void InsertSort(int first, int last, List<float> list) 
        {
            
            for(int i = 1; i < list.Count; i++) 
            {

                float temp = list[i];

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
        recodeList = new List<float>();
        inputFileName = fileDirectory  + "/" + fileName + "." + fileExtension;

        //テスト
        OutRecord(testRecodeList, true);
        InRecord();

        for(int i = 0; i < recodeList.Count; i++) {

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
    public void OutRecord(List<float> recode, bool isSort) 
    {

        if(isSort) 
        {         
            //出力する前に昇順ソートを行う
            RankingSort(recode);
        }

        StreamWriter sw = new StreamWriter(@inputFileName, false, Encoding.GetEncoding(charaCode));
      
        for(int i = 0; i < recode.Count; i++) 
        {

            //上位4位までを記録
            if(i > outputRecodeNum - 1) {
                break;
            }

            string[] tempStr = { recode[i].ToString() };
            string wrStr = string.Join(",", tempStr);
            sw.WriteLine(wrStr);

            Debug.Log("Out " + i + ":" + recode[i]);

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
            
            recodeList.Add(float.Parse(tempLine));
            
        }

        sr.Close();

    }

    /// <summary>
    /// @brief 新規csvファイルを作成するための仮データを作成する
    /// </summary>
    private void InitRecode()
    {

        List<float> temp = new List<float>() { 300, 360, 420, 480 };
        OutRecord(temp, false);

    }

    /// <summary>
    /// @brief 受けとった記録と読み込んだ記録を合わせランキングデータを作成
    /// </summary>
    /// <param name="recode">タイムを格納したリスト</param>
    private void RankingSort(List<float> recode)
    {
        
        //記録を読み込む
        InRecord();

        //受け取った記録を読み込んだ記録に合わせる
        for(int i = 0; i < recode.Count; i++)
        {

            recodeList.Add(recode[i]);
            Debug.Log(i + ":" + recodeList[i]);

        }

        //ソートを行う
        rankingSort.InsertSort(0, recodeList.Count - 1, recodeList);

    }

}
