using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointManager : MonoBehaviour
{
    private float time = 0.0f;
    public float timeDelay = 5.0f;
    public bool delayActive;
    public bool isFirstRun = true;

    // will be made an GameObject[] later so multiple spawnpoints can be loaded into the manager 
    /* 
      may not be necessary if each spawnpoint has its own manager, this approach may be necessary
      for each spawnpoint to be able to keep track of its time and timedelay. To do this each level
      will need to have a spawnpoint manager placed into the spawnpoint in the inspector rather than 
      using GameObject.Find("SpawnPointManager") in SpawnPointBehavior.
    */
    public GameObject loadedSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (delayActive)
            disableSpawner(loadedSpawnPoint);
    }

    public void disableSpawner(GameObject spawner)
    {
        if(isFirstRun) // trying to avoid running unnecessary code each frame with this if
        {
            delayActive = true;
            spawner.SetActive(false);
            isFirstRun = false;
        }
        time += Time.deltaTime;
        if (time >= timeDelay)
        {
            delayActive = false;
            time = 0f;
            spawner.GetComponent<SpawnPointBehavior>().refillHealth();
            spawner.SetActive(true);
            isFirstRun = true;
        }
    }

    public void setSpawnPoint(GameObject spawner)
    {
        loadedSpawnPoint = spawner;
    }
}
