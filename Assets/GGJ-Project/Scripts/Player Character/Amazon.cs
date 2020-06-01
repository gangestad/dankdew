using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Remember to pause stamina consumption
public class Amazon : MonoBehaviour
{
    public Text money;
    private PlayerStatus status;
    private float bongOne = 350f;
    private float bongTwo = 1500f;
    private float roomOne = 2000f;
    private float roomTwo = 5000f;
    // Start is called before the first frame update
    void Start()
    {
        money = GetComponent<Text>();
        status = FindObjectOfType<PlayerStatus>();
    }

    // Update is called once per frame
    
    void Update()
    {
        if (money)
        {
            money.text = status.money.ToString("F2");
        }

    }
    public void QuitAmazin()
    {
        Canvas amazinSiteGay;
        amazinSiteGay = GameObject.FindGameObjectWithTag("Computer").GetComponent<Canvas>();

        if (amazinSiteGay)
        {
            amazinSiteGay.enabled = false;
            status.SetAmazin(true);
        }

    }
    // Remember to instantiate and/or change the scene when buying stuff
    public void BuyBong(int bongType)
    {
        switch(bongType)
        {
            case 0:
                if (status.money >= bongOne)
                {
                    Button button = GameObject.Find("Bong1").GetComponent<Button>();
                    button.GetComponentInChildren<Text>().text = "Purchased";
                    status.money -= bongOne;
                    status.IncreaseBongStrength(0.15f);
                    button.interactable = false;
                    Debug.Log("Bought bongOne");
                }            
                break;
            case 1:
                if (status.money >= bongTwo)
                {
                    Button button = GameObject.Find("Bong2").GetComponent<Button>();
                    button.GetComponentInChildren<Text>().text = "Purchased";
                    status.money -= bongTwo;
                    status.IncreaseBongStrength(0.25f);
                    button.interactable = false;
                    Debug.Log("Bought bongTwo");
                }
                break;
        }
    }
    public void BuyRoom(int roomUpgrade)
    {
        switch (roomUpgrade)
        {
            case 0:
                if (status.money >= roomOne && status.roomSize < 1)
                {
                    Button button = GameObject.Find("GrowSpace1").GetComponent<Button>();
                    button.GetComponentInChildren<Text>().text = "Purchased";
                    status.money -= roomOne;
                    status.roomSize = 1;
                    button.interactable = false;
                    Debug.Log("Bought roomOne");
                }
                break;
            case 1:
                if (status.money >= roomTwo)
                {
                    Button button = GameObject.Find("GrowSpace2").GetComponent<Button>();
                    button.GetComponentInChildren<Text>().text = "Purchased";
                    status.money -= roomTwo;
                    if (status.roomSize == 0)
                    {
                        Button button2 = GameObject.Find("GrowSpace1").GetComponent<Button>();
                        button.GetComponentInChildren<Text>().text = "Purchased";
                        button.interactable = false;
                    }
                    status.roomSize = 2;
                    button.interactable = false;

                }
                break;
        }
    }
}
