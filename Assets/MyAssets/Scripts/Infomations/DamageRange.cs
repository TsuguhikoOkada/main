using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 被攻撃範囲
/// </summary>
[RequireComponent(typeof(Collider))]
public class DamageRange : MonoBehaviour
{
    /// <summary>
    /// 対応キャラクターのステータス
    /// </summary>
    [SerializeField]
    CharacterStatus status = default;

    /// <summary>
    /// 被攻撃範囲コライダー
    /// </summary>
    Collider[] ranges = default;

    /// <summary>
    /// 攻撃がヒットしたときに発生させるパーティクル
    /// </summary>
    [SerializeField]
    GameObject[] hitEffects = default;


    /// <summary>
    /// ダメージを受けた
    /// </summary>
    bool isDamaged = false;

    /// <summary>
    /// 強攻撃だった
    /// </summary>
    bool isHardHit = false;


    /// <summary>
    /// ダメージを受けた方向(-π～0～π)
    /// </summary>
    float damagedDirection = 0.0f;


    public bool IsDamaged { get => isDamaged; set => isDamaged = value; }
    public bool IsHardHit { get => isHardHit; }
    public float DamagedDirection { get => damagedDirection; }
    


    // Start is called before the first frame update
    void Start()
    {
        ranges = GetComponents<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        //倒されたら、ダメージを受けるコライダーを無効化
        if(status.IsDefeated)
        {
            foreach(Collider range in ranges)
            {
                range.enabled = false;
            }
        }
    }


    /// <summary>
    /// 敵対者からの攻撃を受け、HPを減らす
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if((other.CompareTag("Enemy") && transform.CompareTag("Player"))
            || (other.CompareTag("Player") && transform.CompareTag("Enemy")))
        {
            WeaponInfo attacker = other.gameObject.GetComponent<WeaponInfo>();
            if (!attacker) return;

            //ダメージをうけた
            isDamaged = true;

            //強攻撃かの判定
            isHardHit = attacker.DoStrongAttack;

            //パーティクル生成手続き
            if(hitEffects.Length > 0)
            {
                foreach(GameObject effect in hitEffects)
                {
                    if (effect)
                    {
                        //パーティクル生成
                        GameObject initialized = Instantiate(effect);
                        initialized.transform.position = other.ClosestPoint(other.transform.position);
                        Destroy(initialized, 3.0f);
                    }
                }
            }

            //どの方向から受けたかを求める
            Vector3 vec = Vector3.Normalize(other.transform.position - this.transform.position);
            damagedDirection = Vector3.SignedAngle(this.transform.forward, vec, this.transform.up);

            //ダメージ計算(仮)
            int damage = (int)((attacker.Status.Power + attacker.WeaponPower) * attacker.PowerRatio);

            //HPを減らす
            status.NowHp = Mathf.Max(status.NowHp - damage, 0);
        }
    }
}
