using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;



public class waveManager : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }


    public GameObject[] spawnPoints;
    public Vector2[] wavesFeature;
    public int currentWave;
    public int[] indexofSpawners;

    public List<Pool> pools;    
    public Dictionary<string,Queue<GameObject>> poolDictionary = new Dictionary<string,Queue<GameObject>>();

    private void Start()
    {
        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for(int i=0; i<pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    void WaveStarted()
    {
        if(currentWave < wavesFeature.Length)
        {
            int closeNumber = (int)wavesFeature[currentWave].x;
            int rangeNumber = (int)wavesFeature[currentWave].y;

            for(int i=0; i<closeNumber; i++)
            {
                
                //close number kadar farklý spawner sec ve spawn et
                while (true)
                {
                    int selectedNum = Random.Range(0, spawnPoints.Length);
                    GameObject selectedObj = spawnPoints[selectedNum];
                    if(selectedObj.activeSelf == true)
                    {
                        break;
                    }
                }

                GameObject objtoSpawn = poolDictionary["close"].Dequeue();

                objtoSpawn.SetActive(true);
                objtoSpawn.transform.position = selectedObj.transform.position;
                selectedObj.SetActive(false);
            }
            for (int i = 0; i < rangeNumber; i++)
            {
                int selectedNum = Random.Range(0, spawnPoints.Length);
                GameObject selectedObj = spawnPoints[selectedNum];
                GameObject objtoSpawn = poolDictionary["close"].Dequeue();

                objtoSpawn.SetActive(true);
                objtoSpawn.transform.position = selectedObj.transform.position;
                selectedObj.SetActive(false);
            }
        }
    }
}
