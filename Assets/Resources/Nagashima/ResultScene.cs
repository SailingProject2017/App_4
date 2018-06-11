using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScene : BaseObject{

    [SerializeField]
    private Text rankText;            //順位
    [SerializeField]
    private Text[] nameText;          //名前
    [SerializeField]
    private Text[] timeText;          //時間

    public void Start() {

        int[] time = { 0, 1, 2, 3 };
        string[] name = { "Name1", "Name2", "Name3", "Name4" };
        int rank = 1;
        SetRanking(name, time, rank);

    }

    public void SetRanking(string[] argPlayerName,int[] argTime,int argRank) {

        string[] ordinalNumber = { "st", "nd", "rd", "th" };    //序数

        for(int i = 0; i < nameText.Length; i++) {

            nameText[i].text = argPlayerName[i];

        }

        for(int i = 0; i < timeText.Length; i++) {

            timeText[i].text = "00:00:00";

        }

        //金 255,215,0
        //銀 192,192,192
        //銅 196,112,34
        switch(argRank) {
            case 1:
                rankText.color = new Color(255f / 255f, 215f / 255f, 0f / 255f);
                break;
            case 2:
                rankText.color = new Color(192f / 255f, 192f / 255f, 192f / 255f);
                break;
            case 3:
                rankText.color = new Color(196f / 255f, 112f / 255f, 34f / 255f);
                break;
        }
        
        rankText.text = argRank.ToString() + ordinalNumber[argRank - 1];

    }

}
