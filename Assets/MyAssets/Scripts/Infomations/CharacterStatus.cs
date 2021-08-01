using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterStatus : MonoBehaviour
{
    /// <summary>
    /// 最大の体力
    /// </summary>
    [SerializeField]
    int maxHp = 100;
    /// <summary>
    /// 現在の体力
    /// </summary>
    [SerializeField]
    int nowHp = 100;
    /// <summary>
    /// 攻撃力
    /// </summary>
    [SerializeField]
    int power = 30;
    /// <summary>
    /// スタミナ
    /// </summary>
    [SerializeField]
    int stamina = 20;


    /// <summary>
    /// 倒されたか
    /// </summary>
    [SerializeField]
    bool isDefeated = false;
    [SerializeField]
    string sceneName;



    /* プロパティ */
    public int MaxHp { get => maxHp; set => maxHp = value; }
    public int NowHp { get => nowHp; set => nowHp = value; }
    public int Power { get => power; set => power = value; }
    public int Stamina { get => stamina; set => stamina = value; }
    public bool IsDefeated { get => isDefeated; set => isDefeated = value; }



    // Start is called before the first frame update
    void Start()
    {
        nowHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        isDefeated = nowHp <= 0;

        if (isDefeated == true)
        {
            StartCoroutine(sceneChange());
        }
    }
    IEnumerator sceneChange()
    {
        yield return new WaitForSeconds(3.0f);

        SceneManager.LoadScene(sceneName);
    }

}
