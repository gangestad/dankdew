using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cupboardscript : Interactable
{
    private bool opener;
    private PlayerStatus status;
    public override void Interact()
    {
        if (!status.nightActive) // Don't let the player move the cupboard if it's night-time.
        {
            base.Interact();
            Debug.Log("Interacting with cupboard");
            SlideCupboard();
        }
    }
    public override void Start()
    {
        base.Start();
        status = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
    }
    public override void Update()
    {
        base.Update();
        Debug.Log("World" + transform.position.x);
        Debug.Log("Local" + transform.localPosition.x);
        if (opener == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(-0.01f, 0f), 100 * Time.deltaTime);
            if (transform.localPosition.x <= -0.0103f)
            {
                Debug.Log("Stop sliding waah");
                opener = false;
            }
        }
    }

    private void SlideCupboard()
    {

        Debug.Log("Slide me daddy");
        opener = true;

    }
}
