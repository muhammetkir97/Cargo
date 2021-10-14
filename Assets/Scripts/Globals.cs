using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour
{
    public static Globals Instance;

    void Awake()
    {
        Instance = this;
    }

    int CurrentLevel = 0;

    float BaseRailSpeed = 1f;
    float RailSpeedMultiplier = 1.02f;

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




    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
