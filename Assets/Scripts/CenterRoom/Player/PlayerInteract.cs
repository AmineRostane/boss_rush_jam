using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float rayCastDist;
    public LayerMask interactibleLayer;
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("OnInteract called!");

            Vector2 direction = transform.up;

            // Cast a ray from the player's position in the forward direction
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rayCastDist, interactibleLayer);

            if (hit.collider != null)
            {
                // Check if the hit object implements the IInteractable interface
                IInteractible interactible = hit.collider.GetComponent<IInteractible>();
                if (interactible != null)
                {
                    Debug.Log($"Interacting with: {hit.collider.gameObject.name}");
                    interactible.Interact(); // Call the Interact method on the object
                }
            }
            else
            {
                Debug.Log("No interactable object found.");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a debug line to visualize the raycast in the editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * rayCastDist);
    }

}
