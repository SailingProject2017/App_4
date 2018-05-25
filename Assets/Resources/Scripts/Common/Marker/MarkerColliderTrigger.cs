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
	}
    
    private void MoveMakerPoint()
    {         
        // ポイントの移動
		markerSign.transform.position = new Vector3(hitMarkerList[currentListNum].transform.position.x,
		                                            hitMarkerList[currentListNum].transform.position.y + 11,
		                                            hitMarkerList[currentListNum].transform.position.z);      
    }

    /// <summary>
    /// @brief あたり判定用メソッド
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter(Collider other)
	{
		if (hitMarkerList[currentListNum].gameObject == other.gameObject)
		{
			// goalタグのオブジェクトに接触したときに走る命令
			if (other.tag == "goal")
			{
				Singleton<GameInstance>.instance.IsGoal = true;
			}
			// markerに当たったとき次のmarkerを指すようにする
			else
			{
				currentListNum++;
				Debug.Log("マーカー:" + currentListNum);

				MoveMakerPoint();
			}
		}
    }
}