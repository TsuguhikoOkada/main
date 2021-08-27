using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearSceneChange : MonoBehaviour
{
    List<CharacterStatus> enemyStatus = new List<CharacterStatus>();

    [SerializeField]
    string sceneName = default;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] enemyObj = GameObject.FindGameObjectsWithTag("Enemy");

        foreach(GameObject obj in enemyObj)
        {
            CharacterStatus status = obj.GetComponentInChildren<CharacterStatus>();
            if (status) enemyStatus.Add(status);
        }
    }

    // Update is called once per frame
    void Update()
    {
        int undefeatedCount = 0;

        foreach(CharacterStatus status in enemyStatus)
        {
            if (!status.IsDefeated) undefeatedCount++;
        }

        if(undefeatedCount <= 0)
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
