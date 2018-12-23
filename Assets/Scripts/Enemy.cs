using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{

    public override void DecideHand()
    {
        if (State != HandState.DONT_DECIDE)
        {
            return;
        }

        //仮
        Action = Actions.Gu;
        State = HandState.FINISH_DECIDE;
    }
}
