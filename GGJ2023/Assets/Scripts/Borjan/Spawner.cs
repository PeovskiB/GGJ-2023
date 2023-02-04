using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] private float spawnTime = 3f;
    private float spawnTimer;

    //0 for infinity
    [SerializeField] private float destroyTime = 0f;
    [SerializeField] private GameObject spawnPrefab;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = spawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if(spawnTimer <= 0){
            spawnTimer = spawnTime;
            GameObject obj = Instantiate(spawnPrefab, transform.position, Quaternion.identity);
            if(destroyTime != 0){
                Destroy(obj, destroyTime);
            }
        }
    }
}
