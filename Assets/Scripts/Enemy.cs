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

    //対戦相手を引数に入れて必殺技をドーン
    public virtual void Special(ref Dragon dragon)
    {
        //仮
        dragon.HP -= 50;
    }
}
