using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{

    public GameObject pooledObject;
    public int pooledAmountInicial = 20;
    public bool willGrow = true;

    List<GameObject> pooledObjects;



    // Use this for initialization
    void Awake()
    {
        pooledObjects = new List<GameObject>();

        for (int i = 0; i < pooledAmountInicial; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject, transform);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }

    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject, transform);
            pooledObjects.Add(obj);
            return obj;
        }

        return null;
    }

}
