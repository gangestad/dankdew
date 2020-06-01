using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Should probably create a couple different scripts that inherit from this to create differences in strains and so on
public class WeedPlant : Interactable
{
    [SerializeField] private int growTime; // Just used to show the total required growth time for a given strain
    private int curGrowTime;
    [SerializeField] private GrowStage stage = 0;
    private int yield;
    private Daytime daytime;
    private bool nutrients;
    private bool isSick;
    private bool recentlySick;
    private float chanceToGetSick;

    private int seed, sapling, adolescent, adult; // Duration of each stage, growTime should equal total 
    [SerializeField] private Mesh seedMesh, saplingMesh, adolescentMesh, adultMesh, deadMesh;
    private MeshFilter curMesh;
    public float Hydration { get; private set; } = 100.0f;

    // Start is called before the first frame update
    void Start()
    {

        growTime = seed + sapling + adolescent + adult;

    }
    void Awake()
    {
        daytime = FindObjectOfType<Daytime>();
        curMesh = GetComponent<MeshFilter>();
    }
    // Update is called once per frame
    void Update()
    {
        if (daytime.isDay)
        {

        }
    }

    public override void Interact()
    {
        base.Interact();
        // Harvest
        Harvest();
    }



    public void ApplyNutrients()
    {
        if (stage < GrowStage.Adult)
        {
            nutrients = true;
            --growTime;
            switch (stage)
            {
                case GrowStage.Seed:
                    --seed;
                    break;
                case GrowStage.Sapling:
                    --sapling;
                    break;
                case GrowStage.Adolescent:
                    --adolescent;
                    break;
            }
        }
    }
    public void Hydrate(float hydrationPerSecond) // If we're actively watering the plants, otherwise I guess we could just set the hydration level to max again.
    {
        if (Hydration <= 100.0f)
        {
            Hydration += (hydrationPerSecond * Time.deltaTime);
        }
    }
    public void Growth()
    {
        ++curGrowTime;
        if (!isSick) // If the plant is sick it won't grow that day.
        {
            switch (stage)
            {
                case GrowStage.Seed:
                    if (curGrowTime >= seed)
                    {
                        stage++;
                        curMesh.mesh = saplingMesh;
                    }
                    break;
                case GrowStage.Sapling:
                    if (curGrowTime >= (seed + sapling))
                    {
                        stage++;
                        curMesh.mesh = adolescentMesh;

                    }
                    break;
                case GrowStage.Adolescent:
                    if (curGrowTime >= (seed + sapling + adolescent))
                    {
                        stage++;
                        curMesh.mesh = adultMesh;

                    }
                    break;
                case GrowStage.Adult:
                    if (curGrowTime >= (seed + sapling + adolescent + adult))
                    {
                        stage++;
                        curMesh.mesh = deadMesh;
                    }
                    break;
            }
        }
        // Reset stats for a new day
        Hydration = 0;
        nutrients = false;
        if (stage < GrowStage.Adult && !recentlySick) // to avoid getting sick twice in a row
        {
            float getSick = Random.Range(0.0f, 100.0f);
            if (getSick >= chanceToGetSick)
                isSick = true;
        }
        recentlySick = false;
    }
    public int Harvest()
    {
        if (stage == GrowStage.Dead)
        {
            return 0;
        }
        else
        { return yield; }
    }
    public void TreatPlant()
    {
        isSick = false;
    }
}
enum GrowStage
{
    Seed = 0,
    Sapling = 1,
    Adolescent = 2,
    Adult = 3,
    Dead = 4,
}
