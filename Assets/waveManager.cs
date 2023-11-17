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

    public float characterZoneR;

    public GameObject[] spawnPoints;
    public Vector2[] wavesFeature;
    public int currentWave;
    public int[] indexofSpawners;
    bool shouldTeleport;

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

        WaveStarted();
    }

    void WaveStarted()
    {
        if(currentWave < wavesFeature.Length)
        {
            int closeNumber = (int)wavesFeature[currentWave].x;
            int rangeNumber = (int)wavesFeature[currentWave].y;

            for(int i=0; i<closeNumber; i++)
            {
                shouldTeleport = false;

                GameObject selectedObj,objtoSpawn;
                int selectedNum = Random.Range(0, spawnPoints.Length);
                selectedObj = spawnPoints[selectedNum];

                if (!spawnPoints[selectedNum].activeInHierarchy)
                {
                    i--;
                }
                else
                {
                    spawnPoints[selectedNum].SetActive(false);
                    shouldTeleport = true;
                }

                if (shouldTeleport)
                {
                    objtoSpawn = poolDictionary["close"].Dequeue();
                    objtoSpawn.SetActive(true);
                    objtoSpawn.transform.position = selectedObj.transform.position;
                    selectedObj.SetActive(false);
                }
            }
            for (int i = 0; i < rangeNumber; i++)
            {
                shouldTeleport = false;

                GameObject selectedObj, objtoSpawn;
                int selectedNum = Random.Range(0, spawnPoints.Length);
                selectedObj = spawnPoints[selectedNum];

                float differenceX = (selectedObj.transform.position.x - GameObject.Find("Player").transform.position.x);
                float differenceY = (selectedObj.transform.position.y - GameObject.Find("Player").transform.position.y);

                if (!spawnPoints[selectedNum].activeInHierarchy)
                {
                    i--;
                }
                else
                {
                    spawnPoints[selectedNum].SetActive(false);
                    shouldTeleport = true;
                }

                if (shouldTeleport)
                {
                    objtoSpawn = poolDictionary["range"].Dequeue();
                    objtoSpawn.SetActive(true);
                    objtoSpawn.transform.position = selectedObj.transform.position;
                    selectedObj.SetActive(false);
                }
            }
        }
    }
}
