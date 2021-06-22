using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject firstSelect = default;

    Button[] buttonUIs = default;

    public void ActivateOrNotActivate(bool flag)
    {
        foreach(Button b in buttonUIs)
        {
            b.interactable = flag;
        }
        if (flag)
        {
            EventSystem.current.SetSelectedGameObject(firstSelect);
        }
    }

    private void Awake()
    {
        buttonUIs = this.transform.GetComponentsInChildren<Button>();
    }

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(firstSelect);
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
