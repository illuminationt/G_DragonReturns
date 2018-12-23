using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BattleManager : MonoBehaviour
{

    [SerializeField] private Dragon m_dragon;
    [SerializeField] private Enemy m_enemy;

    private void Start()
    {
        m_battleState = new StateBeforeBattle();

        //ここで戦うDragonとEnemyをGameManagerから受け取る。
        //m_dragon=....

    }

    private void Update()
    {
        BattleState next = m_battleState.Execute(m_dragon, m_enemy);
        if (next != m_battleState)
        {
            m_battleState.Exit(m_dragon, m_enemy);
            m_battleState = next;
            m_battleState.Enter(m_dragon, m_enemy);
        }
    }






    public abstract class BattleState
    {
        //BattleManagerが保持してる、現在戦っているDragonとEnemyを渡す
        public virtual void Enter(Dragon dragon, Enemy enemy) { }
        public abstract BattleState Execute(Dragon dragon, Enemy enemy);
        public virtual void Exit(Dragon dragon, Enemy enemy) { }


        protected Actor getWinner(Dragon dragon, Enemy enemy)
        {
            if (dragon.IsWinner)
            {
                return dragon;
            }
            else if (enemy.IsWinner)
            {
                return enemy;
            }
            else
            {
                return null;
            }
        }

    }
    private BattleState m_battleState;
}
