/**************************************************************************************/
/*! @file   TutorialEvent.cs
***************************************************************************************
@brief      チュートリアルのイベント発生時に呼ばれる
***************************************************************************************
@author     Yuta Takatsu
***************************************************************************************
* Copyright © 2017 Yuta Takatsu All Rights Reserved.
***************************************************************************************/
using System.Collections.Generic;
using UnityEngine;

public class TutorialEvent : BaseObject {

    [SerializeField]
    int eventId = 0; // @brief イベントID　順番に登録され再生される
    [SerializeField]
    bool isOneTimeOnly = false; // @brief そのイベントのみを再生させるかどうか
    [SerializeField]
    List<GameObject> animations; // @brief 一つのイベント内のデータをリストで管理

    private bool used = false; // フラグ格納

    /// <summary>
    /// @brief アニメーションIDのアクセサー
    /// </summary>
    public int AnimationId
    {
        get;
        set;
    }

    /// <summary>
    /// @brief イベントデータリストのアクセサー
    /// </summary>
    public List<GameObject> Animations
    {
        get { return animations; }
    }

    /// <summary>
    /// @brief イベントのコールバック関数アクセサー
    /// </summary>
    public System.Action<int> EventCallback
    {
        private get;
        set;
    }

    protected override void AppendListConstructor()
    {
        base.AppendListConstructor();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if((!used && isOneTimeOnly))
        {
            EventCallback.Invoke(eventId);

            used = isOneTimeOnly ? true : false;
        }
    }

    /// <summary>
    /// @brief イベント開始時の処理
    /// </summary>
    public void BeginEvent()
    {
       
    }
    /// <summary>
    /// @brief イベント終了時の処理
    /// </summary>
    public void ExitEvent()
    {
        
    }
}
