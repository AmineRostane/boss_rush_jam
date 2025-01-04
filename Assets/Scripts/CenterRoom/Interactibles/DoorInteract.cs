using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class DoorInteract : MonoBehaviour, IInteractible
{
    public GameManager gameManager;
    private bool isPlayerNearby = false;

    public void Interact()
    {
        
            Debug.Log("interaction occured");
            gameManager.LoadBoss();
        
    }
}
