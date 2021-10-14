using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomTrigger : MonoBehaviour
{
    [SerializeField] private int ColorNumber;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        RailObject tempObject = other.transform.GetComponent<RailObject>();

        if(tempObject.GetObjectColor() == ColorNumber)
        {
            Debug.Log("dogru");
        }
        else
        {
           Debug.Log("yanlış"); 
        }

        tempObject.SendToPool();



    }
}
