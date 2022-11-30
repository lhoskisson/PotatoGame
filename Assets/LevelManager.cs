using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject potatoManager;
    public GameObject potatoGun;

    private bool initPotatoesSpawned = false;
    private bool gameOver = false;

    void Start()
    {
        if (potatoManager == null)
            potatoManager = GameObject.FindWithTag("Potato Manager");
    }

    void Update()
    {
        if (!initPotatoesSpawned)
            SpawnInitPotatoes();

        //lose condition
        if (!gameOver && potatoGun.GetComponent<PotatoGunScript>().getAmmoCount() <= 0 && potatoManager.GetComponent<PotatoManager>().PotatoCount() == 0)
        {
            // resetting the pathing for the enemies so they have the proper pathing mode when the player restarts
            BroccoliBehavior.pathingMode = false;
            CarrotBehavior.pathingMode = false;
            TomatoBehavior.pathingMode = false;

            Timer.levelTime = 0;
            gameOver = true;
        }
    }

    private void SpawnInitPotatoes()
    {
        PotatoManager pm = potatoManager.GetComponent<PotatoManager>();
        if (!pm.initialized)
            return;
        pm.SpawnPotatoes(new Vector3(0, 0, 1), 5, 100);
        initPotatoesSpawned = true;
    }
}
