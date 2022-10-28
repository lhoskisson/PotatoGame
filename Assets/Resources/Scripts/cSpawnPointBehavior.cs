using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cSpawnPointBehavior : MonoBehaviour
{
    public GameObject carrotPrefab;

    // Start is called before the first frame update
    void Start()
    {
        carrotPrefab = Resources.Load<GameObject>("Prefabs/Carrot");
    }

    // Update is called once per frame
    void Update()
    {
        // try a for loop if you have trouble delaying carrot spawns
        checkCarrotCount();
    }

    public void checkCarrotCount()
    {
        if(CarrotBehavior.carrotCount < 10)
        {
            spawnCarrot();
        }
    }

    private void spawnCarrot()
    {
        // these are the spawn conditions that we can tweak with, this code
        // spawns them within 10% of the x and y outside of the camera view
        /*
        float randX = Random.Range(1f, 1.1f);
        float randY = Random.Range(1f, 1.1f);
        Vector3 randPos = new Vector3(randX, randY, 1f);
        randPos = Camera.main.ViewportToWorldPoint(randPos);
        */
        // increasing carrot count and spawning

        // try spawning the carrots within an area around the spawn point.
        CarrotBehavior.carrotCount++;
        float newX = gameObject.transform.position.x + (Random.Range(1f, 3f));
        float newY = gameObject.transform.position.y + (Random.Range(1f, 3f));
        Vector3 newPos = new Vector3(newX, newY, 0);
        Instantiate(carrotPrefab, newPos, Quaternion.identity);
    }
}
