using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static int carrotCount;
    public static int broccoliCount;
    public static int tomatoCount;

    public GameObject carrotPrefab;
    public GameObject broccoliPrefab;
    public GameObject tomatoPrefab;

    // Start is called before the first frame update
    void Start()
    {
        carrotPrefab = Resources.Load<GameObject>("Prefabs/Carrot");
        broccoliPrefab = Resources.Load<GameObject>("Prefabs/Broccoli");
        tomatoPrefab = Resources.Load<GameObject>("Prefabs/Tomato");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setPathingMode(bool mode)
    {
        CarrotBehavior.pathingMode = mode;
        BroccoliBehavior.pathingMode = mode;
        TomatoBehavior.pathingMode = mode;
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

    public void spawnTomato(Vector3 spawnPosition)
    {
        tomatoCount++;
        float newX = spawnPosition.x + (Random.Range(1f, 3f));
        float newY = spawnPosition.y + (Random.Range(1f, 3f));
        Vector3 newPos = new Vector3(newX, newY, 0);
        Instantiate(tomatoPrefab, newPos, Quaternion.identity);
    }

}
