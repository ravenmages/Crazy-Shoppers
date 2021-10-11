using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{

    public void loadMainMenu() {
        SceneManager.LoadScene(0);
    }


    public void loadLevel(float levelNum) {

        if (CurrentLevel.level >= levelNum) {
            string sceenName = "Level" + levelNum;
            SceneManager.LoadScene(sceenName);
        }
    }

}
