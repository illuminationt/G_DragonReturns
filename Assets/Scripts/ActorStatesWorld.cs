using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class ActorWorld :MonoBehaviour {

    protected class StateWorldIdle:ActorStateWorld
    {
        public override void Enter()
        {

        }
        public override ActorStateWorld Execute(ActorWorld actor,Vector3 mapSize)
        {
            // Debug.Log("update : StateIdle");
            ActorStateWorld next = this;


            if (!actor.IsMoving)
            {
                int dx = 0, dz = 0, rotY = 0;
                actor.GetMove(out dx, out dz, out rotY);
                actor.restrictMove(mapSize,ref dx, ref dz);

                if (dx != 0 || dz != 0 || rotY != 0)
                {
                    actor.m_trueNextRotY += rotY;
                    actor.smoothMove(dx, dz, rotY);
                }
            }

            if (actor.IsMoving)
            {
                next = new StateWorldMove();
            }

            return next;
        }
    }

    protected class StateWorldMove:ActorStateWorld
    {
        public override void Enter()
        {

        }

        //mapSizeは使わない
        public override ActorStateWorld Execute(ActorWorld actor,Vector3 mapSize)
        {
            //Debug.Log("update : StateMove");
            ActorStateWorld next = this;

            if (!actor.IsMoving)
            {
                next = new StateWorldIdle();
            }

            return next;
        }
    }
}
