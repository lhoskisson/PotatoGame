using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGameMenu : MonoBehaviour
{
    public static bool pauseGame = false;
    public GameObject menu;
    public GameObject canvas;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p")){
            if(pauseGame){
                Resume();
            }else{
                Pause();
            }
        }
    }
    public void Resume(){
        menu.SetActive(false);
        canvas.SetActive(true);
        Time.timeScale = 1f;
        pauseGame = false;
    }
    public void Pause(){
        menu.SetActive(true);
        canvas.SetActive(false);
        Time.timeScale = 0f;
        pauseGame = true;
    }
    public void QuitGame(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
