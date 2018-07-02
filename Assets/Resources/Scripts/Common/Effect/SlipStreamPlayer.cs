﻿/***********************************************************************/
/*! @file   SlipStreamPlayer.cs
*************************************************************************
*   @brief  風エフェクトを再生する
*************************************************************************
*   @author Tsuyoshi Takaguchi
************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipStreamPlayer : BaseObject {

    [SerializeField]
    private GameObject windParticle; // 生成するパーティクルオブジェクトを格納する

    [SerializeField]
    private GameObject parentObject; // 親としたいオブジェクトを格納する

    private GameObject windObject;

    private bool windActive; // エフェクトのアクティブ状態を管理する変数

    /// <summary>
    /// @brief 風エフェクトの再生
    /// </summary>
    private void PlayWindEffect()
    {       
        if (Singleton<GameInstance>.instance.IsShipMove && !windActive)
        {
            windObject = (GameObject)New(windParticle);
            windActive = true;
            windObject.transform.parent = parentObject.transform;
        }
    }

    /// <summary>
    /// @brief 風エフェクトの停止
    /// </summary>
    private void EndWindEffect()
    {
        if (Singleton<GameInstance>.instance.IsGoal)
        {
            Delete(windObject);
        }
    }

    public void Start()
    {
        windActive = false;
    }

    public void Update()
    {
        PlayWindEffect();
        EndWindEffect();
    }
}
