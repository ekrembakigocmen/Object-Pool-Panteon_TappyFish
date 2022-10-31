using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectPool : MonoBehaviour
{
  
    [Serializable]
    public struct Pool
    {

        public Queue<GameObject> pooledObjects;
        public GameObject objPrefab;
        public int poolSize;
    }

    [SerializeField] private Pool[] pools;
    private void Awake()
    {
        for (int i = 0; i < pools.Length; i++)
        {

            pools[i].pooledObjects = new Queue<GameObject>();

            for (int j = 0; j < pools[i].poolSize; j++)
            {
                GameObject obj = Instantiate(pools[i].objPrefab);

                obj.SetActive(false);
                pools[i].pooledObjects.Enqueue(obj);
            }
        }
    }

    public GameObject GetPooledObject(int objType)
    {

        if (objType >= pools.Length) return null;
        

        GameObject obj = pools[objType].pooledObjects.Dequeue();
        obj.SetActive(true);
        pools[objType].pooledObjects.Enqueue(obj);
        return obj;

    }
}
