/***********************************************************************/
/*! @file   EffectManager.cs
*************************************************************************
*   @brief  エフェクトを管理するスクリプト
*************************************************************************
*   @author Tsuyoshi Takaguchi
************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : BaseObjectSingleton<EffectManager> {

    [SerializeField]
    private GameObject[] effectObject; // 再生をしたいエフェクトを格納する配列

    private class EffectInfo
    {

        public string resourceName;
        public GameObject effectObject;
        
        // コンストラクタ
        public EffectInfo(string resourceName, GameObject effectObject)
        {
            this.resourceName = resourceName;
            this.effectObject = effectObject;
        }
    }

    private Dictionary<string, EffectInfo> effectClips;

    /// <summary>
    /// @brief エフェクトの初期化関数
    /// </summary>
    private void EffectInitialize()
    {
        effectClips = new Dictionary<string, EffectInfo>();
   
        effectClips.Add("Tap", new EffectInfo("TapEffect", effectObject[(int)eEffectType.eTap_Effect]));
        effectClips.Add("PassedMarker", new EffectInfo("PassedMarkerEffect", effectObject[(int)eEffectType.ePassedMarker_Effect]));
    }

    /// <summary>
    /// @brief エフェクトの再生関数
    /// </summary>
    /// <param name="effectName"></param>
    /// <param name="EffectPosition"></param>
    public void PlayEffect(string effectName, Vector3 EffectPosition, Quaternion EffectRotate)
    {
        if(effectClips.ContainsKey(effectName) == false)
        {
            return;
        }
        // エフェクトオブジェクトを生成し一定時間後に削除する
        Destroy(Instantiate(effectClips[effectName].effectObject, EffectPosition, EffectRotate), 1.2f);
    }

    /// <summary>
    /// @brief 全てのエフェクトの停止関数
    /// </summary>
    public void EndEffectAll()
    {
        foreach (KeyValuePair<string, EffectInfo> pair in effectClips)
        {
            Destroy(pair.Value.effectObject);
        }
    }

    public void Start()
    {
        EffectInitialize();
    }
}