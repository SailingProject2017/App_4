/***********************************************************************/
/*! @file   NextScene.cs
*************************************************************************
*   @brief  次のシーンを登録するスクリプト
*************************************************************************
*   @author yuta takatsu
*************************************************************************
*   Copyright © 2017 yuta takatsu All Rights Reserved.
************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Scene;

public class NextScene : BaseObject
{

    protected override void AppendListConstructor()
    {
        base.AppendListConstructor();
        //RemoveSceneObject(this);
    }

    [SerializeField]
    private SCENES nextScene; // 次のシーン格納用

    public SCENES AccessNextScene { set; get; }

    public void mOnTap()
    {
        //シーン破棄時に実行される関数をオブジェクト分だけ実行
        foreach (var obj in CurrentSceneObjectList)
        {
           // Debug.Log(obj.gameObject.name);
            obj.OnEnd();
        }

        //現在のシーンのオブジェクトをすべて消去
        //RemoveSceneObjectAll();

        SceneManager.SceneMove(nextScene); // SceneManagerを呼び出す 引数は次のシーン
    }
}