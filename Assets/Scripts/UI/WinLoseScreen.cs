using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseScreen : MonoBehaviour
{


    private DepositBox box;
    [SerializeField] private GameObject overlay;

    [SerializeField] private bool isWinScreen;
    [SerializeField] private bool isLoseScreen;

    private void Start() {
        box = DepositBox.instance;

        if (isWinScreen)
            box.onWin += enableOverlay;
        else if (isLoseScreen)
            box.onLose += enableOverlay;
    }

    public void onButtonPressed() {
        Time.timeScale = 1;
        SceneManager.LoadScene("LevelSelect");
    }

    private void enableOverlay() {
        overlay.SetActive(true);
    }

}
