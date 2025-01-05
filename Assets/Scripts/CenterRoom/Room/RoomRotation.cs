using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomRotation : MonoBehaviour
{
    [SerializeField] private float rotationDuration = 1f;
    private bool isRotating = false;

    public static Quaternion currentRotation = Quaternion.identity;

    private void Awake()
    {
        if (currentRotation != Quaternion.identity)
        {
            transform.rotation = currentRotation;
        }

        if(GameManager.currentRound>=GameManager.maxRound)
        {
            ResetRotation();
        }
        Debug.Log($"Applying Saved Rotation: {currentRotation.eulerAngles}");
    }

    public void TriggerRotation()
    {
        if(!isRotating)
        {
            isRotating = true;
            RotateRoom();
        }
    }
    
    private void RotateRoom()
    {
        Debug.Log("i m rotating");
        isRotating = true;

        float[] possibleAngles = { 90f, 180f };
        float randomAngle = possibleAngles[Random.Range(0, possibleAngles.Length)];
        int direction = Random.value > 0.5f ? 1 : -1;
        float targetAngle = randomAngle * direction;

        Debug.Log("target angle " + targetAngle);

        StartCoroutine(PerformRotation(targetAngle));
    }

    public void ResetRotation()
    {
        // Reset the static rotation and the room's rotation
        currentRotation = Quaternion.identity;
        transform.rotation = currentRotation;
        Debug.Log("Room rotation reset to default.");
    }

    private IEnumerator PerformRotation(float targetAngle)
    {
        float elapsedTime = 0f;
        Quaternion initialRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0f, 0f , targetAngle));

        while (elapsedTime < rotationDuration)
        {
            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / rotationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.rotation = targetRotation;
        currentRotation = transform.rotation;
        isRotating = false;
    }
}
