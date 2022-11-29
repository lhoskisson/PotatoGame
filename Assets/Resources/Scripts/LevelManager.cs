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
        if (!gameOver && /*potatoGun.GetComponent<PotatoGunScript>().getAmmoCount() == 0 &&*/ potatoManager.GetComponent<PotatoManager>().PotatoCount() == 0)
        {
            Timer.levelTime = 0;
            gameOver = true;
        }
    }

    private void SpawnInitPotatoes()
    {
        PotatoManager pm = potatoManager.GetComponent<PotatoManager>();
        if (!pm.initialized)
            return;
        pm.SpawnPotatoesRectangle(new Vector3(0, 0, 1), 7, 7);
        initPotatoesSpawned = true;
    }
}
