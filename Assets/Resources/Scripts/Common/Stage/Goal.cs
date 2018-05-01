using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scene;

public class Goal : BaseObject {

    [SerializeField]
    private SCENES nextScene; // @brief 次のシーン格納用

    private bool flag = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ship")
        {
            flag = true;
        }
    }

    public override void OnUpdate()
    {
        if(Singleton<GameInstance>.instance.IsGoal && flag)
        {
            SceneManager.SceneMove(nextScene);
            flag = false;
        }
    }
}