/**********************************************************************************************/
/*@file       SwipeShipControl.cs
*********************************************************************************************
* @brief      ユーザーのスワイプ操作の処理を行う
*********************************************************************************************
* @author     Shun Tsuchida
*********************************************************************************************
* Copyright © 2017 Shun Tsuchida All Rights Reserved.
**********************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeShipControl : BaseObject
{
	// ※ 動作確認のため、変数すべてにSerializeFieldをつけています。不要な場合はなくても大丈夫です。

	// private:
	[SerializeField] private Vector2 touchStartPos;		// @brief タッチされた位置
	[SerializeField] private Vector2 touchPos;			// @brief タッチしている位置
	[SerializeField] private Vector2 touchEndPos;		// @brief タッチをやめた位置
	[SerializeField] private float touchDiscrepancy;    // @brief スワイプした距離
	[SerializeField] private bool onTouch;              // @brief タッチしているかのフラグ
	[SerializeField] private bool useSwipe;              // @brief スワイプ操作を使用するかどうかのフラグ

	// Accessor
	[SerializeField] private string fripDir;        // @brief スワイプの方向
	[SerializeField] private float moveAcceleration;// @brief 移動加速度（→touchDiscrepancyが大きいほど高くなる）

	/// <summary>
	/// @brief スワイプの方向
	/// @get スワイプの方向を取得
	/// @set スワイプの方向を代入
	/// </summary>
	public string AccessorFripDir
	{
		get { return fripDir; }
		private set { fripDir = value; }
	}
	/// <summary>
	/// @brief 移動加速度
	/// @get 移動加速度を取得
	/// @set スワイプの距離に応じて、移動加速度をセット
	/// </summary>
	public float AccessorMoveAcceleration
	{
		get { return moveAcceleration; }
		private set { moveAcceleration = 1 + Mathf.Abs(value / 1000); }
	}

	/// <summary>
	/// @brief 変数の初期化
	/// </summary>
	/// <param name="void"></param>
	/// <retrun>void</retrun>
	void Start()
	{
		useSwipe = false;
		onTouch = false;
		fripDir = "None";
	}
	/// <summary>
	/// @brief タッチ判定とスワイプの計算処理
	/// </summary>
	/// <param name="void"></param>
	/// <retrun>void</retrun>
	override public void OnUpdate()
	{
		if(useSwipe) Touch();
		if (onTouch)
		{
			SwipeRangeCalculation();
			SetDirection();
			AccessorMoveAcceleration = touchDiscrepancy;
			Move();	// Test
		}
	}

	/// <summary>
	/// @brief タッチの処理
	/// </summary>
	/// <param name="void"></param>
	/// <retrun>void</retrun>
	private void Touch()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			onTouch = true;
			touchStartPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		}
		else if (Input.GetKey(KeyCode.Mouse0))
		{
			onTouch = true;
			touchPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		}
		else if (Input.GetKeyUp(KeyCode.Mouse0))
		{
			onTouch = false;
			fripDir = "None";
			touchEndPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		}
	}
	/// <summary>
	/// @brief スワイプした距離の計算
	/// </summary>
	/// <param name="void"></param>
	/// <retrun>void</retrun>
	private void SwipeRangeCalculation()
	{
		touchDiscrepancy = touchStartPos.x - touchPos.x;
	}
	/// <summary>
	/// @brief スワイプした方向の計算し、fripDirをセットする。
	/// </summary>
	/// <param name="void"></param>
	/// <retrun>void</retrun>
	private void SetDirection()
	{
		// left
		if (touchDiscrepancy > 0)
		{
			AccessorFripDir = "Left";
		}
		// right
		if (touchDiscrepancy < 0)
		{
			AccessorFripDir = "Right";
		}
	}

	/// <summary>
	/// @brief Test：移動確認（本来は角度）
	/// </summary>
	/// <param name="void"></param>
	/// <retrun>void</retrun>
	private void Move()
	{
		switch (AccessorFripDir)
		{
			case "Left":
				this.transform.position += new Vector3(0.1f * AccessorMoveAcceleration, 0.0f, 0.0f);
				break;

			case "Right":
				this.transform.position -= new Vector3(0.1f * AccessorMoveAcceleration, 0.0f, 0.0f);
				break;
		}
	}
}