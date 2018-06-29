/**************************************************************************************/
/*! @file   ResultUpdate.cs
***************************************************************************************
@brief      リザルトのテキストを更新する
*********************************************************************************************
* @author   yuta nagashima
*********************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUpdate : BaseObject
{

	[SerializeField] private Text rankText;           // @brief プレイヤーの順位テキスト
	[SerializeField] private Text[] nameText;         // @brief 名前テキスト
	[SerializeField] private Text[] timeText;         // @brief タイムテキスト

	//仮構造体
	public struct ResultData
	{
		public float time;
		public string name;
	}

	private ResultData[] resultDatas;   // @brief 各プレイヤーのリザルトデータ
	private int playerRank;             // @brief プレイヤーの順位

	public void Start()
	{

		TestData();
		RankTextUpdate();
		ResultTextUpdate();

	}

	/// <summary>
	/// @brief リザルトデータとそのプレイヤーの順位を受け取る関数
	/// </summary>
	/// <param name="argDatas"></param>
	/// <param name="rank"></param>
	public void SetResultData(ResultData[] argDatas, int rank)
	{

		resultDatas = argDatas;
		playerRank = rank;

	}

	//テストデータ
	public void TestData()
	{

		playerRank = 1;
		resultDatas = new ResultData[4];
		resultDatas[0].time = 250000;
		resultDatas[0].name = "PLayer1";
		resultDatas[1].time = 249999;
		resultDatas[1].name = "Player2";
		resultDatas[2].time = 249998;
		resultDatas[2].name = "Player3";
		resultDatas[3].time = 250001;
		resultDatas[3].name = "Player4";

	}

	/// <summary>
	/// @brief  順位によって色と序数を変更する関数
	/// </summary>
	public void RankTextUpdate()
	{
		string[] ordinalNumber = { "st", "nd", "rd", "th" };    // @brief 数字の後に付く序数(?)

		Color[] rankColor =
		{
			new Color(255f / 255f, 215f / 255f,   0f / 255f),   //金
            new Color(192f / 255f, 192f / 255f, 192f / 255f),   //銀
            new Color(196f / 255f, 112f / 255f,  34f / 255f),   //銅
            new Color(  0f / 255f,   0f / 255f,   0f / 255f),   //黒
        };

		rankText.color = rankColor[playerRank - 1];
		rankText.text = playerRank.ToString() + ordinalNumber[playerRank - 1];

	}

	/// <summary>
	/// テキストにプレイヤーの名前とタイムを入れる関数
	/// タイムは分、秒、ミリ秒に分割
	/// </summary>
	public void ResultTextUpdate()
	{

		int milliSecond;    // @brief ミリ秒
		int second;         // @brief 秒
		int minute;         // @brief 分

		for (int i = 0; i < timeText.Length; i++)
		{

			nameText[i].text = resultDatas[i].name;

			milliSecond = (int)(resultDatas[i].time % 1000);
			second = (int)(resultDatas[i].time / 1000 % 60);
			minute = (int)(resultDatas[i].time / 1000 / 60);

			timeText[i].text = minute.ToString("00") + ":" + second.ToString("00") + ":" + milliSecond.ToString("000");

		}

	}

}
