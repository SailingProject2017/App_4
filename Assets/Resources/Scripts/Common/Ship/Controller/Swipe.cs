using UnityEngine;
using System.Collections;

public class Swipe : BaseObject
{

    [SerializeField]
    private GameObject obj;


    Vector2 sPos;   //タッチした座標
    Quaternion sRot;//タッチしたときの回転
    float wid, hei, diag;  //スクリーンサイズ
    float tx, ty;    //変数

    protected override void AppendListConstructor()
    {
        base.AppendListConstructor();
        wid = Screen.width;
        hei = Screen.height;
        diag = Mathf.Sqrt(Mathf.Pow(wid, 2) + Mathf.Pow(hei, 2));
    }


    public override void OnUpdate()
    {
        if (Input.touchCount == 1)
        {
            //回転
            Touch t1 = Input.GetTouch(0);
            if (t1.phase == TouchPhase.Began)
            {
                sPos = t1.position;
                sRot = obj.transform.rotation;
            }
            else if (t1.phase == TouchPhase.Moved || t1.phase == TouchPhase.Stationary)
            {
                tx = (t1.position.x - sPos.x) / wid; //横移動量(-1<tx<1)
                //ty = (t1.position.y - sPos.y) / hei; //縦移動量(-1<ty<1)
                obj.transform.rotation = sRot;
                obj.transform.Rotate(new Vector3(90 * ty, -90 * tx, 0));
                
            }
        }
    }
}