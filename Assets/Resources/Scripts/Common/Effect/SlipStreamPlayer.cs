/***********************************************************************/
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
    private GameObject WindParticle; // 生成するパーティクルオブジェクトを格納する

    [SerializeField]
    private GameObject ParentObject; // 親としたいオブジェクトを格納する

    private GameObject WindObject;

    private bool WindActive; // エフェクトのアクティブ状態を管理する変数

    /// <summary>
    /// @brief 風エフェクトの再生
    /// </summary>
    private void PlayWindEffect()
    {       
        if (Singleton<GameInstance>.instance.IsShipMove && !WindActive)
        {
            WindObject = (GameObject)New(WindParticle);
            WindActive = true;
            WindObject.transform.parent = ParentObject.transform;
        }
    }

    /// <summary>
    /// @brief 風エフェクトの停止
    /// </summary>
    private void EndWindEffect()
    {
        if (Singleton<GameInstance>.instance.IsGoal)
        {
            Delete(WindObject);
        }
    }

    public void Start()
    {
        WindActive = false;
    }

    public void Update()
    {
        PlayWindEffect();
        EndWindEffect();
    }
}
