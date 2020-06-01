using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Water : Interactable
{
    WeedPlant[] weeds;
    bool showText = false;
    Text plants;
    Text whatDo;
    float timer = 3f;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        weeds = FindObjectsOfType<WeedPlant>();
        plants = GameObject.Find("Plants").GetComponent<Text>();
        whatDo = GameObject.Find("WhatDo").GetComponent<Text>();
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            whatDo.enabled = true;
        }
    }
    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            whatDo.enabled = false;
            //SetInteracted(false);
        }
    }
    public override void Interact()
    {
        base.Interact();
        foreach (WeedPlant weed in weeds)
        {
            weed.Hydrate();
        }
        plants.enabled = true;
        whatDo.enabled = false;
    }
    public override void Update()
    {
        base.Update();
        if (plants.enabled)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
                plants.enabled = false;
        }
    }
}
