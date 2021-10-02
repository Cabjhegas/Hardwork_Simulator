using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{

    public GameObject[] pooledObjectsTemplates;
    public int pooledAmountInicial = 20;
    public bool willGrow = true;

    //o pooledObjects é uma array de listas. Cada lista da array corresponde a um template.
    //E cada item da lista corresponde a uma instancia do obj da pool
    List<GameObject>[] pooledObjects;



    // Use this for initialization
    void Awake()
    {
        pooledObjects = new List<GameObject>[pooledObjectsTemplates.Length];
        for (int i = 0; i < pooledObjects.Length; i++)
        {
            pooledObjects[i] = new List<GameObject>();
        }
        for (int j = 0; j < pooledObjectsTemplates.Length; j++)
        {
            for (int i = 0; i < pooledAmountInicial; i++)
            {
                GameObject obj = (GameObject)Instantiate(pooledObjectsTemplates[j], transform);
                obj.SetActive(false);
                pooledObjects[j].Add(obj);
            }
        }

    }

    public GameObject GetPooledObject()
    {
        int randomTemplateIndex = Random.Range(0, pooledObjectsTemplates.Length);
        for (int i = 0; i < pooledObjects[randomTemplateIndex].Count; i++)
        {
            if (!pooledObjects[randomTemplateIndex][i].activeInHierarchy)
            {
                return pooledObjects[randomTemplateIndex][i];
            }
        }

        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(pooledObjectsTemplates[randomTemplateIndex], transform);
            pooledObjects[randomTemplateIndex].Add(obj);
            return obj;
        }

        return null;
    }

}
