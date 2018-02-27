using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scene;
public class MarkerJuge : BaseObject
{

    [SerializeField]
    private GameObject resultPopup; // @brief Resultのインスタンス化

    [SerializeField]
    private GameObject[] markerArray; // @brief ステージ上のマーカーを登録する
    private int nuwArrayNumver; // @brief 現在の配列番号

    private void Start()
    {
        nuwArrayNumver = 0;
    }

    /// <summary>
    /// @brief あたり判定用メソッド
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {

        // goalタグのオブジェクトに接触したときに走る命令
        if (other.tag == "Goal")
        {
            if (Singleton<TutorialState>.instance.TutorialStatus != eTutorial.eTutorial_End)
            {
                PopupResult result = resultPopup.GetComponent<PopupResult>();
                result.Open();

                //SceneManager.SceneMove(nextScene);

            }
        }

        // markerに当たったとき次のmarkerを指すようにする
        if (markerArray[nuwArrayNumver] == other)
        {
            nuwArrayNumver++;
        }       
    }
}