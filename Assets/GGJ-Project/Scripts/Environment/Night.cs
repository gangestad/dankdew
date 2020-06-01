using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Night : MonoBehaviour
{
    Image image;
    Color color;
    public bool fadeNow;
    private float timer;
    private bool fadeOut;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();

        color = image.color;

    }
    public void FadeIn()
    {
        if (image.color.a < 1f)
        {
            color.a = Mathf.Clamp(timer, 0f, 1f);
            image.color = color;
        }
        else
        {
            fadeNow = false;
            fadeOut = true;
        }

    }
    public void FadeOut()
    {

        color.a = Mathf.Clamp(timer, 0f, 1f);
        image.color = color;

        if (image.color.a == 0f) fadeOut = false;
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(image.color.a);
        if (fadeNow)
        {
            timer += Time.deltaTime;
            FadeIn();
        }
        if (fadeOut)
        {
            timer -= Time.deltaTime;
            FadeOut();
        }
    }
    //void OnEnable()
    //{
    //    spawnNPC.PopulateList();
    //    // Fade out screen and move the player to a static location
    //    // Start the dialogue script, spawn NPCs (random NPCs with some variance based on HEAT)
    //    // Spawn X amount of NPCs based on your current street cred, try to sell as much weed as possible to increase your consumer base

    //}
}
