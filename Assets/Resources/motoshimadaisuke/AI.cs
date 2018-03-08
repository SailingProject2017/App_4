/***********************************************************************/
/*! @file  AI.cs
*************************************************************************
*   @brief  敵の行動を制御するクラス
*************************************************************************
*   @author daisuke motoshima
*************************************************************************/
using UnityEngine;

public class AI : BaseObject
{
    GetWindParam getwind;       // @brief getwindクラスを使うための変数
    GameObject WindObject;      // @brief　GameObject型で GameObject.Find用
    private enum EenemyStatus   // @brief 行動の状態を表す
    {
        eTURNING,               //旋回 
        eNORMAL,                //通常
        eACCELERATION,          //一定いかになったら加速する方向に向く
        NULL
    }
    private EenemyStatus type;                  // @brief 状態の変更を表す
    private GameObject[] targetObject;          // @brief ターゲットとなるオブジェクト
    const int objectArrayNum = 5;               // @brief 配列の数
    private float playerRadian;                 // @brief プレイヤーの角度
    private float rad = 0, deg = 0;             // @brief radはラジアン　degは角度
    private int num = 0;                        // @brief 配列の番号を示す変数
    int i;                                      // @brief 無限ループの変数
    private int cnt;                            // @brief フレームカウント用
    private int speedRandom;                    // @brief スピード変化用の乱数
    private int randMin = 2;                    // @brief 乱数の最大値
    private int randMax = 8;                    // @brief 乱数の最大値
    private float speedMin = 0.4f;              // @brief スピードの最小値
    private float speedMax = 0.6f;              // @brief スピードの最大値
    private float sppeedFluctuation = 0.001f;   // @brief スピードの変動する値   
    private Vector3 enemyPosition;              // @brief 船の場所を表すもの
    [SerializeField]
    public Vector3 aISpeed = new Vector3(0.5f, 0, 0.5f);// @brief エネミーのスピード初期値
    /// <summary>
    /// @brief最初の処理でオブジェクトを見つけて次のマーカーまでの角度を求める
    /// </summary>
    void Start()
    {
        FindObject();
        Speedchange();
        Radian();
        type = EenemyStatus.eNORMAL;
        transform.rotation = Quaternion.Euler(0, deg, 0);
    }
    public override void OnUpdate()
    {
        cnt++;
        SpeedControl();
        if (cnt > 100)
        {
            cnt = 0;
            Speedchange();
        }
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
                //Turning(WindSet());
                break;
        }
    }
    /// <summary>
    /// @brief当たった時に次のマーカーの場所を示してtype変数に次の行動を表す値を入れる
    /// </summary>
    /// <param name="Hit"></param>
    void OnTriggerEnter(Collider Hit)
    {
        //Debug.Log(Hit.tag);
        //tagの名前を使ってどのマーカーに当たったかを判断しnumに入れる
      
        
            num++;
            type = EenemyStatus.eTURNING;
        

    }
    /// <summary>
    /// @briefAtan2でラジアン値を求めてラジアン値を角度に戻している
    /// </summary>
    /// <returns></returns>
    float Radian()
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
    /// <summary>
    ///マーカーを発見しtargetObjectに入れる
    /// </summary>
    void FindObject()
    {
        targetObject = new GameObject[objectArrayNum];
        for (i = 0; i < objectArrayNum; i++)
        {
            targetObject[i] = GameObject.Find("HitMarker" + i);//+iでマーカーの番号を示して要素の数だけFindして見つける
            if (targetObject[i] == null)
            {
                //Debug.Log("null");
                targetObject[i] = GameObject.Find("Center");
            }
        }
    }
    /// <summary>
    /// @brief引数で角度を渡すとそこまで旋回する
    /// </summary>
    /// <param name="deg"></param>
    void Turning(float deg)
    {
        if (Mathf.DeltaAngle(playerRadian, deg) < 0)                    //PlayerRからdegまでの最短の角度を求めてそれが一定の値以上なら動く
        {
            transform.Rotate(new Vector3(0f, -1f, 0f));                 //一定の速度で角度を加算する
            transform.position += transform.forward * -(aISpeed.z);     //向いてる方向に進む
        }
        else if (Mathf.DeltaAngle(playerRadian, deg) > 1)               //PlayerRからdegまでの最短の角度を求めてそれが一定の値以上なら動く
        {
            transform.Rotate(new Vector3(0f, -1f, 0f));                 //一定の速度で角度を加算する
            transform.position += transform.forward * -(aISpeed.z);     //向いてる方向に進む
        }
        else
        {
            type = EenemyStatus.eNORMAL;
        }
    }
    /// <summary>
    /// 移動
    /// </summary>
    void Move()
    {
        enemyPosition = transform.position;
        enemyPosition.x += aISpeed.x * Mathf.Cos(rad);
        enemyPosition.z += aISpeed.z * Mathf.Sin(rad);
        transform.position = enemyPosition;
    }/// <summary>
     /// 風の取得
     /// </summary>
     /// <returns></returns>
    //float WindSet()
    //{
    //    WindObject = GameObject.Find("Wind");
    //    getwind = WindObject.GetComponent<GetWindParam>();
    //    return getwind.valuewind;
    //}
    /// <summary>
    /// スピード変化用の乱数を発生させてcntを初期化する
    /// </summary>
    void Speedchange()
    {

        speedRandom = Random.Range(0, 11);

    }
    /// <summary>
    /// 乱数によって決めた値に応じてスピードを変動
    /// </summary>
    void SpeedControl()
    {
        if (speedRandom < randMin)//減速
        {
            aISpeed.x -= sppeedFluctuation;
            aISpeed.z -= sppeedFluctuation;
        }
        else if (speedRandom > randMax)//加速
        {
            aISpeed.x += sppeedFluctuation;
            aISpeed.z += sppeedFluctuation;
        }
        if (aISpeed.x > speedMax)//スピードの最大値
        {
            aISpeed.x = speedMax;
            aISpeed.z = speedMax;
        }
        else if (aISpeed.x < speedMin)//スピードの最小値
        {
            aISpeed.x = speedMax;
            aISpeed.z = speedMax;
        }
    }
}

