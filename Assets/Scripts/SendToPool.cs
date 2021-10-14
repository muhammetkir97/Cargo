using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendToPool : MonoBehaviour
{
    [SerializeField] private PoolController MoverObjectPool;
    [SerializeField] private PoolController ObjectPool;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.GetComponent<MoverObject>() != null)
        {
            collision.transform.GetComponent<MoverObject>().SetStatus(false);
            MoverObjectPool.AddToPool(collision.gameObject);
        }
        else if(collision.transform.GetComponent<RailObject>() != null)
        {
            collision.transform.GetComponent<RailObject>().SetStatus(false);
            ObjectPool.AddToPool(collision.gameObject);
        }
    }
}
