using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI text;

    DepositBox box;
    int timerMinutes;
    int timerSeconds;
    string displayTextOne = "{0}:{1}";
    string displayTextTwo = "{0}:{1}{2}";

    void Start()
    {
        box = DepositBox.instance;
    }

    void updateTimerVals() {

        int currentSeconds = Mathf.FloorToInt(box.getTimerVal());
        timerMinutes = Mathf.FloorToInt(currentSeconds / 60);
        timerSeconds = currentSeconds - (timerMinutes * 60);

    }

    void updateTimerText() {

        updateTimerVals();
        if (timerSeconds >= 10)
            text.text = String.Format(displayTextOne, timerMinutes, timerSeconds);
        else
            text.text = String.Format(displayTextTwo, timerMinutes, 0, timerSeconds);
    }

    void Update()
    {
        updateTimerText();
    }
}
