using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolController : MonoBehaviour
{
    [SerializeField] private GameObject PoolObject;
    [SerializeField] private int PoolSize;
    Stack<GameObject> PoolStack = new Stack<GameObject>();


    void Awake()
    {
        for(int i=0; i<PoolSize; i++)
        {
            GameObject clone = Instantiate(PoolObject,transform.position,PoolObject.transform.rotation);
            clone.transform.SetParent(transform);
            PoolStack.Push(clone);
        }
    }

    public GameObject GetFromPool()
    {
        GameObject obj = null;
        if(PoolStack.Count > 0)
        {
            obj = PoolStack.Pop();
            //obj.transform.SetParent(null);
        }

        return obj; 
    }

    public void AddToPool(GameObject poolObject)
    {
        //poolObject.transform.SetParent(transform);
        PoolStack.Push(poolObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
