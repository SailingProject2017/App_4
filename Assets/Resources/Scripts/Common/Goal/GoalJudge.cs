/**************************************************************************************/
/*! @file   GoalJudge.cs
***************************************************************************************
@brief      ステージをクリアしたときに呼ばれる関数をまとめたクラス
***************************************************************************************
@author     yuta takatsu
***************************************************************************************
* Copyright © 2018 yuta takatsu All Rights Reserved.
***************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Scene;

public class GoalJudge : BaseObject
{


    //[SerializeField]
    //private SCENES nextScene; // @brief SCENESのインスタンス化
    [SerializeField]
    private GameObject resultPopup; // @brief Resultのインスタンス化

    
    /// <summary>
    /// @brief あたり判定用メソッド
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        // goalタグのオブジェクトに接触したときに走る命令
        if (other.tag == "Ship")
        {
            if (Singleton<TutorialState>.instance.TutorialStatus != eTutorial.eTutorial_End)
            {
                PopupResult result = resultPopup.GetComponent<PopupResult>();
                result.Open();

                //SceneManager.SceneMove(nextScene);

            }
        }
    }
#if(DEBUG)

    /// <summary>
    /// @brief チュートリアルが終わった判定をだすメソッド(デバッグ用)
    /// </summary>
    public void OnTap()
    {
        if (Singleton<TutorialState>.instance.TutorialStatus != eTutorial.eTutorial_End)
        {
            //resultPopup.Open();
        }
    }
#endif
}