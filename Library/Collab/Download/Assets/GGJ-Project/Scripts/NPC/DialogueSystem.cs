using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Invector.CharacterController;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance { get; set; }
    public GameObject dialoguePanel;
    PlayerStatus playerStatus;
    vThirdPersonCamera playerCam;
    private int copLoss = -4, homieLoss = -2, homieGain = 4, copGain = 4;

    public float gramSold = 0.5f, pricePerGram = 200f;
    public string npcName, npcType;
    public List<string> dialogueLines = new List<string>(); // List of strings for our dialogue
    private Button continueButton, sellButton, abortButton;
    private Text dialogueText, nameText, roleText;
    private int dialogueIndex;

    Camera cam;

    void Start()
    {
        playerStatus = GameObject.Find("PlayerCharacter").GetComponent<PlayerStatus>();
        playerCam = GameObject.Find("vThirdPersonCamera").GetComponent<vThirdPersonCamera>();
    }
    // Start is called before the first frame update
    void Awake()
    {
        cam = Camera.main;

        // Wiring up the continue button, dialogue text, name text and the onClick event!
        continueButton = dialoguePanel.transform.Find("Continue").GetComponent<Button>();
        dialogueText = dialoguePanel.transform.Find("Text").GetComponent<Text>();
        roleText = dialoguePanel.transform.Find("Role").GetChild(0).GetComponent<Text>();
        nameText = dialoguePanel.transform.Find("Name").GetChild(0).GetComponent<Text>();
        sellButton = dialoguePanel.transform.Find("Sell").GetComponent<Button>();
        abortButton = dialoguePanel.transform.Find("Abort").GetComponent<Button>();
        sellButton.onClick.AddListener(delegate { Sell(); });
        abortButton.onClick.AddListener(delegate { Abort(); });
        continueButton.onClick.AddListener(delegate { ContinueDialogue(); });
        dialoguePanel.SetActive(false);


        // If conditions are met an instance exists that is not this instance, lets delete it
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            // Reference to this instance
            Instance = this;
        }
    }

    public void AddNewDialogue(string[] lines, string npcName, string npcType)
    {
        dialogueIndex = 0;
        // Creates a new empty list
        dialogueLines = new List<string>();
        foreach (string line in lines)
        {
            dialogueLines.Add(line);
        }
        //dialogueLines.AddRange(lines);
        this.npcName = npcName;
        this.npcType = npcType;
        Debug.Log(dialogueLines.Count);
        CreateDialogue();
    }

    public void CreateDialogue()
    {
        // Assign the approriate text to dialogue -> Assign the appropriate name -> Enable the dialogue panel
        dialogueText.text = dialogueLines[dialogueIndex];
        nameText.text = npcName;
        //roleText.text = npcType;
        dialoguePanel.SetActive(true);
        playerCam.lockCamera = true; // Lock the camera while dialogue is happening.
        GameObject.Find("PlayerCharacter").GetComponent<vThirdPersonController>().lockMovement = true;
        playerCam.height = 1.1f; // Set camera height a little lower.
        continueButton.gameObject.SetActive(false);

    }

    public void Sell()
    {
        Debug.Log(npcType);
        if (npcType == "Cop")
        {

            Debug.Log("You sold to a cop, busted!");
            dialogueText.text = "Trying to sell the devils lettuce are you?";
            playerStatus.SetCurWeedMultiplier(.5f);
            dialogueIndex++;
            continueButton.gameObject.SetActive(true);
            sellButton.gameObject.SetActive(false);
            abortButton.gameObject.SetActive(false);
            playerStatus.AddStreetcred(copLoss);
            StartOver();

            //Doing this in couroutine rn
            //NPC needs to leave
            //New NPC enters          

        }
        else
        {
            if (playerStatus.GetCurWeed() >= gramSold)
            {
                dialogueText.text = "Thanks fam";
                playerStatus.SetCurWeed(-.5f);
                playerStatus.AddStreetcred(homieGain);
                playerStatus.money += playerStatus.GetStreetcred() * (gramSold * pricePerGram);
                dialogueIndex++;
                sellButton.gameObject.SetActive(false);
                abortButton.gameObject.SetActive(false);
                continueButton.gameObject.SetActive(true);

            }
            else
            {
                dialogueText.text = "Seems like you've ran dry J OwO";
                playerStatus.AddStreetcred(homieLoss);
                sellButton.gameObject.SetActive(false);
                abortButton.gameObject.SetActive(false);
                StartCoroutine(Wait(2));
            }

            //StartCoroutine(CustomerTimer(5));

            //Doing this in couroutine rn
            //NPC needs to leave
            //New NPC enters
        }

        //dialoguePanel.SetActive(false);
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        //Unlock camera    
    }

    public void Abort()
    {
        Debug.Log(npcType);
        if (npcType == "Cop")
        {
            Debug.Log("Found cop, abort");
            dialogueText.text = "I'll get you next time J";
            sellButton.gameObject.SetActive(false);
            abortButton.gameObject.SetActive(false);
            playerStatus.AddStreetcred(copGain);

            // Doing this in couroutine rn
            // NPC needs to leave
            // New NPC enters          

        }
        else
        {
            dialogueText.text = "Damn homie, why you doing this?";
            sellButton.gameObject.SetActive(false);
            abortButton.gameObject.SetActive(false);
            if (playerStatus.GetStreetcred() + homieLoss >= 0)
            {
                playerStatus.AddStreetcred(homieLoss);
            }
            StartCoroutine(Wait(2));

            //Doing this in couroutine rn
            //NPC needs to leave
            //New NPC enters
        }

        //dialoguePanel.SetActive(false);
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        //Unlock camera  
    }

    public void ContinueDialogue()
    {
        if (dialogueIndex < dialogueLines.Count - 1)
        {
            dialogueIndex++;
            dialogueText.text = dialogueLines[dialogueIndex];
        }
        else
        {
            dialoguePanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            StartOver();
        }
    }
    public void StartOver()
    {
        dialoguePanel.SetActive(false);
        sellButton.gameObject.SetActive(true);
        abortButton.gameObject.SetActive(true);
        continueButton.gameObject.SetActive(true);
        playerCam.lockCamera = false;
        playerCam.height = 1.5f;
        GameObject.Find("PlayerCharacter").GetComponent<vThirdPersonController>().lockMovement = false;
        if (playerStatus.nightActive && playerStatus.GetCurWeed() > 0.5f)
        {
            playerStatus.NewNPC();
            Debug.Log("Spawning new NPC");
        }
        else
        {
            playerStatus.NightToDay();
            Debug.Log("Night is over");
        }

        // NPC leaves -> new comes in
    }
    IEnumerator Wait(int waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        StartOver();
    }
}
