/**************************************************************
 
 作成者: 沢井　伶奈
 
 作成内容: 風を乱数化し、取得させる。
 　　　　　0度から360度の範囲にさせ、数値を一つ決定させる。

 関数: random→風の数値をランダム
       valuewind→値の制限

 意味: r_Wind→ランダムの風
 　　  l_valuewind→getで渡す際の変数名
     
 ***********************************************************/
using System;
using UnityEngine;

public class GetWindParam : BaseObject
{
    //風の乱数入れる箱
    public int r_Wind;

    [Range(0, 360)]
    private float l_valuewind = 0;

    private int random(object r_Wind)
    {
        throw new NotImplementedException();
    }



    void Start()
    {
        Random();
    }


    //風ランダムで一つ表示させるための関数
    public void Random()
    {
        //-180~180までの値をランダムで出す
        r_Wind = UnityEngine.Random.Range(-180, 180);
        Debug.Log(r_Wind);
        
        // 値を制限
        
        //180度以上にしない
        if (r_Wind >= 180)
        {
            l_valuewind = r_Wind - 180;
        }
        //-180度以下にしない
        else if (r_Wind < -180)
        {
            l_valuewind = 180 + r_Wind;
        }
        //そのまま
        else
            l_valuewind = r_Wind;
    }

   
    public float Valuewind
    {
        get { return l_valuewind; }
        
    }
}
