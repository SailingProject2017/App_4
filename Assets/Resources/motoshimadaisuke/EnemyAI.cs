/***********************************************************************/
/*! @file   EnemyAI.cs
*************************************************************************
*   @brief  敵の行動を制御するクラス
*************************************************************************
*   @author daisuke motoshima
*************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : BaseObject {
    private enum EenemyStatus {
        eTURNING,
        eNORMAL,
        eACCELERATION,
        NULL
    }
    private GameObject[] targetObject;// ターゲットとなるオブジェクト
    GetWindParam getwind;
    GameObject WindObject;
    const int objectArrayNum = 5;//配列の数
    private float playerRadian;
    private float rad = 0, deg = 0;//radはラジアン　degは角度
    private int num = 0;//配列の番号を示す変数
    private EenemyStatus type;
    int i;//無限ループの変数、
    private Vector3 enemyPosition;
    [SerializeField]
    public Vector3 aISpeed = new Vector3(0.1f, 0, 0.1f);//エネミーのスピード
    void Start()
    {
        FindObject();
        Radian();
        type = EenemyStatus.eNORMAL;//初期に通常の移動をするようにする
        transform.rotation = Quaternion.Euler(0, deg, 0);//初期角度を入れる
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        switch (type)
        {
            case EenemyStatus.eNORMAL:
                Move();
                break;
            case EenemyStatus.eTURNING:
                Turning(Radian());
                break;
            case EenemyStatus.eACCELERATION:
                Turning(WindSet());
                break;
        }
    }
    void OnTriggerEnter(Collider Hit)//当たった時に次のマーカーの場所を示してtype変数に次の行動を表す値を入れる
    {
        //Debug.Log(Hit.tag);
        //tagの名前を使ってどのマーカーに当たったかを判断しnumに入れる
        if (Hit.tag == "first")
        {  
                num ++;
            type = EenemyStatus.eTURNING;
        }
        if (Hit.tag == "ship")
        {
            Turning(30);
        }
    }
    float Radian()//Atan2でラジアン値を求めてラジアン値を角度に戻している
    {
        playerRadian = transform.localEulerAngles.y;
        // ラジアン
        // atan2(目標方向のy座標 - 初期位置のy座標, 目標方向のx座標 - 初期位置のx座標)
        // これでラジアンが出る。
        // このラジアンをCosやSinに使い、特定の方向へ進むことが出来る。
        rad = Mathf.Atan2(
                targetObject[num].transform.position.z - transform.position.z,
                targetObject[num].transform.position.x - transform.position.x);
        deg = rad * Mathf.Rad2Deg + 87;//ラジアン値を角度に変更
       return deg *= -1;//+-を反転
    }
    void FindObject()//マーカーを発見しtargetObjectに入れる
    {
        targetObject = new GameObject[objectArrayNum];
        for (i = 0; i < objectArrayNum; i++)
        {
            targetObject[i] = GameObject.Find("HitMarker" + i);//+iでマーカーの番号を示して要素の数だけFindして見つける
            if (targetObject[i] == null)
            {
                Debug.Log("null");
                targetObject[i] = GameObject.Find("Center");
            }
        }
    }
    void Turning(float deg)//呼び出したときに一回角度を求めてその角度まで旋回する
    {
        if (Mathf.DeltaAngle(playerRadian, deg) < 0)//PlayerRからdegまでの最短の角度を求めてそれが一定の値以上なら動く
        {
            transform.Rotate(new Vector3(0f, -1f, 0f));//一定の速度で角度を加算する
            transform.position += transform.forward * -(aISpeed.z);//向いてる方向に進む
        }
        else if (Mathf.DeltaAngle(playerRadian, deg) > 1)//PlayerRからdegまでの最短の角度を求めてそれが一定の値以上なら動く
        {
            transform.Rotate(new Vector3(0f, -1f, 0f));//一定の速度で角度を加算する
            transform.position += transform.forward * -(aISpeed.z);//向いてる方向に進む
        }
        else
        {
            type = EenemyStatus.eNORMAL;
        }
    }
    void Move()//Enemyの移動
    {
        enemyPosition = transform.position;
        enemyPosition.x += aISpeed.x * Mathf.Cos(rad);
        enemyPosition.z += aISpeed.z * Mathf.Sin(rad);
        transform.position = enemyPosition;
    }
    float WindSet()
    {
        WindObject=GameObject.Find("Wind");
        getwind = WindObject.GetComponent<GetWindParam>();
        return getwind.valuewind;
    }
}

