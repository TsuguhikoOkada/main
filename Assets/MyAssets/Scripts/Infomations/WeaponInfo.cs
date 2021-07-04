using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ํ๎๑
/// </summary>
[RequireComponent(typeof(Collider))]
public class WeaponInfo : MonoBehaviour
{
    /// <summary>
    /// ฮLN^[ฬXe[^X
    /// </summary>
    CharacterStatus status = default;


    /// <summary>
    /// UออRC_[
    /// </summary>
    Collider[] ranges = default;


    /// <summary>
    /// ํผ
    /// </summary>
    [SerializeField]
    string name = "ํฬผO";

    /// <summary>
    /// ํUอ
    /// </summary>
    [SerializeField]
    int weaponPower = 10;



    /* vpeB */
    public CharacterStatus Status { get => status; set => status = value; }
    public int WeaponPower { get => weaponPower; }
    public string Name { get => name; }




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
    /// UออRC_[๐AflagชtrueลNฎAfalseลIน
    /// </summary>
    public void RangeActivator(bool flag)
    {
        foreach(Collider range in ranges)
        {
            range.enabled = flag;
        }
    }
}
