using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// エンディング画面を制御する
/// </summary>
public class EndingSystem : MonoBehaviour
{
    [SerializeField]
    string nextSceneName = "Title";

    [SerializeField]
    string useButton = "Submit";

    bool isClickedMessageWindow = false;

    [SerializeField]
    GameObject praiseMessage = default;

    [SerializeField]
    NovelMessageController novelMessageController = default;

    [SerializeField]
    SceneChanger sceneChanger = default;


    // Start is called before the first frame update
    void Start()
    {
        praiseMessage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (novelMessageController.IsRunAllActions)
        {
            praiseMessage.SetActive(true);

            if (Input.GetButtonDown(useButton) || isClickedMessageWindow) sceneChanger.GoScene(nextSceneName);
        }
        else
        {
            isClickedMessageWindow = false;
        }
    }

    /// <summary>
    /// 画面上のボタン入力により操作するためのメソッド
    /// ButtonのOnClickにて呼び出す
    /// </summary>
    public void ClickedMessageWindow()
    {
        isClickedMessageWindow = true;
    }
}
