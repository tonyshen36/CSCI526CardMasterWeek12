using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RetryGame : MonoBehaviour
{   
    public void RestartButton()
    {
        SceneManager.LoadScene("Main");
    }
    
    public void ExitButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void TutorialButton()
    {
        SceneManager.LoadScene("tutorial");
    }


}
