using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailObject : MonoBehaviour
{
    Rigidbody ObjectRigidbody;
    bool Status;
    Vector3 LastMoveDirection;
    float Speed;

    void Awake()
    {
        Speed = Globals.Instance.GetObjectSpeed();
        ObjectRigidbody = transform.GetComponent<Rigidbody>();

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(Status)
        {
            Vector3 moveDirection = GetMovementDirection();

            if(moveDirection == Vector3.zero)
            {
                moveDirection = LastMoveDirection;
            }
            else
            {
                LastMoveDirection = moveDirection;
            }

            //Vector3 moveDirection = Vector3.forward;
            transform.Translate(moveDirection * Speed * Time.deltaTime);
        }
        
    }

    Vector3 GetMovementDirection()
    {
        Vector3 dir = Vector3.zero;
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1))
        {
            dir = -hit.transform.up;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
        }

        return dir;
    }

    public void SetStatus(bool status)
    {
        if(status)
        {
            ObjectRigidbody.isKinematic = false;
        }
        else
        {
            ObjectRigidbody.isKinematic = true;
        }

        Status = status;
    }
}
