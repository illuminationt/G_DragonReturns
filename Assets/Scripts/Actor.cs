using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(Animator))]
public abstract class Actor : MonoBehaviour {
    //ネットから拾ってきた書き方(アニメーションのハッシュ値を登録しておくらしい)(ハッシュ値て何？）
    public readonly int hashAttack = Animator.StringToHash("Attack");


    public enum HandState
    {
        DONT_DECIDE,//まだ手を決めていない
        FINISH_DECIDE,//ちょうど手を決め終わった
        ACTION,
    }
    public HandState State { get; set; }

    //グーとかチョキとか。拡張性をつけるためenumで
    public enum Actions
    {
        Gu,
        Choki,
        Par,
        Item,

        Unknown,
        Error,
    }

    public int HP;
    public int AttackGu;
    public int AttackChoki;
    public int AttackPar;
    
    public Animator Anim;

    public bool IsWinner = false;

    protected void Start()
    {
        HP = 100; AttackGu = 7; AttackChoki = 13; AttackPar = 17;
        Anim = GetComponent<Animator>();
    }

    //g : グー、c : チョキ、p : パー
    public Actions Action = Actions.Unknown;

    //この中でm_handを決定してm_stateをFINISH_DECIDEにする。
    public abstract void DecideHand();


}
