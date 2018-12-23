using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonWorld : ActorWorld {

    public override void GetMove(out int dx, out int dz, out int rotY)
    {
        dx = dz = rotY = 0;
        if (IsMoving)
        {
            return;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rotY = -90;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rotY = 90;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            rotY = 180;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            float rad = Mathf.Deg2Rad * transform.eulerAngles.y;
            dx = (int)Mathf.Sin(rad);
            dz = (int)Mathf.Cos(rad);
        }
    }
}
