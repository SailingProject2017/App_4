/**********************************************************************************************/
/*@file       SettingMenu.cs
*********************************************************************************************
* @brief      設定メニューの表示非表示
*********************************************************************************************
* @author     Shun Tsuchida
*********************************************************************************************
* Copyright © 2018 Shun Tsuchida All Rights Reserved.
**********************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingMenu : BaseObject
{
	[SerializeField] private GameObject settingMenu;	// メニューを出すボタン
	[SerializeField] private GameObject settingButten;	// メニュー
	[SerializeField] private bool activeMenu;			// メニューの表示非表示のフラグ

	/// <summary>
	/// @brief 変数の初期化
	/// </summary>
	private void Start()
	{
		activeMenu = false;
		settingMenu.SetActive(activeMenu);
		settingButten.SetActive(!activeMenu);
	}

	/// <summary>
	/// @brief booleanを入れ替える
	/// </summary>
	bool ChengeBool(bool arg)
	{
		return !arg;
	}

	/// <summary>
	/// @brief 設定メニューの表示非表示切り替え
	/// </summary>
	public void ActiveMenu()
	{
		activeMenu = ChengeBool(activeMenu);
		settingMenu.SetActive(activeMenu);
		settingButten.SetActive(!activeMenu);
	}
}
