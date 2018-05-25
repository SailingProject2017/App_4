/**********************************************************************************************/
/*@file       SlopeShipControl.cs
*********************************************************************************************
* @brief      ユーザーが加速度センサーを用いた場合の処理を行う
*********************************************************************************************
* @author     Shun Tsuchida
*********************************************************************************************
* Copyright © 2018 Shun Tsuchida All Rights Reserved.
**********************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeShipControl : BaseObject
{
	// private:
	[SerializeField] private float slopeVector;			// @brief 加速度センサーのｘ軸の値を取得
	[SerializeField] private bool useSlope;				// @brief 加速度センサーを使用するかどうかのフラグ

	// Accessor
	[SerializeField] private string slopeDir;			// @brief 傾けた距離
	[SerializeField] private float moveAcceleration;	// @brief 移動加速度（→slopeVectorが大きいほど高くなる）

	/// <summary>
	/// @brief スワイプの方向
	/// @get スワイプの方向を取得
	/// @set スワイプの方向を代入
	/// </summary>
	public string AccessorSlopeDir
	{
		get { return slopeDir; }
		private set { slopeDir = value; }
	}
	/// <summary>
	/// @brief 移動加速度
	/// @get 移動加速度を取得
	/// @set 傾きの度合に応じて、移動加速度をセット
	/// </summary>
	public float AccessorMoveAcceleration
	{
		get { return moveAcceleration; }
		private set { moveAcceleration = 1 + Mathf.Abs(value); }
	}

	/// <summary>
	/// @brief 変数の初期化
	/// </summary>
	/// <param name="void"></param>
	/// <retrun>void</retrun>
	void Start()
	{
		useSlope = false;
		slopeDir = "None";
	}
	/// <summary>
	/// @brief 加速度センサー処理
	/// </summary>
	/// <param name="void"></param>
	/// <retrun>void</retrun>
	override public void OnUpdate()
	{
		// 加速度センサーを使用するなら、以下の加速度センサーの処理を行う。
		if (useSlope)
		{
			slopeVector = Input.acceleration.x;		// 傾きを取得
			SetSlope();								// 傾きから、傾けた方向を取得
			AccessorMoveAcceleration = slopeVector;	// 傾きの度合いから移動量を計算してセット
		}
		// else, かつ、使用中の方向が残っていたら、誤動作を防止のため、傾きの方向をNoneにする。
		else if (AccessorSlopeDir != "None")
		{
			AccessorSlopeDir = "None";
		}
	}

	/// <summary>
	/// @brief 加速度センサーの値に応じて、移動させる方向をセット
	/// </summary>
	/// <param name="void"></param>
	/// <retrun>void</retrun>
	void SetSlope()
	{
		if (slopeVector > 0.1) { AccessorSlopeDir = "Right"; }
		else if (slopeVector < -0.1) { AccessorSlopeDir = "Left"; }
		else { AccessorSlopeDir = "None"; }
	}
}
