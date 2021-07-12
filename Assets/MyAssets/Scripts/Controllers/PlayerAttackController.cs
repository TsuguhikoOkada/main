using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 攻撃ボタンより、プレイヤーに攻撃させるコンポーネント
/// </summary>
public class PlayerAttackController : MonoBehaviour
{
    [Header("inputmanager上のボタン名")]
    
    [SerializeField, Tooltip("弱攻撃ボタン名")] string button_commonAttack = "Fire1";
    [SerializeField, Tooltip("強攻撃ボタン名")] string button_strongAttack = "Fire2";

    [Space]

    /// <summary>
    /// 武器(素手)情報
    /// </summary>
    WeaponInfo[] weapons = default;

    /// <summary>
    /// 素手の武器名
    /// </summary>
    [SerializeField, Tooltip("素手の場合の武器名")] string weapon_bareHand = "素手";


    /// <summary>
    /// キャラクターの能力値
    /// </summary>
    CharacterStatus status = default;


    /// <summary>
    /// 弱攻撃の実施を要求
    /// </summary>
    bool doCommonAttack = false;
    /// <summary>
    /// 強攻撃の実施を要求
    /// </summary>
    bool doStrongAttack = false;
    /// <summary>
    /// 武器を装備中である
    /// </summary>
    bool isArmed = false;
    /// <summary>
    /// 攻撃範囲が有効である(ForPlayerGladiatorAnimatorから変更)
    /// </summary>
    bool isAttacking = false;
    /// <summary>
    /// 他のアクションの受付を許可しているか(ForPlayerGladiatorAnimatorから変更)
    /// </summary>
    bool isAcceptOtherActions = true;



    public bool DoCommonAttack { get => doCommonAttack; }
    public bool DoStrongAttack { get => doStrongAttack; }
    public bool IsArmed { get => isArmed; }
    public bool IsAttacking { set => isAttacking = value; }
    public bool IsAcceptOtherActions { set => isAcceptOtherActions = value; }




    // Start is called before the first frame update
    void Start()
    {
        weapons = GetComponentsInChildren<WeaponInfo>();
        status = GetComponentInChildren<CharacterStatus>();
        CheckArmed();
    }

    // Update is called once per frame
    void Update()
    {
        //攻撃フラグ初期化
        doCommonAttack = false;
        doStrongAttack = false;
        //攻撃アニメーションが攻撃をし終えているので、次の操作入力を許可している
        if (isAcceptOtherActions)
        {
            if (Input.GetButtonDown(button_commonAttack))
            {
                doCommonAttack = true;
            }
            else if (Input.GetButtonDown(button_strongAttack))
            {
                doStrongAttack = true;
            }

            //武器に、強攻撃であるかの情報と威力補正情報を渡す
            foreach(WeaponInfo wep in weapons)
            {
                wep.DoStrongAttack = doStrongAttack;

                wep.PowerRatio = 1.0f;
                if (doStrongAttack) wep.PowerRatio = 1.5f;
            }
        }
        


        //攻撃アニメーションが、ダメージ判定のある動きに入っている
        if (isAttacking)
        {
            //武器毎に当たり判定コライダーを起動
            foreach (WeaponInfo wep in weapons)
            {
                if (isArmed)
                {
                    if(wep.WeaponName != weapon_bareHand) wep.RangeActivator(true);
                }
                else
                {
                    wep.RangeActivator(true);
                }
            }
        }
        else
        {
            //武器毎に当たり判定コライダーを終了
            foreach (WeaponInfo wep in weapons)
            {
                wep.RangeActivator(false);
            }
        }
    }

    /// <summary>
    /// 武器情報をチェック
    /// ・素手以外の武器を装備中かチェック
    /// ・武器すべてに自分のタグを適用
    /// ・武器に自分のステータス情報のアクセッサーを渡す
    /// </summary>
    void CheckArmed()
    {
        isArmed = false;
        foreach (WeaponInfo wep in weapons)
        {
            wep.tag = this.gameObject.tag;
            wep.Status = status;

            if (isArmed || wep.WeaponName == weapon_bareHand) continue;

            isArmed = true;
        }
    }
}
