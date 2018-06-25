﻿/**********************************************************************************************/
/*@file       ResultOpen.cs
*********************************************************************************************
* @brief      リザルト用ポップアップを開くクラス
*********************************************************************************************
* @author     Yuta Takatsu
*********************************************************************************************
* Copyright © 2018 Yuta Takatsu All Rights Reserved.
**********************************************************************************************/
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultOpen : BaseObject {

    [SerializeField]
    private GameObject resultPopup; // @brief Resultのインスタンス化

    private bool isCallOnse;

    private void Start()
    {
        isCallOnse = false;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (!isCallOnse)
        {
            if (Singleton<GameInstance>.instance.IsGoal == true)
            {

                Singleton<GameInstance>.instance.IsGoal = false;
                if (SceneManager.GetActiveScene().name == "InTutorial")
                {
                    PopupResult result = resultPopup.GetComponent<PopupResult>();
                    result.Open();
                }
                else if(SceneManager.GetActiveScene().name == "InGame")
                {

                }
                isCallOnse = true;

            }
        }
    }
}
