using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Actor
{

    public override void DecideHand()
    {
        //仮

        if (Input.GetKeyDown(KeyCode.G))
        {
            Action = Actions.Gu;
            State = HandState.FINISH_DECIDE;
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            Action = Actions.Choki;
            State = HandState.FINISH_DECIDE;
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            Action = Actions.Par;
            State = HandState.FINISH_DECIDE;
        }
        else
        {

        }
    }

    
}
