using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Interactable class that all other interactables should derive from
public class Interactable : MonoBehaviour
{
    public float radius = 0.8f;           // interaction radius
    //public Transform interactionTransform; // the transform from where we interact

    bool isFocus = false;   // check if current interactable is being focused
    public Transform player;       // the player transform
    [SerializeField] bool hasInteracted = false; // has the object already been interacted with

    public virtual void Interact()
    {
        // Method to be overriden by deriving classes
        Debug.Log("Interacting with: " + transform.name);
    }

    public virtual void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
            player = GameObject.FindGameObjectWithTag("Player").transform;
        else
            Debug.Log("Object with tag player doesn't exit, maybe you forgot to instantiate a player character?");
    }

    public virtual void Update()
    {
        // If the object is being focused and not interacted with before(is the last necessary?)
        if (isFocus && !hasInteracted)
        {
            Debug.Log("focus and not interacted");
            // Check if we are within interaction range
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance <= radius)
            {
                Debug.Log("Distance: " + distance);
                Debug.Log("Radius: " + radius);
                // Do interaction

                Debug.Log("DoInteraction");
                Interact();
                hasInteracted = true;
            }
        }
    }

    // Object is in focus
    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = player.transform;
        hasInteracted = false;
    }

    // Object no longer focused
    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = true;
    }

    // Draw the interaction radius
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    public bool GetInteracted() { return hasInteracted; }
    public void SetInteracted(bool interacted) { hasInteracted = interacted; }
}
