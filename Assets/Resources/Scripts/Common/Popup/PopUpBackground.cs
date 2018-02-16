/******************************************************
 * ! @file PopUpBackground
 * ****************************************************
 * @brief ポップアップが表示、非表示するスクリプト
 *        flgで管理
 *        false→開く
 *        true→閉じる
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
    private bool Pushflg;
    void Start()
    {
        Pushflg = true;
        OnTap();

    }
    /// summary 
    /// @brief ボタンが押された時の処理 
    public void OnTap()
    {
        
        if (Pushflg == false)
        {
            BackgroundLog();
            Pushflg = true;
        }
        else if (Pushflg == true)
        {
            BackgroundLogClose();
            Pushflg = false;

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
