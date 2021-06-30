using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームの状態
/// </summary>
public enum GameState
{
    /// <summary>
    /// 始まっていない
    /// </summary>
    NotStart = 0,
    /// <summary>
    /// ゲームプレイ中
    /// </summary>
    Playing = 1,
    /// <summary>
    /// ゲームクリア
    /// </summary>
    Clear = 2,
    /// <summary>
    /// ゲームオーバー
    /// </summary>
    Gameover = 3
}

/// <summary>
/// ゲームのクリア判定、ゲームオーバー判定を実施
/// </summary>
public class GameController : MonoBehaviour
{
    /// <summary>
    /// シーン変更コンポーネント
    /// </summary>
    [SerializeField]
    SceneChanger sceneChanger = default;
    /// <summary>
    /// ゲームクリア時に飛ぶシーン名
    /// </summary>
    [SerializeField]
    string clearedSceneName = "";
    /// <summary>
    /// ゲームオーバー時に飛ぶシーン名
    /// </summary>
    [SerializeField]
    string gameoverSceneName = "";

    
    //[SerializeField]
    /*時間制御コンポーネントを配置*/


    /// <summary>
    /// ゲームの状態
    /// </summary>
    [SerializeField]
    GameState state = GameState.NotStart;



    public GameState State { get => state; }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
