using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    /// <summary>
    /// 直前のシーン名
    /// </summary>
    protected static string BeforeSceneName = "Title";



    /// <summary>
    /// 前のシーンへ戻る
    /// </summary>
    public void BackScene()
    {
        SceneManager.LoadScene(BeforeSceneName);
    }

    /// <summary>
    /// 指定のシーンへ
    /// </summary>
    public void GoScene(string sceneName)
    {
        BeforeSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
