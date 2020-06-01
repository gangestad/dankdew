using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNPC : MonoBehaviour
{
    PlayerStatus status;
    List<GameObject> prefabList = new List<GameObject>();
    [Header("Cop Prefabs")]
    public GameObject Cop;
    public GameObject UnderCover1;

    [Header("Regular NPCs")]
    public GameObject Prefab1;
    public GameObject Prefab2;
    public GameObject Prefab3;
    public GameObject Prefab4;
    public GameObject Prefab5;


    private int copChance = 0;
    private int prefabIndex;
    // Heat increases chance of cops appearing?
    // Heat as a value from 0-1, multiply it by 
    // Start is called before the first frame update
    void Start()
    {
        status = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();

    }
    public void PopulateList()
    {
        float heat = status.GetHeat();
        for (int j = 0; j < heat / 10; j++)
            copChance++;
        // Just add all the different prefabs to the list and grab a prefab from that list to spawn next.
        for (int i = 0; i < copChance; i++) // For each (20 or whatever) amount of heat, add 1 to copChance and add one cop.
        {
            if (heat > 30f)
                prefabList.Add(UnderCover1);
            else
                prefabList.Add(Cop);
        }
        prefabList.Add(Prefab1);
        prefabList.Add(Prefab2);
        prefabList.Add(Prefab3);
        prefabList.Add(Prefab4);
        prefabList.Add(Prefab5);
        prefabIndex = Random.Range(0, prefabList.Count - 1);
    }
    public void Spawn(Vector3 location)
    {
        // Need to set a location and rotation for this spawn.  
        Instantiate(prefabList[prefabIndex], location, Quaternion.Euler(0f,-90f,0f)); // Spawn a random prefab from the list.
    }
    // Update is called once per frame
    void Update()
    {

    }

}

