using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RetryGame : MonoBehaviour
{
    public void RestartButton()
    {
        SceneManager.LoadScene(4);
    }

    public void ExitButton()
    {
        SceneManager.LoadScene(0);
    }

    public void TutorialButton()
    {
        SceneManager.LoadScene(1);
    }

    public void LevelTwoButton()
    {
        SceneManager.LoadScene(7);
    }

}
