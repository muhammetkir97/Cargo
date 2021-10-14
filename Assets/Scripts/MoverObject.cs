using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverObject : MonoBehaviour
{
    [SerializeField] private PoolController MoverPool;
    BoxCollider ObjectCollider;
    Vector3 Pos1,Pos2,TargetPos;
    bool Status = false;
    float Speed = 0;
    float MoverSpeed = 0;
    int Direction = 0;
    bool IsChangeDirection = false;
    Vector3 Dir = Vector3.zero;


    void Awake()
    {
        
    }

    void Start()
    {
        
        ObjectCollider = transform.GetComponent<BoxCollider>();
        ObjectCollider.enabled = false;
        Speed = Globals.Instance.GetObjectSpeed();
        MoverSpeed = Globals.Instance.GetMoverSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(Status)
        {
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);

            if(IsChangeDirection)
            {
                transform.Translate(Dir * MoverSpeed * Time.deltaTime);

                float distance = 0;

                if(TargetPos.x < 0)
                {
                    distance = transform.position.x - TargetPos.x;
                }
                else
                {
                    distance = TargetPos.x - transform.position.x;
                }

                if(distance <= 0)
                {
                    IsChangeDirection = false;
                    ObjectCollider.enabled = false;
                    transform.position  = new Vector3(TargetPos.x,transform.position.y,transform.position.z);
                    if(Direction == 1)
                    {
                        Direction = -1;
                    }
                    else
                    {
                        Direction = 1;
                    }
                } 

               
            }

            if(transform.position.z > GameSystem.Instance.GetEndPosition().z)
            {
                MoverPool.AddToPool(gameObject);
            }
        }
    }

    public void SetStatus(bool status)
    {
        if(status)
        {
            
        }
        else
        {
            
        }

        Status = status;
    }

    public void SetDirection(int dir)
    {
        Direction = dir;
    }

    public int GetDirection()
    {
        return Direction;
    }

    public void SetPositions(Vector3 pos1, Vector3 pos2)
    {
        Pos1 = pos1;
        Pos2 = pos2;
    }

    public void ChangeDirection(float delay)
    {
        if(!IsChangeDirection) Invoke("LateChangeDirection",delay);
        
    }

    void LateChangeDirection()
    {
        Dir = Vector3.right;
        TargetPos = Pos1;

        if(transform.position.x == Pos1.x)
        {
            Dir = -Vector3.right;
            TargetPos = Pos2;
        }
        
        ObjectCollider.enabled = true;
        IsChangeDirection = true;
    }
}
