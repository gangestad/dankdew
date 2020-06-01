using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    public string[] dialogue;
    public string npcName;
    public string role;
 


    public override void Interact()
    {
        //base.Interact();
        
        DialogueSystem.Instance.AddNewDialogue(dialogue, npcName, role);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Debug.Log("Interacting with NPC.");

    }
}
