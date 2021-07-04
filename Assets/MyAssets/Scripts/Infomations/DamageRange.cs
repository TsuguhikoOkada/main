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
    private CharacterStatus status = default;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if((other.tag == "Enemy" && transform.tag == "Player")
            || (other.tag == "Player" && transform.tag == "Enemy"))
        {
            WeaponInfo attacker = other.gameObject.GetComponent<WeaponInfo>();
            if (!attacker) return;


            //ダメージ計算(仮)
            int damage = attacker.Status.Power + attacker.WeaponPower;

            //HPを減らす
            status.NowHp = Mathf.Max(status.NowHp - damage, 0);
        }

        
    }
}
