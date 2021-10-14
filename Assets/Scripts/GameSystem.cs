using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public static GameSystem Instance;
    [SerializeField] private PoolController[] ObjectPools;
    [SerializeField] private Transform StartPosition;
    [SerializeField] private Transform EndPosition;

    [SerializeField] private MeshRenderer MainRailRenderer;

    [SerializeField] private Transform SideRails;


    [Header("Mover")]
    [SerializeField] private Transform[] MoverPositions;
    [SerializeField] private PoolController MoverObjectPool;

    [SerializeField] private Color[] Colors;
    int MoverCount = 0;


    void Awake()
    {
        Instance = this;
        
    }

    void Start()
    {
        MainRailRenderer.material.SetFloat("Vector1_CCDBA678",Globals.Instance.GetRailSpeed());
        InvokeRepeating("AddObjectToRail",3,5f);

        float moverAddSpeed = Globals.Instance.GetObjectSpeed();
        InvokeRepeating("AddMover",0,moverAddSpeed/12f);

        CreateLevel();
    }

    void CreateLevel()
    {
        Transform[] railPositions = Globals.Instance.GetLevelPositions();
        for(int i=0; i<SideRails.transform.childCount; i++)
        {
            Transform selectedRail = SideRails.GetChild(i);
            selectedRail.position = railPositions[i].position;
            selectedRail.rotation = railPositions[i].rotation;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddMover()
    {
        Vector3 moverPos = MoverPositions[0].position;
        int direction = -1;

        if(MoverCount % 2 == 0)
        {
            direction = 1;
            moverPos = MoverPositions[1].position;
        }
        
        GameObject cloneMover = MoverObjectPool.GetFromPool();
        cloneMover.transform.position = moverPos;
        cloneMover.GetComponent<MoverObject>().SetStatus(true);
        cloneMover.GetComponent<MoverObject>().SetDirection(direction);
        cloneMover.GetComponent<MoverObject>().SetPositions(MoverPositions[0].position,MoverPositions[1].position);
        MoverCount++;
    }

    void AddObjectToRail()
    {
        int random = Random.Range(0,ObjectPools.GetLength(0));
        GameObject railObject = ObjectPools[random].GetFromPool();
        railObject.transform.position = StartPosition.position;
        railObject.GetComponent<RailObject>().SetStatus(true);
    }

    public void ChangeMoverDirection(Vector3 position, int direction)
    {
        List<Transform> movers = new List<Transform>();

    
        foreach(Transform mover in MoverObjectPool.transform)
        {
            if(Vector3.Distance(mover.position,position) < 1.8f)
            {
                MoverObject temp = mover.GetComponent<MoverObject>();
                if(temp.GetDirection() == direction)
                {
                    if(movers.Count > 0)
                    {
                        bool isInserted = false;
                        for(int i=0; i<movers.Count; i++)
                        {
                            if(mover.position.z < movers[i].position.z)
                            {
                                movers.Insert(i,mover);
                                isInserted = true;
                                break;
                            }
                        }

                        if(!isInserted)
                        {
                            movers.Add(mover);
                        }
                    }
                    else
                    {
                        movers.Add(mover);
                    }
                }   
            }

        }
        
        for(int i=0; i<movers.Count; i++)
        {
            int val = movers.Count - i - 1;
            movers[i].GetComponent<MoverObject>().ChangeDirection(val * 0.1f);
        }
    }

    public Vector3 GetEndPosition()
    {
        return EndPosition.position;
    }

    public Color[] GetColors()
    {
        return Colors;
    }

    public PoolController GetPoolController(int category)
    {
        return ObjectPools[category];
    }
}
