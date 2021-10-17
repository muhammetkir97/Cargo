using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rail : MonoBehaviour
{
    [SerializeField] private int ColorNumber;

    GameObject DetectedObject;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        RailObject tempObject = other.transform.GetComponent<RailObject>();

        if(tempObject.GetObjectColor() == ColorNumber)
        {
            GameSystem.Instance.CargoStatus(true);
        }
        else
        {
           GameSystem.Instance.CargoStatus(false);
        }
        DetectedObject = other.transform.gameObject;
        Invoke("DeleteObject",3);
    }

    void DeleteObject()
    {
        DetectedObject.GetComponent<RailObject>().SendToPool();
    }
}
