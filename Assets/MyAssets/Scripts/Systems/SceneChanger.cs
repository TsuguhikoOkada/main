using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    /// <summary>
    /// 遷移先のシーン名
    /// </summary>
    [SerializeField]
    string sceneChanageName = null;

    /// <summary>
    /// 直前のシーン名
    /// </summary>
    protected static string BeforeSceneName = "Title";

    /// <summary>
    /// 会話パートを制御するコンポーネント
    /// </summary>
    [SerializeField]
    NovelMessageController novelSystem = default;

    /// <summary>
    /// シーン変更にかける遅延時間
    /// </summary>
    [SerializeField]
    float sceneChanageDelayTime = 2.0f;

    /// <summary>
    /// 暗転用画像を制御するAnimation
    /// </summary>
    [SerializeField]
    Animation blinderAnim = default;

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

    void Update()
    {
        //会話文システムをすべて実行し終えたらシーンを変更
        if(novelSystem && novelSystem.IsRunAllActions)
        {
            StartCoroutine(GoSceneLateTime());
        } 
    }

    /// <summary>
    /// シーン変更を遅延実行
    /// </summary>
    /// <returns></returns>
    IEnumerator GoSceneLateTime()
    {
        float delayTime = sceneChanageDelayTime;

        if (blinderAnim)
        {
            delayTime = Mathf.Max(delayTime - blinderAnim.clip.length, 0.001f);

            yield return new WaitForSeconds(blinderAnim.clip.length);

            blinderAnim.Play();
        }

        yield return new WaitForSeconds(delayTime);

        GoScene(sceneChanageName);
    }
}
