using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenuOnStoryController : MonoBehaviour
{
    /// <summary>
    /// メニュー背景
    /// </summary>
    [SerializeField] GameObject menuBackground = default;

    //メニューボタン
    [SerializeField] Button menuButton = default;


    /// <summary>
    /// メインメニュー
    /// </summary>
    [SerializeField] GameObject mainMenu = default;
    [SerializeField] Button[] mainMenuButtons = default;
    [SerializeField] GameObject mainMenuFirstFocus = default;

    /// <summary>
    /// 確認メニュー
    /// </summary>
    [SerializeField] GameObject confirmMenu = default;
    [SerializeField] Button[] confirmMenuButtons = default;
    [SerializeField] GameObject confirmMenuFirstFocus = default;

    /// <summary>
    /// 操作確認表メニュー
    /// </summary>
    [SerializeField] GameObject instructionMenu = default;
    [SerializeField] Button buckButton = default;


    /// <summary>
    /// StartCoroutineにより、次のフレームでボタンにフォーカスを当てる
    /// </summary>
    /// <param name="buttonObj"></param>
    /// <returns></returns>
    IEnumerator FocusButtonNextFrame(GameObject buttonObj)
    {
        yield return null;
        yield return null;

        EventSystem.current.SetSelectedGameObject(buttonObj);
    }


    //メニューを開く
    public void OpenMenu()
    {
        menuBackground.SetActive(true);

        instructionMenu.SetActive(false);
        mainMenu.SetActive(true);
        confirmMenu.SetActive(false);

        menuButton.interactable = false;

        StartCoroutine(FocusButtonNextFrame(mainMenuFirstFocus));
    }

    //確認メニューを開く
    public void OpenConfirmMenu()
    {
        menuBackground.SetActive(true);

        mainMenu.SetActive(false);
        confirmMenu.SetActive(true);

        menuButton.interactable = false;

        StartCoroutine(FocusButtonNextFrame(confirmMenuFirstFocus));
    }


    //メニューを閉じる
    public void CloseMenu()
    {
        instructionMenu.SetActive(false);
        mainMenu.SetActive(true);
        confirmMenu.SetActive(false);

        menuButton.interactable = true;

        menuBackground.SetActive(false);
    }

    /// <summary>
    /// 操作確認メニューを開く
    /// </summary>
    public void OpenInstructionMenu()
    {
        instructionMenu.SetActive(true);
        mainMenu.SetActive(false);
        confirmMenu.SetActive(false);

        StartCoroutine(FocusButtonNextFrame(buckButton.gameObject));
    }



    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(mainMenuFirstFocus);
    }

    // Update is called once per frame
    void Update()
    {
        //Cancel(Esc)ボタンでメニュー画面の表示・非表示
        if (Input.GetButtonDown("Cancel"))
        {
            if (menuBackground.activeSelf) CloseMenu();
            else OpenMenu();
        }
    }
}
