using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GladiatorAnimator : MonoBehaviour
{
    [Header("Animatorに渡すパラメーター名")]
    /// <summary>
    /// パラメーター名：武器所持フラグ
    /// </summary>
    [SerializeField, Tooltip("パラメーター名：武器所持フラグ")] protected string Param_isArmed = "剣を持っているか";
    /// <summary>
    /// パラメーター名：X軸速度
    /// </summary>
    [SerializeField, Tooltip("パラメーター名：X軸速度")] protected string Param_moveDirectionX = "X軸の速度";
    /// <summary>
    /// パラメーター名：Y軸速度
    /// </summary>
    [SerializeField, Tooltip("パラメーター名：Y軸速度")] protected string Param_moveDirectionY = "Y軸の速度";
    /// <summary>
    /// パラメーター名：走行フラグ
    /// </summary>
    [SerializeField, Tooltip("パラメーター名：走行フラグ")] protected string Param_isRunning = "走っているか";
    /// <summary>
    /// パラメーター名:攻撃フラグ
    /// </summary>
    [SerializeField, Tooltip("パラメーター名：攻撃フラグ")] protected string Param_isAttack = "攻撃指示があったか";
    /// <summary>
    /// パラメーター名：強攻撃フラグ
    /// </summary>
    [SerializeField, Tooltip("パラメーター名：強攻撃実行フラグ")] protected string Param_isStrongAttack = "放った攻撃が強攻撃かどうか";
    /// <summary>
    /// パラメーター名：強攻撃フラグ
    /// </summary>
    [SerializeField, Tooltip("パラメーター名：被ダメージフラグ")] protected string Param_isDamaged = "ダメージを受けたか";
    /// <summary>
    /// パラメーター名：強攻撃フラグ
    /// </summary>
    [SerializeField, Tooltip("パラメーター名：強攻撃被弾フラグ")] protected string Param_isHardHit = "受けた攻撃が強攻撃かどうか";
    /// <summary>
    /// パラメーター名：被ダメージ方向(正面を0°)
    /// </summary>
    [SerializeField, Tooltip("パラメーター名：被ダメージ方向(正面を0°)")] protected string Param_damageDirectionAngle = "正面を0°とした時の被ダメージ方向角度";
    /// <summary>
    /// パラメーター名：行動不能フラグ
    /// </summary>
    [SerializeField, Tooltip("パラメーター名：行動不能フラグ")] protected string Param_isDefeated = "倒されたか";



    /// <summary>
    /// 対象のアニメーター
    /// </summary>
    protected Animator animator = default;

    /// <summary>
    /// 対象のステータス
    /// </summary>
    protected CharacterStatus status = default;

    /// <summary>
    /// 対象のダメージ処理コンポーネント
    /// </summary>
    protected DamageRange damageRange = default;


    public void AttackStart()
    {

    }

    public void AttackEnd()
    {

    }

    public void AcceptOtherActions()
    {

    }

    public void Hit()
    {

    }

    public void FootR()
    {

    }

    public void FootL()
    {

    }
}
