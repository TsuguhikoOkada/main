using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// このコンポーネントをつけているオブジェクトのレイヤ番号を記録し、
/// やられた際にレイヤ名を変更する
/// </summary>
public class DefeatedController : MonoBehaviour
{
    /// <summary>
    /// 初期のレイヤ番号
    /// </summary>
    int originallyLayer = -1;

    /// <summary>
    /// キャラクターが倒されたときに書き換えるレイヤ名
    /// </summary>
    [SerializeField,Tooltip("倒されたキャラクターレイヤ")] string Layer_Defeated = "Defeated";
    int defeatedLayer = -1;

    /// <summary>
    /// 対象のキャラクターステータス
    /// </summary>
    CharacterStatus status = default;


    // Start is called before the first frame update
    void Start()
    {
        status = GetComponentInChildren<CharacterStatus>();
        if (!status) status = GetComponentInParent<CharacterStatus>();

        originallyLayer = this.gameObject.layer;
        defeatedLayer = LayerMask.NameToLayer(Layer_Defeated);
    }

    // Update is called once per frame
    void Update()
    {
        //倒されていたら
        if(status.IsDefeated)
        {
            //倒された用のレイヤに切り替える
            this.gameObject.layer = defeatedLayer;
        }
        else if(this.gameObject.layer != originallyLayer)
        {
            this.gameObject.layer = originallyLayer;
        }
    }
}
