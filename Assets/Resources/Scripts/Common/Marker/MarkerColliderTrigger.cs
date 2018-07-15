/**********************************************************************************************/
/*@file       MarkerJuge.cs
*********************************************************************************************
* @brief      markerの当たった時の処理を扱う
*********************************************************************************************
* @author     Yuta Takatsu
*********************************************************************************************
* Copyright © 2018 Yuta Takatsu & Ryo Sugiyama All Rights Reserved.
**********************************************************************************************/
using UnityEngine;
public class MarkerColliderTrigger : MarkerBase
{
    [SerializeField]
    private GameObject markerSign; // @brief 今目指すべきマーカーを示す矢印



	protected override void MarkerInitialize()
	{
		base.MarkerInitialize();
		MoveMakerPoint();
		currentMarker = 0;
	}
    
    private void MoveMakerPoint()
    {         
        // ポイントの移動
		markerSign.transform.position = new Vector3(hitMarkerList[currentHitMarker].transform.position.x,
		                                            hitMarkerList[currentHitMarker].transform.position.y + 11,
		                                            hitMarkerList[currentHitMarker].transform.position.z);      
    }

    /// <summary>
    /// @brief あたり判定用メソッド
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter(Collider other)
	{
		if (hitMarkerList[currentHitMarker].gameObject == other.gameObject)
		{
            // goalタグのオブジェクトに接触したときに走る命令
            if (other.tag == "goal")
            {
                Singleton<GameInstance>.instance.IsGoal = true; // ゲーム全体で管理しているフラグ
                isGoal = true;                                  // ランクで管理しているフラグ
                Singleton<SoundPlayer>.instance.PlaySE("Goal");
            }
            // markerに当たったとき次のmarkerを指すようにする
            else
            {
                currentHitMarker++;
                MoveMakerPoint();
                Singleton<SoundPlayer>.instance.PlaySE("PassedMarker");
                // エフェクトの再生
                BaseObjectSingleton<EffectManager>.Instance.PlayEffect("PassedMarker", other.transform.position, other.transform.rotation, other.transform.localScale);
            }
		}
        // わかりやすくするために別でif文かけてます
	    if(other.gameObject == hitMarkerList[currentMarker].gameObject && other.tag != "goal")
		{
			currentMarker++;
		}

    }
}