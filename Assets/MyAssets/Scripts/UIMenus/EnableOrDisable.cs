using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOrDisable : MonoBehaviour
{
    [SerializeField] GameObject target = default;

    public void DoEnableOrDisable(bool flag)
    {
        target.SetActive(flag);
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
