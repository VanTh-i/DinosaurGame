using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnableObjects
    {
        public GameObject prefab;
        [Range(0f, 1f)]
        public float spawnChance;
    }
    public SpawnableObjects[] objects;

    private void OnEnable()
    {
        Invoke(nameof(Spawn), Random.Range(1f, 2f));
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Spawn()
    {
        float spawnChance = Random.value;
        foreach (var obj in objects)
        {
            if (spawnChance < obj.spawnChance)
            {
                GameObject obstacle = Instantiate(obj.prefab);
                obstacle.transform.position += transform.position;
                break;
            }
            spawnChance -= obj.spawnChance;
        }

        Invoke(nameof(Spawn), Random.Range(1f, 2f));
    }
}
