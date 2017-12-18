using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Scene;

public class GoalJudge : BaseObject
{


    [SerializeField]
    SCENES nextScene;

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("hit");
        if (other.tag == "goal")
        {
            SceneManager.SceneMove(nextScene);
        }
    }
}