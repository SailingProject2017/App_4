/**************************************************************************************/
/*! @file   TimeGet_to_Text.cs
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

public class Get_to_Text : BaseObject
{

	[SerializeField] Text text;         // タイマーの時間を反映させたいテキスト
	[SerializeField] GameObject obj;    // テキストに反映させたい、TimeManagerの入ったオブジェクト
	TimeManager script;                 // objのTimeManagerをアタッチする。

	/// <summary>
	/// @brief objのTimeManagerをアタッチします
	/// </summary>
	private void Start() { script = obj.GetComponent<TimeManager>(); }
	/// <summary>
	/// @brief TimeManagerのMillTimeを取得してテキストに代入します。
	/// </summary>
	public void UpdateText() { text.text = script.MillTime.ToString(); }

	/// <summary>
	/// @brief UpdateTextを呼びます。
	/// </summary>
	public override void OnUpdate() { UpdateText(); }
}
