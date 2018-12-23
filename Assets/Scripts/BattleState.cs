﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BattleManager : MonoBehaviour
{
    //ジャンケンの勝者を保持しておく変数
    //まだ決まってないor 値が不要になったらUnknownを代入しておくこと。エラーチェックになる


    //バトル始まる前？一応用意した。使わないかも。
    public class StateBeforeBattle : BattleState
    {
        public override BattleState Execute(Dragon dragon, Enemy enemy)
        {
            BattleState next = this;

            if (Input.GetKeyDown(KeyCode.Z))
            {
                //仮
                next = new StateDecideHand();
            }

            return next;
        }
    }

    //プレイヤーがジャンケンの手を考えている状態。
    public class StateDecideHand : BattleState
    {


        public override void Enter(Dragon dragon, Enemy enemy)
        {
            Debug.Log("手を決めてください...G or C or P");
        }

        public override BattleState Execute(Dragon dragon, Enemy enemy)
        {
            BattleState next = this;

            //両者の手を決める
            dragon.DecideHand();
            enemy.DecideHand();

            //両者ともに手を出し終わった。アニメーション（アクション）へGo
            if (dragon.State == Actor.HandState.FINISH_DECIDE &&
                enemy.State == Actor.HandState.FINISH_DECIDE)
            {
                dragon.State = enemy.State = Actor.HandState.DONT_DECIDE;
                next = new StateAction();
            }



            return next;
        }

        //両者の手が出し終わった。まず勝者の判別
        //死んでるかとかのチェックはアニメーションが終わってから。
        public override void Exit(Dragon dragon, Enemy enemy)
        {
            CalculateDamage(ref dragon, ref enemy);
            Debug.Log("Dragon : " + dragon.Action + " Enemy : " + enemy.Action);
        }


        //上のExit()のみで使う
        //dragon,enemyを入れてダメージ計算
        /*
        private void judgeWinner(ref Dragon dragon,ref Enemy enemy)
        {
            //ジャンケンの勝者判別
            switch (dragon.Action)
            {
                case Actor.Actions.Gu:
                    switch (enemy.Action)
                    {
                        case Actor.Actions.Gu:m_winner = Winner.Niether;  ; break;
                        case Actor.Actions.Choki:m_winner = Winner.Dragon;winAction = Actor.Actions.Gu; break;
                        case Actor.Actions.Par:m_winner = Winner.Enemy;   winAction = Actor.Actions.Par; break;
                        default:Debug.LogError("やばい"); winAction = Actor.Actions.Error; break;
                    }
                    break;
                case Actor.Actions.Choki:
                    switch (enemy.Action)
                    {
                        case Actor.Actions.Gu:m_winner = Winner.Enemy;     winAction = Actor.Actions.Gu; break;
                        case Actor.Actions.Choki:m_winner = Winner.Niether;winAction = Actor.Actions.Unknown; break;
                        case Actor.Actions.Par:m_winner = Winner.Dragon;   winAction = Actor.Actions.Choki; break;
                        default: Debug.LogError("やばい"); winAction = Actor.Actions.Error; break;
                    }
                    break;
                case Actor.Actions.Par:
                    switch (enemy.Action)
                    {
                        case Actor.Actions.Gu:m_winner = Winner.Dragon;  winAction = Actor.Actions.Par; break;
                        case Actor.Actions.Choki:m_winner = Winner.Enemy;winAction = Actor.Actions.Choki; break;
                        case Actor.Actions.Par:m_winner = Winner.Niether;winAction = Actor.Actions.Unknown; break;
                        default: Debug.LogError("やばい"); winAction = Actor.Actions.Error; break;
                    }
                    break;
                default:
                    Debug.LogError("変な手が代入されているよ");
                    winAction = Actor.Actions.Error;
                    break;
            }
            */

        //この中でダメーCジを計算してしまう
        private void CalculateDamage(ref Dragon dragon, ref Enemy enemy)
        {
            dragon.IsWinner = enemy.IsWinner = false;
            //ジャンケンの勝者判別
            switch (dragon.Action)
            {
                case Actor.Actions.Gu:
                    switch (enemy.Action)
                    {
                        case Actor.Actions.Gu:   ; break;
                        case Actor.Actions.Choki:dragon.IsWinner = true; enemy.HP -= dragon.AttackGu; break;
                        case Actor.Actions.Par:  enemy.IsWinner = true; dragon.HP -= enemy.AttackPar; break;
                        case Actor.Actions.Special:enemy.IsWinner = true;break;
                        default: Debug.LogError("やばい"); break;
                    }
                    break;
                case Actor.Actions.Choki:
                    switch (enemy.Action)
                    {
                        case Actor.Actions.Gu:   enemy.IsWinner = true; dragon.HP -= enemy.AttackGu; break;
                        case Actor.Actions.Choki: break;
                        case Actor.Actions.Par:  dragon.IsWinner = true; enemy.HP -= dragon.AttackChoki; break;
                        default: Debug.LogError("やばい"); break;
                    }
                    break;
                case Actor.Actions.Par:
                    switch (enemy.Action)
                    {
                        case Actor.Actions.Gu:   dragon.IsWinner = true; enemy.HP -= dragon.AttackPar; break;
                        case Actor.Actions.Choki:enemy.IsWinner = true; dragon.HP -= enemy.AttackChoki; break;
                        case Actor.Actions.Par:  break;
                        default: Debug.LogError("やばい"); break;
                    }
                    break;
                default:
                    Debug.LogError("変な手が代入されているよ");
                    break;
            }

        }

    }



    public class StateAction : BattleState
    {

        public override void Enter(Dragon dragon, Enemy enemy)
        {
            //アニメーション再生！
            Debug.Log("Enter Animation");
            Actor winner = getWinner(dragon, enemy);
            if (winner != null)
            {
                winner.Anim.SetTrigger("Attack");
            }

        }

        public override BattleState Execute(Dragon dragon, Enemy enemy)
        {
            BattleState next = this;


            //ジャンケンの勝者が決まっている (＝あいこではない)
            Actor winner = getWinner(dragon, enemy);
            if (winner != null)
            {
                var nowAnimState = winner.Anim.GetCurrentAnimatorStateInfo(0);
                if (nowAnimState.fullPathHash != winner.hashAttack)
                {//攻撃アニメーションが終わったら
                    Debug.Log("Dragon HP = " + dragon.HP + " , Enemy HP = " + enemy.HP);

                    //どちらかが死んでたらリザルト（ここらへんは他のメンバーと要相談
                    if (dragon.HP <= 0 || enemy.HP <= 0)
                    {
                        return new StateResult();
                    }

                    return new StateDecideHand();
                }
            }
            else
            {
                //あいこだった。即座に次の手を決めよう
                return new StateDecideHand();
            }
            return next;
        }

        //アニメーションが終わったら体力を見て死んでたらリザルトへGO
        public override void Exit(Dragon dragon, Enemy enemy)
        {

        }



    }

    public class StateResult : BattleState
    {
        public override void Enter(Dragon dragon, Enemy enemy)
        {
            Actor winner = getWinner(dragon,enemy);
            Debug.Log(winner.name + " win");
        }

        public override BattleState Execute(Dragon dragon, Enemy enemy)
        {
            BattleState next = this;




            return next;
        }

        public override void Exit(Dragon dragon, Enemy enemy)
        {

        }
    }


}
