using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField] private ObjectPool _objectPool;

    [SerializeField] private float _spawnInterval;
    
    public float maxY;
    public float minY;
    private float randomY;

    private void Start()
    {
        StartCoroutine(nameof(SpawnRoutine));
    }

   
    private IEnumerator SpawnRoutine()
    {
       
        while (true)
        {
            randomY = Random.Range(minY, maxY);
            var obj = _objectPool.GetPooledObject(0);
            obj.transform.position =new Vector2(7.1f,randomY);
            yield return new WaitForSeconds(_spawnInterval);
        }


    }
}
