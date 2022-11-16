using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void Restart()
    {
        Timer.levelTime = Timer.startLevelTime;
        SceneManager.LoadScene("SampleScene");
    }

    public void MainMenu()
    {
        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        Timer.levelTime = Timer.startLevelTime;
        SceneManager.LoadScene("Menu");
    }

}
