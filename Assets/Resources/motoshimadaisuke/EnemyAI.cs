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
    GetWind getwind;
    private float PlayerRadian;
    // ターゲットとなるオブジェクト
    private GameObject[] targetObject;
    // ラジアン変数
    private float rad = 0, deg = 0;//radはラジアン　degは角度
    // 現在位置を代入する為の変数
    private Vector3 EnemyPosition;
    // Use this for initialization
    private int num = 0;//配列の番号を示す変数
    private EenemyStatus type;//1は旋回2は通常移動
    int i;//無限ループの変数、
    const int ObjectArrayNum = 3;//配列の数
    [SerializeField]
    public Vector3 AI_Speed = new Vector3(0.1f, 0, 0.1f);//エネミーのスピード
    void Start()
    {
         
        FindObject();
        Radian();
        type = EenemyStatus.eNORMAL;//初期に通常の移動をするようにする
        transform.rotation = Quaternion.Euler(0, deg, 0);//初期角度を入れる
    }

    // Update is called once per frame
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
                Turning();
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
        PlayerRadian = transform.localEulerAngles.y;
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
        targetObject = new GameObject[ObjectArrayNum];
        for (i = 0; i < ObjectArrayNum; i++)
        {
            targetObject[i] = GameObject.Find("HitMarker" + i);//+iでマーカーの番号を示して要素の数だけFindして見つける

            if (targetObject[i] == null)
            {
                Debug.Log("null");
                targetObject[i] = GameObject.Find("Center");
            }
            Debug.Log(targetObject[i].transform.position);
        }
    }
    void Turning(float deg)//呼び出したときに一回角度を求めてその角度まで旋回する
    {
        if (Mathf.DeltaAngle(PlayerRadian, deg) > 1f)//PlayerRからdegまでの最短の角度を求めてそれが一定の値以上なら動く
        {
            // Debug.Log(Mathf.DeltaAngle(PlayerR, deg));

            transform.Rotate(new Vector3(0f, 1f, 0f));//一定の速度で角度を加算する
            transform.position += transform.forward * -(AI_Speed.z);//向いてる方向に進む
        }
        else
        {

            type = EenemyStatus.eNORMAL;
        }
    }
    void Move()//Enemyの移動
    {
        // 現在位置をPositionに代入
        //transform.position += transform.forward * -0.2f;

        // Debug.Log("num:" + num);
        EnemyPosition = transform.position;

        // これで特定の方向へ向かって進んでいく。

        EnemyPosition.x += AI_Speed.x * Mathf.Cos(rad);
        EnemyPosition.z += AI_Speed.z * Mathf.Sin(rad);
        // 現在の位置に加算減算を行ったPositionを代入する
        transform.position = EnemyPosition;
    }
    float WindSet
    {

    }
}

