using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Keep track of various statuses
public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private float maxStamina = 100f, stamina, bongStrength = 0.1f, curWeed = .5f, heat, nutrients, streetCred = 5f;
    Night night;
    GameObject cam;
    WeedPlant[] weeds;
    SpawnNPC spawnNPC;
    private bool amazinSite = false;
    private int totalYield;
    private int growLevel = 1;
    private List<WeedPlant> plantable;
    private float timer;
    public float money = 50f;
    public int roomSize = 0;
    public GameObject statsPanel;
    Text moneyText, credText, weedText;
    public bool nightActive = false;
    // Start is called before the first frame update
    void Start()
    {
        moneyText = statsPanel.transform.Find("CashAmountText").GetComponent<Text>();
        credText = statsPanel.transform.Find("CredAmountText").GetComponent<Text>();
        weedText = statsPanel.transform.Find("WeedAmountText").GetComponent<Text>();
        night = GameObject.Find("NightTimeFade").GetComponent<Night>();
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        spawnNPC = GameObject.Find("Spawner").GetComponent<SpawnNPC>();
        stamina = maxStamina;
        weeds = FindObjectsOfType<WeedPlant>();
        UpdateYield();
        // remember to add the initial type of plantable weed to the plantable list
    }
    public void newPlantable(WeedPlant plant) { plantable.Add(plant); }
    public List<WeedPlant> GetPlantable() { return plantable; } // Should probably iterate through this list to search for each type and use that when selecting what to plant.
    public void UpdateYield()
    {
        totalYield = 0;
        foreach (WeedPlant weed in weeds)
        {
            totalYield += weed.yield;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (amazinSite != true)
        {
            if (!nightActive)
                stamina -= Time.deltaTime;
            if (stamina <= 0f)
            {
                DayToNight();
            }
        }
        //Debug.Log(transform.position);

        moneyText.text = "" + money;
        credText.text = "" + streetCred;
        weedText.text = "" + curWeed + "g";
        // Debug.Log(streetCred);

    }
    public void DayToNight() // Use this script to force a new day 
    {
        night.fadeNow = true;
        nightActive = true;
        spawnNPC.PopulateList(); // Get a new random selection of NPCs
        ResetStamina();
        StartCoroutine(Wait(1)); // Wait one second so the screen is blacked out while stuff happens

    }
    public void NightToDay()
    {
        night.fadeNow = true;
        StartCoroutine(Wait3(1));

    }
    public void NewNPC()
    {
        night.fadeNow = true;
        StartCoroutine(Wait2(1)); // Wait one second so the screen is blacked out while stuff happens
    }
    void UpdateEverything()
    {
        GameObject cupboard = GameObject.Find("Cupboard");
        // Hacky as fuck, dunno what's wrong with this cupboard's local positions
        cupboard.transform.position = new Vector3(cupboard.transform.position.x + 0.01f, cupboard.transform.position.y, cupboard.transform.position.z); // Reset the cupboard to its old position.
        foreach (WeedPlant weed in weeds)
        {
            if (weed)
                weed.Growth();
        }
    }
    IEnumerator Wait(int time)
    {
        yield return new WaitForSeconds(time);
        spawnNPC.Spawn(new Vector3(-11.22f, -0.009f, -0.308f));
        transform.position = new Vector3(-15.5f, 0f, 0f); // Move the player.
        UpdateEverything();
    }
    IEnumerator Wait2(int time)
    {
        yield return new WaitForSeconds(time);
        GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC");
        foreach (GameObject npc in npcs)
        {
            Destroy(npc); // Remove all current NPCs from the scene
        }
        transform.position = new Vector3(-15.5f, 0f, 0f); // Move the player.
        spawnNPC.Spawn(new Vector3(-11.22f, -0.009f, -0.308f)); // Spawn a new NPC
    }
    IEnumerator Wait3(int time)
    {
        yield return new WaitForSeconds(time);
        GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC");
        foreach (GameObject npc in npcs)
        {
            Destroy(npc); // Remove all current NPCs from the scene
        }
        nightActive = false;

    }
    public float GetHeat() { return heat; }
    public float GetStreetcred() { return streetCred; }
    public float GetCurWeed() { return curWeed; }
    public float GetBongStrength() { return bongStrength; }
    public float GetStamina() { return stamina; }
    public void ReduceStamina(float reduceVal) { stamina -= reduceVal; }
    public void IncreaseMaxStamina() { maxStamina += bongStrength * maxStamina; }
    public float GetMaxStamina() { return maxStamina; }
    public void ResetStamina() { stamina = maxStamina; }
    public void SetCurWeed(float weed) { curWeed += weed; }
    public void SetCurWeedMultiplier(float weed) { curWeed *= weed; }
    public void IncreaseBongStrength(float newBong) { bongStrength += newBong; }
    public void SetAmazin(bool amazin)
    {
        amazinSite = amazin;
    }

    public void AddStreetcred(float cred)
    {
        streetCred += cred;
    }

    public void AddHeat(float heatRep)
    {
        heat += heatRep;
    }

}
