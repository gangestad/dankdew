using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Computerscript : Interactable
{
    PlayerStatus playerStatus;
    Canvas amazinSiteGay;
    public override void Interact()
    {
        base.Interact();
        Debug.Log("Interacting with computer");
        LoadAmazin();

    }
    public override void Start()
    {
        base.Start();
        amazinSiteGay = GameObject.FindGameObjectWithTag("Computer").GetComponent<Canvas>();
        
    }
    private void LoadAmazin()
    {
        if (amazinSiteGay)
        {
            amazinSiteGay.enabled = true;
            playerStatus.SetAmazin(false);
        }

        else
            Debug.Log("something happened");
    }

}
