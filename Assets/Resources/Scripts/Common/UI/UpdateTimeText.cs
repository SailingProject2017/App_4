/**************************************************************************************/
/*! @file   UpdateTimeText.cs
***************************************************************************************
@brief      TimeManagerのタイムを取得して、テキストに反映させます
*********************************************************************************************
* @note     2018-06-29 制作
*********************************************************************************************
* @author   Tsuchida Shun
*********************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateTimeText : BaseObject
{
	[SerializeField] private Text text;         // @brief タイマーの時間を反映させたいテキスト
	[SerializeField] private GameObject obj;    // @brief テキストに反映させたい、TimeManagerの入ったオブジェクト
	private TimeManager script;                 // @brief objに入っているTimeManagerをアタッチする。

	/// <summary>
	/// @brief objのTimeManagerをアタッチします
	/// </summary>
	private void Start()
	{
		script = obj.GetComponent<TimeManager>();
	}

	/// <summary>
	/// @brief TimeManagerのMillTimeを取得してテキストに代入します。
	/// </summary>
	public void UpdateText()
	{
		text.text = script.MillTime.ToString();
	}

	/// <summary>
	/// @brief UpdateTextを呼びます。
	/// </summary>
	public override void OnUpdate()
	{
		UpdateText();
	}
}
