using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject miniMap;
    public GameObject potato;
    public GameObject potatoDead;
    public void Restart()
    {
        Time.timeScale = 1f;
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
        miniMap.SetActive(false);
        potato.SetActive(false);
        potatoDead.SetActive(false);
        Transform winTextTransform = transform.Find("WinText");
        if (winTextTransform != null)
            winTextTransform.gameObject.SetActive(true);
    }

    public void DisplayLoss()
    {
        miniMap.SetActive(false);
        potato.SetActive(false);
        potatoDead.SetActive(false);
        Time.timeScale = 0f;
        Transform loseTextTransform = transform.Find("LoseText");
        if (loseTextTransform != null)
            loseTextTransform.gameObject.SetActive(true);
    }
}
