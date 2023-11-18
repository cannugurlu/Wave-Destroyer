using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    bool shouldTeleport;

    public List<Pool> pools;    
    public Dictionary<string,Queue<GameObject>> poolDictionary = new Dictionary<string,Queue<GameObject>>();

    [SerializeField] List<GameObject> currentEnemies = new List<GameObject>();

    private void Awake()
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

    private void Start()
    {
        Invoke(nameof(WaveStarted),0.5f);
        InvokeRepeating(nameof(checkAllDeath), 2.0f, 3.0f);
    }

    private void FixedUpdate()
    { 
    }
    void WaveStarted()
    {
        if(currentWave < wavesFeature.Length)
        {
            int closeNumber = (int)wavesFeature[currentWave].x;
            int rangeNumber = (int)wavesFeature[currentWave].y;
            pullObjects(closeNumber, rangeNumber);
        }
    }

    void pullObjects(int closeNumber,int rangeNumber)
    {
        for (int i = 0; i < closeNumber; i++)
        {
            shouldTeleport = false;

            GameObject selectedObj, objtoSpawn;
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
                currentEnemies.Add(objtoSpawn);
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
                currentEnemies.Add(objtoSpawn);
                objtoSpawn.SetActive(true);
                objtoSpawn.transform.position = selectedObj.transform.position;
                selectedObj.SetActive(false);
            }
        }
    }

    void pushBackObjects()
    {
        foreach(GameObject obj in currentEnemies)
        {
            obj.GetComponent<enemyScript>().health = 100;
            obj.GetComponent<enemyScript>().isDead = false;
            obj.GetComponent<enemyScript>().shouldFollow = true;
            obj.SetActive(false);
        }
        currentEnemies.Clear();
    }


    bool TumDusmanlarOlduMu()
    {
        foreach (GameObject dusman in currentEnemies)
        {
            if (dusman.activeSelf)
            {
                return false;
            }
        }
        return true;
    }



    void checkAllDeath()
    {
        if (TumDusmanlarOlduMu())
        {
            pushBackObjects();
            pushBackObjects();
            currentWave++;
            Invoke("WaveStarted", 3);
        }
    }

}
