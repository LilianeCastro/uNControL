using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoSingleton<ObjectPooler>
{
    public GameObject pooledObject;
    public int pooledAmount;
    public bool willGrow = true;
    public List<GameObject> polledObjects;

    public override void Init()
    {
        base.Init();

        polledObjects = new List<GameObject>();

        for(int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = Instantiate(pooledObject) as GameObject;
            obj.SetActive(false);
            polledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {

        for(int i = 0; i < polledObjects.Count; i++)
        {
            if(!polledObjects[i].activeInHierarchy)
            {
                return polledObjects[i];
            }
        }

        if(willGrow)
        {
            GameObject obj = Instantiate(pooledObject) as GameObject;
            polledObjects.Add(obj);
            return obj;
        }

        return null;
    }
}
