using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailObject : MonoBehaviour
{
    [SerializeField] private int Category = -1;
    Rigidbody ObjectRigidbody;
    bool Status;
    Vector3 LastMoveDirection;
    float Speed;
    Outline OutlineScript;
    int SelectedColor = -1;

    Quaternion StartAngle;

    void Awake()
    {


    }
    void Start()
    {
        StartAngle = transform.rotation;
        OutlineScript = transform.GetComponent<Outline>();
        Speed = Globals.Instance.GetObjectSpeed();
        ObjectRigidbody = transform.GetComponent<Rigidbody>();
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
            InitSettings();
            ObjectRigidbody.isKinematic = false;
            
        }
        else
        {
            ObjectRigidbody.isKinematic = true;
        }

        Status = status;
    }

    void InitSettings()
    {
        transform.rotation = StartAngle;
        ObjectRigidbody.velocity = Vector3.zero;
        ObjectRigidbody.angularVelocity = Vector3.zero;

        Color[] colors = GameSystem.Instance.GetColors();
        SelectedColor = Random.Range(0,colors.GetLength(0));
        OutlineScript.OutlineColor = colors[SelectedColor];
    }

    public int GetObjectColor()
    {
        return SelectedColor;
    }

    public int GetCategory()
    {
        return Category;
    }

    public void SendToPool()
    {
        
        GameSystem.Instance.GetPoolController(Category).AddToPool(gameObject);
    }
}
