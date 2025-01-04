using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject bossManager;
    private BossSelection bossSelect;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        bossSelect = bossManager.GetComponent<BossSelection>();
    }

    public void ReturnToRoom()
    {
        SceneManager.LoadScene("CenterRoom");
    }

    public void LoadBoss()
    {
        bossSelect.SelectBoss();
        int sceneIndex = bossSelect.GetBossIndex();
        Debug.Log("selected boss " + sceneIndex);
        SceneManager.LoadScene(sceneIndex);
    }
    
}
