using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのモデルのアニメーターにパラメーターを渡すコンポーネント
/// </summary>
[RequireComponent(typeof(Animator))]
public class ForPlayerGladiatorAnimator : MonoBehaviour
{
    [Header("Animatorに渡すパラメーター名")]
    /// <summary>
    /// パラメーター名：武器所持フラグ
    /// </summary>
    [SerializeField, Tooltip("パラメーター名：武器所持フラグ")] string Param_isArmed = "剣を持っているか";
    /// <summary>
    /// パラメーター名：X軸速度
    /// </summary>
    [SerializeField, Tooltip("パラメーター名：X軸速度")] string Param_moveDirectionX = "X軸の速度";
    /// <summary>
    /// パラメーター名：Y軸速度
    /// </summary>
    [SerializeField, Tooltip("パラメーター名：Y軸速度")] string Param_moveDirectionY = "Y軸の速度";
    /// <summary>
    /// パラメーター名：走行フラグ
    /// </summary>
    [SerializeField, Tooltip("パラメーター名：走行フラグ")] string Param_isRunning = "走っているか";
    /// <summary>
    /// パラメーター名:攻撃フラグ
    /// </summary>
    [SerializeField, Tooltip("パラメーター名：攻撃フラグ")] string Param_isAttack = "攻撃指示があったか";
    /// <summary>
    /// パラメーター名：強攻撃フラグ
    /// </summary>
    [SerializeField, Tooltip("パラメーター名：強攻撃フラグ")] string Param_isStrongAttack = "強攻撃かどうか";



    /// <summary>
    /// 対象のアニメーター
    /// </summary>
    Animator animator = default;

    /// <summary>
    /// 対象の移動処理コンポーネント
    /// </summary>
    CubeController moveCtrl = default;

    /// <summary>
    /// 対象の攻撃処理コンポーネント
    /// </summary>
    PlayerAttackController attackCtrl = default;




    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        moveCtrl = GetComponentInParent<CubeController>();
        attackCtrl = GetComponentInParent<PlayerAttackController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 md = new Vector2(moveCtrl.MoveDirection.x, moveCtrl.MoveDirection.z);
        animator.SetBool(Param_isArmed, attackCtrl.IsArmed);
        animator.SetFloat(Param_moveDirectionX, md.x);
        animator.SetFloat(Param_moveDirectionY, md.y);
        animator.SetBool(Param_isRunning, (moveCtrl.IsRunning && md.sqrMagnitude > 0.0f));

        if (attackCtrl.DoCommonAttack)
        {
            attackCtrl.IsAcceptOtherActions = false;
            animator.SetTrigger(Param_isAttack);
            animator.SetBool(Param_isStrongAttack, false);
        }
        else if (attackCtrl.DoStrongAttack)
        {
            attackCtrl.IsAcceptOtherActions = false;
            animator.SetTrigger(Param_isAttack);
            animator.SetBool(Param_isStrongAttack, true);
        }
        
    }

    public void AttackStart()
    {
        attackCtrl.IsAttacking = true;
    }

    public void AttackEnd()
    {
        attackCtrl.IsAttacking = false;
    }

    public void AcceptOtherActions()
    {
        attackCtrl.IsAcceptOtherActions = true;
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
