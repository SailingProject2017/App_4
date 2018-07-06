/**************************************************************************************/
/*! @file   TimeManager.cs
***************************************************************************************
@brief      クリアタイムを測る
*********************************************************************************************
* @note     2018-06-28 制作
*********************************************************************************************
* @author   Tsuchida Shun
*********************************************************************************************/

using System.Collections;
using UnityEngine;

class TimeManager : BaseObject
{
	[SerializeField] private bool onTimer;      // @brief タイマーの更新フラグ
	[SerializeField] private float millTime;    // @brief 経過時間

	/// <summary>
	/// @get タイマーの経過時間（ミリ秒）を取得する
	/// </summary>
	public float MillTime
	{
		get { return millTime; }
	}

	/// <summary>
	/// @brief TimerResetを呼び出します。
	/// </summary>
	private void Start()
	{
		ResetTimer();
	}

	/// <summary>
	/// @brief タイマーを0に初期化します。
	/// </summary>
	public void ResetTimer()
	{
		millTime = 0;
	}

	/// <summary>
	/// @brief タイマー更新のオンオフを切り替えます。
	/// </summary>
	public void TimerSwich()
	{
		onTimer = !onTimer;
	}

	/// <summary>
	/// @brief タイマーがオンの時だけ更新します
	/// </summary>
	public override void OnUpdate()
	{
		if (onTimer) { millTime += Time.deltaTime; }
	}
}

