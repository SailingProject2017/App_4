using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalUI : BaseObject {

    [SerializeField]
    private GameObject goalUI;

	// Use this for initialization
	void Start () 
    {
        goalUI.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(Singleton<GameInstance>.instance.IsGoal) 
        {
            goalUI.gameObject.SetActive(true);
        }
        else
        {
            goalUI.gameObject.SetActive(false);
        }
	}
}
