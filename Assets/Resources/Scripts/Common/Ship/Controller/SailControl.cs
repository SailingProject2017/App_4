/***********************************************************************/
/*! @file   SailControl.cs
*************************************************************************
*   @brief  風と進行方向に対して最適な帆の角度を算出して移動させるクラス
*************************************************************************
*   @author Ryo Sugiyama
*************************************************************************
*   Copyright © 2017 Ryo Sugiyama All Rights Reserved.
************************************************************************/
using UnityEngine;

public class SailControl : BaseObject {


    private GameObject player;              // @brief 船オブジェクトを格納する変数
    private GameObject sail;                // @brief 船のセールオブジェクトを格納する変数
	private GameObject human;               // @brief 人オブジェクトを格納する変数
	private GameObject moveCircle;          // @brief 
	private ShipController shipController;  // @brief 
	private GetWindParam windVec;           // @brief 
    

	private float sailRotate;     // @brief 

	[SerializeField]
	private float minSpeed;       // @brief 

    [SerializeField]
	private float maxSpeed;       // @brief 

	private float constantValue;  // @brief 

    private void Start()
    {
    
		player = GameObjectExtension.Find("Player");
		sail = GameObjectExtension.Find("Sail");
		human = GameObjectExtension.Find("Human");
		moveCircle = GameObjectExtension.Find("Circle");
		shipController = gameObject.GetComponent<ShipController>();
		windVec = GameObjectExtension.Find("UIWind").GetComponent<GetWindParam>();


		minSpeed = 10;
		maxSpeed = 60;

		constantValue = (maxSpeed - minSpeed) / 180;

		CircleChangeRotate();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
		constantValue = (maxSpeed - minSpeed) / 180;
		SailRotate(windVec.ValueWind, player.transform.localEulerAngles.y);
		CircleMove();

    }

    /// <summary>
    /// @brief 風と進行方向に対して最適な帆の角度を算出して移動させる
    /// </summary>
	/// <param name="windVector"> 風のベクトルの方向 </param>
    /// <param name="playerRotate"> プレイヤーが進行しているベクトルの方向 </param>
    private void SailRotate(float windVector, float playerRotate)
    {
  
		playerRotate -= 180;

		if(playerRotate >= windVector + 45)
		{
			sailRotate = 10 + ((playerRotate - 45) * 0.5925f);
			shipController.Speed = Mathf.Abs(10 + ((playerRotate - 45) * constantValue));
		}
		if (playerRotate <= windVector - 45)
        {
<<<<<<< HEAD
			sailRotate = -10 + ((playerRotate + 45) * 0.5925f);
			shipController.Speed = Mathf.Abs(10 + ((playerRotate - 45) * constantValue));
=======
			sailRotate = -10 + ((playerRotate + ableMoveDegree) * 0.5925f);
			curMaxSpeed = Mathf.Abs(10 + ((playerRotate - ableMoveDegree) * (50 - 10) / 180));
>>>>>>> origin/Player
        }
     
		sail.transform.localEulerAngles = new Vector3(0, sailRotate, 0);

    }

    /// <summary>
    /// @brief 
    /// </summary>
    public void CircleMove()
	{
		moveCircle.transform.position = player.transform.position;
	}

    /// <summary>
    /// @brief 
    /// </summary>
    public void CircleChangeRotate()
	{
		moveCircle.transform.eulerAngles = new Vector3(90, windVec.ValueWind * -1, 0);
	}

    /* セールの角度を算出する計算式について */

    // 風向きを0°とする
	// 船が風向きに対して進める角度は、45° ~ 180° : -45° ~ -180° である。
    // 自艇の角度が45°の時、セールの角度は10°である
	// 自艇の角度が180°の時、セールの角度は90°である
    // この時、自艇の角度1°大きくなるたびにセールの角度は0.5925大きくなる
    //
    // よって、自艇の角度から、セールの角度を求める式は、
    // 自艇の角度をx、セールの角度をyとする
    //
    // 自艇の角度xが、x < 45 ではない時
	// y =  10 + ((x - 45) * 0.5925)
	// 自艇の角度xが、x < -45 ではない時
	// y = -10 + ((x + 45) * 0.5925)
    // となる


}
