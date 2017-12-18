using UnityEngine;
using System.Collections;

public class ShipCamera : BaseObject
{

    public enum ECameraMode // 視点を表す列挙
    {
        FPS,
        TPS
    }

    [SerializeField]
    private GameObject Ship; // 追跡する船
    [SerializeField]
    private Camera Camera;      // 対象のカメラ
    [SerializeField]
    private ECameraMode cameraPerspective; // 視点

    private int layerMaskShip;  // 船のレイヤー
    private float distance;     // 船とカメラの距離
    private float cameraHeight; // カメラの高さ
    private float followSpeed = 20; // カメラのディレイスピード
    
    void Start()
    {
        layerMaskShip = 1 << LayerMask.NameToLayer("Ship"); // レイヤー情報を取得

        if (cameraPerspective == ECameraMode.FPS)
        {
            Camera.main.cullingMask &= ~layerMaskShip;// 非表示
            transform.SetPosY(1);
            transform.SetPosZ(0);
        }
        if (cameraPerspective == ECameraMode.TPS)
        {
            Camera.main.cullingMask |= layerMaskShip; // 表示
            transform.SetPosY(7);
            transform.SetPosZ(9);
        }
        //平面(X,Z)での距離を取得
        distance = Vector3.Distance(
            new Vector3(Ship.transform.position.x, 0, Ship.transform.position.z),
            new Vector3(transform.position.x, 0, transform.position.z));

        //カメラの高さの差分を取得
        cameraHeight = transform.position.y - Ship.transform.position.y;
    }

    public override void OnLateUpdate()
    {
        //カメラの位置を高さだけ、ターゲットに合わせて作成
        var current = new Vector3(
            transform.position.x,
            Ship.transform.position.y,
            transform.position.z
        );

        //チェック用の位置情報を作成(バックした時にカメラが引けるようにdistance分位置を後ろにずらす)
        var checkCurrent = current + Vector3.Normalize(current - Ship.transform.position) * distance;

        //カメラが到達すべきポイントを計算（もともとのターゲットとの差分から計算します）
        var v = Vector3.MoveTowards(
            Ship.transform.position,
            checkCurrent,
            distance);

        //カメラ位置移動(位置計算後にカメラの高さを修正）
        transform.position = Vector3.Lerp(
            current,
            v,
            Time.deltaTime * followSpeed
        ) + new Vector3(0, cameraHeight, 0);


        //カメラの角度を調整
        var newRotation = Quaternion.LookRotation(Ship.transform.position - transform.position).eulerAngles;
        if (cameraPerspective == ECameraMode.FPS)
        {
            newRotation.x = 0;
        }
        if (cameraPerspective == ECameraMode.TPS)
        {
            
            newRotation.x = 20;
        }
        newRotation.z = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(newRotation), 1);
    }
}