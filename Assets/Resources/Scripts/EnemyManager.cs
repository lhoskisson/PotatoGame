using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static int carrotCount;
    public static int broccoliCount;

    public GameObject carrotPrefab;
    public GameObject broccoliPrefab;

    // Start is called before the first frame update
    void Start()
    {
        carrotPrefab = Resources.Load<GameObject>("Prefabs/Carrot");
        broccoliPrefab = Resources.Load<GameObject>("Prefabs/Broccoli");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnCarrot(Vector3 spawnPosition)
    {
        // spawning the carrots within an area around the spawn point, area defined by Random.Range()
        carrotCount++;
        float newX = spawnPosition.x + (Random.Range(1f, 3f));
        float newY = spawnPosition.y + (Random.Range(1f, 3f));
        Vector3 newPos = new Vector3(newX, newY, 0);
        Instantiate(carrotPrefab, newPos, Quaternion.identity);
    }

    public void spawnBroccoli(Vector3 spawnPosition)
    {
        broccoliCount++;
        float newX = spawnPosition.x + (Random.Range(1f, 3f));
        float newY = spawnPosition.y + (Random.Range(1f, 3f));
        Vector3 newPos = new Vector3(newX, newY, 0);
        Instantiate(broccoliPrefab, newPos, Quaternion.identity);
    }
 
}
