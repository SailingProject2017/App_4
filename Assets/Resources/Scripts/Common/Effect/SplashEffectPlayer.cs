/***********************************************************************/
/*! @file   SplashEffectPlayer.cs
*************************************************************************
*   @brief  水しぶきエフェクトを再生する
*************************************************************************
*   @author Tsuyoshi Takaguchi
************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashEffectPlayer : BaseObject {

    private ParticleSystem splashParticle;

    /// <summary>
    /// @brief 水しぶきエフェクトの再生
    /// </summary>
    private void PlaySplashEffect()
    {
        if (Singleton<GameInstance>.instance.IsShipMove)
        {
            splashParticle.Play();
        }
    }

    /// <summary>
    /// @brief 水しぶきエフェクトの停止
    /// </summary>
    private void EndSplashEffect()
    {
        if (Singleton<GameInstance>.instance.IsGoal)
        {
            Delete(splashParticle);
        }
    }

    public void Start()
    {
        splashParticle = this.GetComponent<ParticleSystem>();
        splashParticle.Stop();
    }

	public void Update()
    {
        PlaySplashEffect();
        EndSplashEffect();
    }
}
