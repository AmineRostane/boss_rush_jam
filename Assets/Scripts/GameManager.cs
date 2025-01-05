using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    public static int currentRound = 0;
    public static int maxRound = 3;
    public bool shouldRotateRoom = false;

    private Quaternion currentRoomRotation = Quaternion.identity;

    public GameObject bossManager;
    private BossSelection bossSelect;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist this instance
        }
        else
        {
            // If another instance exists, destroy this one
            Destroy(gameObject);
        }

        bossSelect = bossManager.GetComponent<BossSelection>();
    }


    public void ReturnToRoom()
    {
        SceneManager.LoadScene("CenterRoom");
        SceneManager.sceneLoaded += OnCenterRoomLoaded;
    }

    private void OnCenterRoomLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "CenterRoom")
        {
            // Find the RoomRotation component dynamically in the CenterRoom scene
            var roomRotationObject = GameObject.FindObjectOfType<RoomRotation>();

            if (roomRotationObject != null && currentRound > 0)
            {
                roomRotationObject.TriggerRotation();
            }
            else
            {
                Debug.LogWarning("RoomRotation component not found in the scene!");
            }

            // Unsubscribe from the scene loaded event
            SceneManager.sceneLoaded -= OnCenterRoomLoaded;
        }
    }

    public void LoadBoss()
    {
        bossSelect.SelectBoss();
        int sceneIndex = bossSelect.GetBossIndex();
        Debug.Log("selected boss " + sceneIndex);
        SceneManager.LoadScene(sceneIndex);
    }
    
}
