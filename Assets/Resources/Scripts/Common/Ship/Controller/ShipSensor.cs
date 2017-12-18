using UnityEngine;
using System.Collections;

public class ShipSensor : BaseObject
{

    private Vector3 acceleration;
   
    private GUIStyle labelStyle;

    // Use this for initialization
    void Start()
    {
        //フォント生成
        this.labelStyle = new GUIStyle();
        this.labelStyle.fontSize = Screen.height / 22;
        this.labelStyle.normal.textColor = Color.white;
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        this.acceleration = Input.acceleration;

        //transform.rotation = Quaternion.Euler(this.acceleration.x * 10, this.acceleration.y * 10, this.acceleration.z * 10);
        transform.Rotate(0,this.acceleration.x,0);
    }

    void OnGUI()
    {
        if (acceleration != null)
        {
            float x = Screen.width / 10;
            float y = 0;
            float w = Screen.width * 8 / 10;
            float h = Screen.height / 20;

            for (int i = 0; i < 3; i++)
            {
                y = Screen.height / 10 + h * i;
                string text = string.Empty;

                switch (i)
                {
                    case 0://X
                        text = string.Format("accel-X:{0}", System.Math.Round(this.acceleration.x, 3));
                        break;
                    case 1://Y
                        text = string.Format("accel-Y:{0}", System.Math.Round(this.acceleration.y, 3));
                        break;
                    case 2://Z
                        text = string.Format("accel-Z:{0}", System.Math.Round(this.acceleration.z, 3));
                        break;
                    default:
                        throw new System.InvalidOperationException();
                }

                GUI.Label(new Rect(x, y, w, h), text, this.labelStyle);
            }
        }
    }
}
