using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 武器情報
/// </summary>
[RequireComponent(typeof(Collider))]
public class WeaponInfo : MonoBehaviour
{
    /// <summary>
    /// 対応キャラクターのステータス
    /// </summary>
    CharacterStatus status = default;


    /// <summary>
    /// 攻撃範囲コライダー
    /// </summary>
    Collider[] ranges = default;


    /// <summary>
    /// 武器名
    /// </summary>
    [SerializeField]
    string weaponName = "素手";

    /// <summary>
    /// 武器攻撃力
    /// </summary>
    [SerializeField]
    int weaponPower = 10;


    /// <summary>
    /// この武器で攻撃するときの強攻撃フラグ
    /// </summary>
    bool doStrongAttack = false;
    /// <summary>
    /// この武器で攻撃するときの威力補正
    /// </summary>
    float powerRatio = 1.0f;



    /* プロパティ */
    public CharacterStatus Status { get => status; set => status = value; }
    public int WeaponPower { get => weaponPower; }
    public string WeaponName { get => weaponName; }
    public bool DoStrongAttack { get => doStrongAttack; set => doStrongAttack = value; }
    public float PowerRatio { get => powerRatio; set => powerRatio = value; }




    // Start is called before the first frame update
    void Start()
    {
        ranges = GetComponents<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 攻撃範囲コライダーを、flagがtrueで起動、falseで終了
    /// </summary>
    public void RangeActivator(bool flag)
    {
        foreach(Collider range in ranges)
        {
            range.enabled = flag;
        }
    }
}
