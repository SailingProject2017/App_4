using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankImage : BaseObject {
    private GameObject rank;
    [SerializeField]
    private GameObject[] no=new GameObject[4];
    RankManager rankmanager;
	// Use this for initialization
	void Start () {
        Find();
        
	}

    // Update is called once per frame
    public override void OnUpdate()
    {
        no[1].SetActive(true);
        base.OnUpdate();
        RankChange();
    }
    void Find()
    {
        rank = GameObject.Find("RankManager");
        rankmanager = rank.GetComponent<RankManager>();
    }
    void RankChange()
    {
        switch (rankmanager.rankImage[3])
        {
            case 1:
                no[0].SetActive(true) ;
                no[1].SetActive(false);
                no[2].SetActive(false);
                no[3].SetActive(false);
                break;
            case 2:
                no[0].SetActive(false);
                no[1].SetActive(true);
                no[2].SetActive(false);
                no[3].SetActive(false);
                break;
            case 3:
                no[0].SetActive(false);
                no[1].SetActive(false);
                no[2].SetActive(true);
                no[3].SetActive(false);
                break;
            case 4:
                no[0].SetActive(false);
                no[1].SetActive(false);
                no[2].SetActive(false);
                no[3].SetActive(true);
                break;
        }
    }
}
