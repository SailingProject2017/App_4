using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scene;
public class MarkerJuge2 : BaseObject
{
    [SerializeField]
    SCENES nextScene;
    public bool firstHit;//1番目のヒット判定
    public bool secondHit;//2番目のヒット判定
    public int Hitcnt;//ヒットした数を数える

    private GameObject Line;

    void Start()
    {
        firstHit = false;
        secondHit = false;
        Hitcnt = 0;

        Line = GameObject.Find("Line");
    }


    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("hit");
        if (other.tag == "first" && Hitcnt == 0)//firstのところにあたり判定のtagの名前を入れる
        {
            firstHit = true;
        }
        if (other.tag == "second" && Hitcnt == 1)//firstのところにあたり判定のtagの名前を入れる
        {
            firstHit = true;
        }
        if (other.tag == " third" && Hitcnt == 2)//firstのところにあたり判定のtagの名前を入れる
        {
            firstHit = true;
        }
        if (other.tag == "through")//secondのところにあたり判定の名前を入れる
        {
            if (firstHit == true)
            {
                secondHit = true;
            }
        }
        if (firstHit == true && secondHit == true)
        {
            Hitcnt += 1;
            firstHit = false;
            secondHit = false;
            Debug.Log(Hitcnt);
            Line.tag = "goal";


        }
        if (other.tag == "goal" && Hitcnt == 3)
        {
            SceneManager.SceneMove(nextScene);
        }
    }
}
