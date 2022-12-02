using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int initialCropRows;
    public int initialCropColumns;

    public GameObject potatoManager;
    public GameObject potatoGun;
    public GameObject gameOverScreen;

    private bool initPotatoesSpawned = false;
    private bool gameOver = false;
    private const int defaultAmmo = 250;
    private static int ammoTracker = defaultAmmo; //tracks ammoCount between levels

    void Start()
    {
        potatoGun.GetComponent<PotatoGunScript>().setAmmoCount(ammoTracker);
        Timer.levelTime = Timer.startLevelTime;
        if (potatoManager == null)
            potatoManager = GameObject.FindWithTag("Potato Manager");
    }

    void Update()
    {
        if (!initPotatoesSpawned)
            SpawnInitPotatoes();

        //Dev Keys for switching between levels
        if(Input.GetKeyDown("]"))
        {
            //advance one level
            Timer.levelTime = 0;
        }
        if(Input.GetKeyDown("["))
        {
            Debug.Log(SceneManager.GetActiveScene().name);
            //go back one level
            if (SceneManager.GetActiveScene().name == "Level 1")
            {
                ResetAmmoTracker();
                Timer.levelTime = Timer.startLevelTime;
                SceneManager.LoadScene("Menu");
            }
            else if (SceneManager.GetActiveScene().name == "Level 2")
            {
                TrackAmmo();
                Timer.levelTime = Timer.startLevelTime;
                SceneManager.LoadScene("Level 1");
            }
            else if (SceneManager.GetActiveScene().name == "Level 3")
            {
                TrackAmmo();
                Timer.levelTime = Timer.startLevelTime;
                SceneManager.LoadScene("Level 2");
            }
        }

        //lose condition
        if (!gameOver && /*potatoGun.GetComponent<PotatoGunScript>().getAmmoCount() == 0 &&*/ potatoManager.GetComponent<PotatoManager>().PotatoCount() == 0)
        {
            Timer.levelTime = 0;
            ResetAmmoTracker();
            gameOver = true;
            gameOverScreen.SetActive(true);
        }

        //handle end of level
        if (Timer.levelTime <= 0)
        {
            //harvest all potato crops
            GameObject[] potatoManagers = GameObject.FindGameObjectsWithTag("Potato Manager");
            foreach (GameObject pm in potatoManagers)
                pm.GetComponent<PotatoManager>().HarvestAllPotatoes();

            TrackAmmo();
            
            //TODO: Add Next Level Menu for inbetween levels

            //go to next level
            if(SceneManager.GetActiveScene().name == "Level 1")
            {
                Timer.levelTime = Timer.startLevelTime;
                SceneManager.LoadScene("Level 2");
            }
            else if(SceneManager.GetActiveScene().name == "Level 2")
            {
                Timer.levelTime = Timer.startLevelTime;
                SceneManager.LoadScene("Level 3");
            }
            //TODO: Add win screen upon completing level 3
            else
            {
                ResetAmmoTracker();
                Timer.levelTime = Timer.startLevelTime;
                SceneManager.LoadScene("Menu");
            }
        }
    }

    private void SpawnInitPotatoes()
    {
        PotatoManager pm = potatoManager.GetComponent<PotatoManager>();
        if (!pm.initialized)
            return;
        pm.SpawnPotatoesRectangle(new Vector3(0, 0, 1), initialCropRows, initialCropColumns);
        initPotatoesSpawned = true;
    }

    private void TrackAmmo()
    {
        //record ammoCount
        ammoTracker = potatoGun.GetComponent<PotatoGunScript>().getAmmoCount();
    }

    private void ResetAmmoTracker()
    {
        ammoTracker = defaultAmmo;
    }
}
