using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのモデルのアニメーターにパラメーターを渡すコンポーネント
/// </summary>
[RequireComponent(typeof(Animator))]
public class ForPlayerGladiatorAnimator : MonoBehaviour
{
    /// <summary>
    /// パラメーター名：武器所持フラグ
    /// </summary>
    [SerializeField] string ParamName_isArmed = "剣を持っているか";
    /// <summary>
    /// パラメーター名：X軸速度
    /// </summary>
    [SerializeField] string ParamName_moveDirectionX = "X軸の速度";
    /// <summary>
    /// パラメーター名：Y軸速度
    /// </summary>
    [SerializeField] string ParamName_moveDirectionY = "Y軸の速度";
    /// <summary>
    /// パラメーター名：走行フラグ
    /// </summary>
    [SerializeField] string ParamName_isRunning = "走っているか";


    /// <summary>
    /// 対象のアニメーター
    /// </summary>
    Animator animator = default;

    /// <summary>
    /// 対象の移動処理コンポーネント
    /// </summary>
    [SerializeField]
    CubeController ctrl = default;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 md = new Vector2(ctrl.MoveDirection.x, ctrl.MoveDirection.z);
        animator.SetBool(ParamName_isArmed, true);
        animator.SetFloat(ParamName_moveDirectionX, md.x);
        animator.SetFloat(ParamName_moveDirectionY, md.y);
        animator.SetBool(ParamName_isRunning, (ctrl.IsRunning && md.sqrMagnitude > 0.0f));
    }

    public void FootR()
    {

    }

    public void FootL()
    {

    }
}
