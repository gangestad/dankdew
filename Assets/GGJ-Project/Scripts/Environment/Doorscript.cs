using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doorscript : Interactable
{
    private bool opener;

    public override void Interact()
    {
        base.Interact();
        Debug.Log("Interacting with door");
        // Harvest
        TurnDoor();
        
    }

    public override void Update()
    {
        base.Update();
        if (opener == true)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 88, 0), 2 * Time.deltaTime);
        }
    }

    private void TurnDoor()
    {
        Debug.Log("Turning door yo");
        opener = true;
        
    }
}
