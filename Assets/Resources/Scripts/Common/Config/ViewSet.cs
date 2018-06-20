﻿/**********************************************************************************************/
/*@file       ViewSet.cs
*********************************************************************************************
* @brief	　初期のカメラ設定をセット
*********************************************************************************************
* @author     Shun Tsuchida
*********************************************************************************************
* Copyright © 2018 Shun Tsuchida All Rights Reserved.
**********************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewSet : BaseObject
{

	[SerializeField] private bool onTPS;        // TPSのオンオフフラグ
	[SerializeField] private Button TPSButton;  // TPSをオンにするためのボタン
	[SerializeField] private bool onFPS;        // FPSのオンオフフラグ
	[SerializeField] private Button FPSButton;  // FPSをオンにするためのボタン

	/// <summary>
	/// @brief TPS
	/// @get TPSをオンオフを取得
	/// @set TPSをオンオフをセット
	/// </summary>
	public bool AccessoronOnTPS
	{
		get { return onTPS; }
		private set
		{
			onTPS = value;
			onFPS = !value;
		}
	}
	/// <summary>
	/// @brief FPS
	/// @get FPSをオンオフを取得
	/// @set FPSをオンオフをセット
	/// </summary>
	public bool AccessoronOnFPS
	{
		get { return onFPS; }
		private set
		{
			onFPS = value;
			onTPS = !value;
		}
	}

	/// <summary>
	/// @brief 変数の初期化
	/// </summary>
	void Start()
	{
		OnViewTPS();
	}

	/// <summary>
	/// @brief FPSをオンにする
	/// </summary>
	public void OnViewFPS()
	{
		AccessoronOnFPS = true;
		FPSButton.GetComponent<Image>().color = new Color(255, 255, 255, 255);
		TPSButton.GetComponent<Image>().color = new Color(0, 0, 0, 255);
	}
	/// <summary>
	/// @brief TPSをオンにする
	/// </summary>
	public void OnViewTPS()
	{
		AccessoronOnTPS = true;
		TPSButton.GetComponent<Image>().color = new Color(255, 255, 255, 255);
		FPSButton.GetComponent<Image>().color = new Color(0, 0, 0, 255);
	}
}
