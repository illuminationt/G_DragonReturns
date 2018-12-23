using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWorld :ActorWorld {
    public override void GetMove(out int dx, out int dz, out int rotY)
    {
        dx = dz = rotY = 0;
    }
}
