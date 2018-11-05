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

    //テスト
    [SerializeField]
    private Text testText;
    private float testRecode;

    private List<float> recodeList;                     //タイムを格納する
    private const string fileDirectory = "";            //読み取るファイルが存在するフォルダ
    private const string fileName = "RankingData.csv";  //読み取るファイル名

    private void Start() {

        Debug.Log("ランキングだよ");
        OutRecord(10.0f);
        InRecord();
        testText.text = testRecode.ToString();

    }

    /// <summary>
    /// @brief テスト用
    /// </summary>
    /// <param name="recode"></param>
    public void OutRecord(float recode)
    {

        StreamWriter sw = new StreamWriter(@fileName, false, Encoding.GetEncoding("Shift_JIS"));
        
        for(int i = 0; i < 1; i++) 
        {

            string[] str = { recode.ToString() };
            string wrStr = string.Join(",", str);
            sw.WriteLine(wrStr);

        }

        sw.Close();

        Debug.Log("出力しました");

    }

    /// <summary>
    /// @brief タイムのリストを受け取りcsvに出力する
    /// </summary>
    /// <param name="recode">タイムを格納したリスト</param>
    public void OutRecord(List<float> recode) {

        StreamWriter sw = new StreamWriter(@fileName, false, Encoding.GetEncoding("Shift_JIS"));

        for(int i = 0; i < recode.Count; i++) 
        {

            string[] timeRecord = { recode[i].ToString() };
            string str2 = string.Join(",", timeRecord);
            sw.WriteLine(str2);

        }

        sw.Close();

        Debug.Log("出力しました");

    }

    /// <summary>
    /// @brief csvを読み取りタイムを格納する
    /// </summary>
    public void InRecord()
    {

        StreamReader sr = new StreamReader(@fileName, Encoding.GetEncoding("Shift_JIS"));
        string line;

        while((line = sr.ReadLine()) != null)
        {

            testRecode = float.Parse(line);
            recodeList.Add(testRecode);

        }

        sr.Close();

        Debug.Log("読み込みました");

    }

}
