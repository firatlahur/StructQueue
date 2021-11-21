using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exercise
{
    public class Que : MonoBehaviour
    {
        [Serializable]
        public struct Pool
        {
            public Queue<GameObject> queue;
            public GameObject prefab;
            public int spawnAmount;
        }

        public Pool[] pool;
        public float spawnInterval;

        private void Awake()
        {
            for (int i = 0; i < pool.Length; i++)
            {
                pool[i].queue = new Queue<GameObject>();

                for (int j = 0; j < pool[i].spawnAmount; j++)
                {
                    GameObject poolObj = Instantiate(pool[i].prefab);
                    poolObj.SetActive(false);
                    pool[i].queue.Enqueue(poolObj);
                }
            }
        }

        private void Start()
        {
            StartCoroutine(nameof(SpawnObj));
        }

        private GameObject GetQueObj(int objectType)
        {
            GameObject gObj = pool[objectType].queue.Dequeue();
            
            gObj.SetActive(true);
            
            pool[objectType].queue.Enqueue(gObj);

            return gObj;
        }

        private IEnumerator SpawnObj()
        {
            int count = 0;
            while (true)
            {
                GameObject spawnedObj = GetQueObj(count++ % 2);
                spawnedObj.transform.position = Vector3.zero;

                yield return new WaitForSeconds(spawnInterval);
            }
        }
    }
}
