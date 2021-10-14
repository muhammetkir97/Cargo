using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public static GameSystem Instance;
    [SerializeField] private PoolController ObjectPool;
    [SerializeField] private Transform StartPosition;

    [SerializeField] private MeshRenderer MainRailRenderer;

    [Header("Mover")]
    [SerializeField] private Transform[] MoverPositions;
    [SerializeField] private PoolController MoverObjectPool;
    int MoverCount = 0;


    void Awake()
    {
        Instance = this;
        MainRailRenderer.material.SetFloat("Vector1_CCDBA678",Globals.Instance.GetRailSpeed());
    }

    void Start()
    {
        InvokeRepeating("AddObjectToRail",0,5f);

        float moverAddSpeed = Globals.Instance.GetObjectSpeed();
        InvokeRepeating("AddMover",0,moverAddSpeed/10f);
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
        GameObject railObject = ObjectPool.GetFromPool();
        railObject.transform.position = StartPosition.position;
        railObject.GetComponent<RailObject>().SetStatus(true);
    }

    public void ChangeMoverDirection(Vector3 position, int direction)
    {
        foreach(Transform mover in MoverObjectPool.transform)
        {
            if(Vector3.Distance(mover.position,position) < 3)
            {
                MoverObject temp = mover.GetComponent<MoverObject>();
                if(temp.GetDirection() == direction) temp.ChangeDirection();
            }
        }
    }
}
