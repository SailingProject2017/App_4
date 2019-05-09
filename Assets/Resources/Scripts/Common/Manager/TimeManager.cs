/**************************************************************************************/
/*! @file   TimeManager.cs
***************************************************************************************
@brief      クリアタイムを測る
*********************************************************************************************
* @note     2018-06-28 制作
*********************************************************************************************
* @author   Shun Tsuchida
*********************************************************************************************/

using System.Collections;
using UnityEngine;

class TimeManager : BaseObject
{
	[SerializeField] private bool onTimer;      // @brief タイマーの更新フラグ
	[SerializeField] private float millTime;    // @brief 経過時間
    [SerializeField] private int minute;     // @brief 分

	/// <summary>
	/// @get タイマーの経過時間（ミリ秒）を取得する
	/// </summary>
	public float MillTime
	{
		get { return millTime; }
	}

    /// <summary>
    /// @get タイマーの経過時間 (分) を取得する
    /// </summary>
    public int Minute
    {
        get { return minute; }
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
        minute = 0;
	}

	/// <summary>
	/// @brief タイマー更新のオンオフを切り替えます。
	/// </summary>
	public void TimerSwich()
	{
		onTimer = !onTimer;
	}

    /// <summary>
    /// @brief タイマー更新フラグのアクセサ
    /// @set フラグの更新
    /// @get フラグの取得
    /// </summary>
	public bool OnTimer
    {
        set { onTimer = value; }
        get { return onTimer; }
    }

	/// <summary>
	/// @brief タイマーがオンの時だけ更新します
	/// </summary>
	public override void OnUpdate()
	{
		if (Singleton<GameInstance>.Instance.IsGoal) onTimer = false;
		
		if (onTimer) { millTime += Time.deltaTime; }
        // 分に変換
        if(millTime>= 60f)
        {
            minute++;
            millTime = millTime - 60;
        }
	}
}

