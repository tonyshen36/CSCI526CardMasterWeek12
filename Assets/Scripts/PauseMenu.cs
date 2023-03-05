using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public static bool IsPaused = false;

    void Update(){
        if (IsPaused){
            pauseMenu.SetActive(true);
            IsPaused = true;
        } else{
            pauseMenu.SetActive(false);
            IsPaused = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (IsPaused){
                Resume();
            } else {
                Pause();
            }
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    public void Restart(int i)
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
        SceneManager.LoadScene(i);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        IsPaused = false;
        SceneManager.LoadScene(0);
    }
    
}

