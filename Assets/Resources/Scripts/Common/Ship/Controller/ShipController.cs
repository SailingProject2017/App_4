using UnityEngine;
using System.Collections;

public class ShipController : BaseObject
{ 
    
    public override void OnUpdate()
    {
        {
            transform.position += transform.forward * -0.2f;
            //transform.Position(0, 1, 0); 
            if (Input.GetKey("right"))
            {
                transform.Rotate(0, 1, 0);
            }
            if (Input.GetKey("left"))
            {
                transform.Rotate(0, -1, 0);
            }
        }
    }
}
