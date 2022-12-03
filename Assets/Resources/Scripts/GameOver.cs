using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void Restart()
    {
        Timer.levelTime = Timer.startLevelTime;
        SceneManager.LoadScene("Level 1");
    }

    public void MainMenu()
    {
        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        Timer.levelTime = Timer.startLevelTime;
        SceneManager.LoadScene("Menu");
    }

    public void DisplayWin()
    {
        Transform winTextTransform = transform.Find("WinText");
        if (winTextTransform != null)
            winTextTransform.gameObject.SetActive(true);
    }

    public void DisplayLoss()
    {
        Transform loseTextTransform = transform.Find("LoseText");
        if (loseTextTransform != null)
            loseTextTransform.gameObject.SetActive(true);
    }
}
