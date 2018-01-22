using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Scene;

public class GoalJudge : BaseObject
{
    private int num;//@briefマーカーの通った数
    const byte  makerNum=4;//@briefマーカーの数
    public string markerName;
    [SerializeField]
    SCENES nextScene;
    /// <summary>
    /// ゴールの判定をするスクリプト
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        if (markerName == "HitMarker" + num)
            {
                num++;

            }
        //Debug.Log("hit");
        if (other.tag == "goal"&&num==makerNum)
        {
            SceneManager.SceneMove(nextScene);
        }
    }
}