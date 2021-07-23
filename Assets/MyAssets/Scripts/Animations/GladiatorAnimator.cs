using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GladiatorAnimator : MonoBehaviour
{
    [Header("Animatorに渡すパラメーター名")]
    /// <summary>
    /// パラメーター名：武器所持フラグ
    /// </summary>
    [SerializeField, Tooltip("パラメーター名：武器所持フラグ")] protected string ParamIsArmed = "isArmed";
    /// <summary>
    /// パラメーター名：X軸速度
    /// </summary>
    [SerializeField, Tooltip("パラメーター名：X軸速度")] protected string ParamMoveDirectionX = "moveDirectionX";
    /// <summary>
    /// パラメーター名：Y軸速度
    /// </summary>
    [SerializeField, Tooltip("パラメーター名：Y軸速度")] protected string ParamMoveDirectionY = "moveDirectionY";
    /// <summary>
    /// パラメーター名：走行フラグ
    /// </summary>
    [SerializeField, Tooltip("パラメーター名：走行フラグ")] protected string ParamIsRunning = "isRunning";
    /// <summary>
    /// パラメーター名:攻撃フラグ
    /// </summary>
    [SerializeField, Tooltip("パラメーター名：攻撃フラグ")] protected string ParamIsAttack = "isAttack";
    /// <summary>
    /// パラメーター名：強攻撃フラグ
    /// </summary>
    [SerializeField, Tooltip("パラメーター名：強攻撃実行フラグ")] protected string ParamIsStrongAttack = "isStrongAttack";
    /// <summary>
    /// パラメーター名：強攻撃フラグ
    /// </summary>
    [SerializeField, Tooltip("パラメーター名：被ダメージフラグ")] protected string ParamIsDamaged = "isDamaged";
    /// <summary>
    /// パラメーター名：強攻撃フラグ
    /// </summary>
    [SerializeField, Tooltip("パラメーター名：強攻撃被弾フラグ")] protected string ParamIsHardHit = "isHardHit";
    /// <summary>
    /// パラメーター名：被ダメージ方向(正面を0°)
    /// </summary>
    [SerializeField, Tooltip("パラメーター名：被ダメージ方向(正面を0°)")] protected string ParamDamageDirectionAngle = "damageDirectionAngle";
    /// <summary>
    /// パラメーター名：行動不能フラグ
    /// </summary>
    [SerializeField, Tooltip("パラメーター名：行動不能フラグ")] protected string ParamIsDefeated = "isDefeated";



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
