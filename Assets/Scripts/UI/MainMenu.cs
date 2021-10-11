using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
   
    public void LoadSelectScreen() {
        SceneManager.LoadScene("LevelSelect");
    }

    public void quit() {
        Application.Quit();
    }

}
