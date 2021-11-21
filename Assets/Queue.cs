using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;


public struct Pool
{
    public Queue<GameObject> objQueue;
    public GameObject prefab;
    public int spawnAmount;
}

public class Queue : MonoBehaviour
{
    public int spawnInterval;


    [Serializable]
    public struct Pool
    {
        public Queue<GameObject> objQueue;
        public GameObject prefab;
        public int spawnAmount;
    }

    public Pool[] pool;
    public List<int> intList;
    public Queue<int> intQueue;

    private IEnumerator QueDebug()
    {
        foreach (int number in intQueue)
        {
            Debug.Log(number);
            yield return new WaitForSeconds(1f);
        }
        
    }
    private void Awake()
    {
      //  intList = new List<int>();
        intQueue = new Queue<int>();

        for (int i = 0; i < intList.Count; i++)
        {
            intQueue.Enqueue(intList[i]);
        }

        int k = intQueue.Dequeue();
        intQueue.Enqueue(k);

        intList = intQueue.ToList();
        


        for (int i = 0; i < pool.Length; i++)
        {
            pool[i].objQueue = new Queue<GameObject>();

            for (int j = 0; j < pool[i].spawnAmount; j++)
            {
                GameObject poolObj = Instantiate(pool[i].prefab);
                poolObj.name = i + " " + j;
                poolObj.SetActive(false);
                
                pool[i].objQueue.Enqueue(poolObj);
            }
        }
    }
    
    

    private void Start()
    {
        StartCoroutine(nameof(SpawnObj));
    }

    public GameObject GetPoolObject(int objectType)
    {

        GameObject gObj = pool[objectType].objQueue.Dequeue();
        gObj.SetActive(true);
        pool[objectType].objQueue.Enqueue(gObj);

        return gObj;
    }

    public IEnumerator SpawnObj()
    {
        int count = 0;
        while (true)
        {
            for (int i = 0; i < pool.Length; i++)
            {
                GameObject obj = GetPoolObject(i);
                obj.transform.position = Vector3.zero;
                obj.GetComponent<Renderer>().material.color = new Color(
                    Random.Range(0f, 1f),
                    Random.Range(0f, 1f),
                    Random.Range(0f, 1f));
                yield return new WaitForSeconds(spawnInterval);
            }
        }
    }
}
