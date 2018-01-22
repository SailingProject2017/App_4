/******************************************************
 * ! @file PopUpBackground
 * ****************************************************
 * @brief ポップアップが表示、非表示するスクリプト
 *        スペースキーで表示、zキーで非表示
 * ****************************************************
 * @author reina sawai
 ****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PopUpBackground : BaseObject
{
  

    void Update()
    {
        ButtonDown();

    }
    /// summary 
    /// @brief ボタンが押された時の処理
    public void ButtonDown(){
    if (Input.GetKeyDown("space"))
        {
            BackgroundLog();

        }

        if ( Input.GetKeyDown("z"))
        {
            BackgroundLogClose();

        }
    }
    /// summary 
    /// @brief ポップアップが表示
    public void BackgroundLog()
    {
        transform.DOLocalMove(new Vector3(1250.0f, 2, 0), 0.3f).SetEase(Ease.InOutQuart);//指定された座標まで移動
       
    }
    /// summary 
    /// @brief ポップアップが非表示
    public void BackgroundLogClose()
     {    
          transform.DOLocalMove(new Vector3(2250, 2, 0), 0.3f).SetEase(Ease.InOutQuart);//指定された座標まで移動
    }

}
