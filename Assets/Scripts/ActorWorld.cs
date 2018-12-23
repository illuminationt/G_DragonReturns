using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract partial class ActorWorld : MonoBehaviour
{
    //マス目を移動する時間
    [SerializeField] private float m_movingDuration;

    public bool IsMoving { get; protected set; }

    //補間アニメーションで値が狂うことが予想される。真の値を格納しておき、
    //アニメ後に真の値を代入する。
    protected int m_trueNextRotY;

    //配列上のポジション(yはダミー。コードが分かりやすくなるように。）
    public Vector3 GridPosition { get; set; }
    
    public float RotY { get { return m_trueNextRotY; } }

    protected virtual void Start()
    {
        IsMoving = false;
        m_actorStateWorld = new StateWorldIdle();
    }

    public virtual void Execute(Vector3 mapSize)
    {


        ActorStateWorld next = m_actorStateWorld.Execute(this,mapSize);
        if (next!=m_actorStateWorld)
        {
            m_actorStateWorld.Exit();
            m_actorStateWorld = next;
            m_actorStateWorld.Enter();
        }




    }

    //ドラゴンと敵の移動はここのdx,dz及びrotYを操作
    public abstract void GetMove(out int dx, out int dz, out int rotY);

    //gridPosition更新
    private void restrictMove(Vector3 mapSize,ref int dx, ref int dz)
    {
        Vector3 nextPos = new Vector3(GridPosition.x + dx, 0, GridPosition.z + dz);
        //範囲外には出れない
        if (nextPos.x < 0 || nextPos.z < 0 || nextPos.x >= mapSize.x||nextPos.z>=mapSize.z)
        {
            return;
        }

        GridPosition += new Vector3(dx, 0, dz);
    }

    //この関数に入るときはm_gridPositionには次に入るべき座標
    //transform.positionには移動前の座標が入っている
    public void smoothMove(int dx, int dz, int rotY)
    {
        if (dx != 0 || dz != 0 || rotY != 0)
        {
            StartCoroutine(stateLerp(dx, dz, rotY));
        }
    }

    IEnumerator stateLerp(int dx, int dz, int rotY)
    {
        IsMoving = true;
        float time = 0f;
        while (time < m_movingDuration)
        {
            time += Time.deltaTime;
            float rate = Time.deltaTime / m_movingDuration;
            transform.position += new Vector3(dx, 0, dz) * rate;
            transform.Rotate(new Vector3(0, rotY, 0) * rate);

            yield return null;
        }

        transform.position = GridPosition;
        transform.eulerAngles = new Vector3(0, m_trueNextRotY, 0);
        IsMoving = false;
    }

    public void SetPosition()
    {
        transform.position = GridPosition;
    }

    public abstract class ActorStateWorld
    {
        public virtual void Enter() { }
        public abstract ActorStateWorld Execute(ActorWorld actor,Vector3 mapSize);
        public virtual void Exit() { }
    }
    private ActorStateWorld m_actorStateWorld;
}