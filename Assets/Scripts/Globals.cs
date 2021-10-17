using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour
{
    public static Globals Instance;

    [SerializeField] private GameObject LevelParent;

    int CurrentLevel = 0;

    float BaseRailSpeed = 1f;
    float RailSpeedMultiplier = 1.02f;

    float BaseMoverSpeed = 3f;
    float MoverSpeedMultiplier = 1.02f;

    float BaseCargoCount = 20f;
    float CargoCountMultiplier = 1.1f;


    void Awake()
    {
        Instance = this;
    }



    public int GetCurrentLevel()
    {
        return CurrentLevel;
    }

    public float GetRailSpeed()
    {
        float speed = BaseRailSpeed;
        int level = GetCurrentLevel();

        for(int i=0; i<level; i++)
        {
            speed = speed * RailSpeedMultiplier;
        }

        return speed;
    }

    public float GetObjectSpeed()
    {
        return GetRailSpeed() * 2;
    }

    public float GetMoverSpeed()
    {
        float speed = BaseMoverSpeed;
        int level = GetCurrentLevel();

        for(int i=0; i<level; i++)
        {
            speed = speed * MoverSpeedMultiplier;
        }

        return speed;
    }

    public Transform[] GetLevelPositions()
    {
        int level = GetCurrentLevel();
        List<Transform> positions = new List<Transform>();
        Transform selectedLevel = LevelParent.transform.GetChild(level);

        for(int i=0; i<selectedLevel.childCount; i++)
        {
            positions.Add(selectedLevel.GetChild(i));
        }

        return positions.ToArray();
    }

    public void SetLevelDecorations()
    {
        int level = GetCurrentLevel();
        int decorationIndex = LevelParent.transform.GetChild(level).childCount-1;
        LevelParent.transform.GetChild(level).GetChild(decorationIndex).gameObject.SetActive(true);
    }

    public int GetCargoCount()
    {
        float count = BaseCargoCount;
        int level = GetCurrentLevel();

        for(int i=0; i<level; i++)
        {
            count = count * CargoCountMultiplier;
        }

        return (int)count;
    }




    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
