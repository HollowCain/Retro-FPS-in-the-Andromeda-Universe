using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    public void playGame()
    {
        SceneManager.LoadScene("Main_tutorial_scene");
    }
    public void quitGame()
    {
        Application.Quit();
    }
}
