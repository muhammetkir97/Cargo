using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverObject : MonoBehaviour
{
    BoxCollider ObjectCollider;
    Vector3 Pos1,Pos2,TargetPos;
    bool Status = false;
    float Speed = 0;
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
                transform.Translate(Dir * Speed * Time.deltaTime);

                if(Mathf.Abs(TargetPos.x - transform.position.x) < 0.05f)
                {
                    IsChangeDirection = false;
                    ObjectCollider.enabled = false;
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

    public void ChangeDirection()
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
