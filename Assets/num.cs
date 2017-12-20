/******************************************************
 * 作成者：沢井伶奈
 * 作成日:2017.12.19
 * 内容　：ポップアップが横から出てくるアニメーション
 * まだ仮置き
 ****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class num : BaseObject
{
  
    void Start()
    {
       
    }

    void Update()
    {
        ButtonDown();

    }
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

    public void BackgroundLog()
    {
        transform.DOLocalMove(new Vector3(169.0f, 2, 0), 0.3f).SetEase(Ease.InOutQuart);
        // me.rectTransform.DOAnchorPosX(movedPos.x, 2.0f).SetEase(Ease.InOutBack);
    }

    public void BackgroundLogClose()
     {    
          transform.DOLocalMove(new Vector3(434.0f, 2, 0), 0.3f).SetEase(Ease.InOutQuart);
    }

}
