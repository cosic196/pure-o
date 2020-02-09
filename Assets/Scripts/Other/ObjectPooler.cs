using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
        public Transform parent;
    }

    public static ObjectPooler Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Duplicate singleton ObjectPooler.");
            Destroy(this);
        }
    }

    public Dictionary<string, Queue<GameObject>> poolDictionary;
    public List<Pool> pools;

	void Start () {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(var pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj;
                if (pool.parent == null)
                {
                    obj = Instantiate(pool.prefab);
                }
                else
                {
                    obj = Instantiate(pool.prefab, pool.parent);
                }
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
	}

    public GameObject SpawnFromPool (string tag, bool setActive = false)
    {
        if(!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + "doesn't exist.");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(setActive);

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
