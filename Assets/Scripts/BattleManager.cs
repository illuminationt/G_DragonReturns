using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BattleManager : MonoBehaviour
{
    [SerializeField] private Dragon m_dragon;
    [SerializeField] private Enemy m_enemy;
    [SerializeField] private BattleMenu m_menu;

    public SceneSequencer Sequencer;

    private void Start()
    {
        m_battleState = new StateBeforeBattle();
        Sequencer = GameObject.Find("SceneSequencer").GetComponent<SceneSequencer>();
        //ここで戦うDragonとEnemyをGameManagerから受け取る。
        //m_dragon=....

    }

    private void Update()
    {
        BattleState next = m_battleState.Execute(m_dragon, m_enemy,m_menu);
        if (next != m_battleState)
        {
            m_battleState.Exit(m_dragon, m_enemy,m_menu);
            m_battleState = next;
            m_battleState.Enter(m_dragon, m_enemy,m_menu);
        }
    }






    public abstract class BattleState
    {
        //BattleManagerが保持してる、現在戦っているDragonとEnemyを渡す
        public virtual void Enter(Dragon dragon, Enemy enemy, BattleMenu menu) { }
        public abstract BattleState Execute(Dragon dragon, Enemy enemy,BattleMenu menu);
        public virtual void Exit(Dragon dragon, Enemy enemy, BattleMenu menu) { }


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
