using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{

    DepositBox box;
    public Text text;
    string scoreTemplate = "Score:\n{0}/{1}";

    
    // Start is called before the first frame update
    void Start()
    {
        box = DepositBox.instance;
        box.onScoreUpdate += updateUI;

        updateUI(0, box.getMaxPoints());

    }


    void updateUI(float score, float maxScore) {
        text.text = String.Format(scoreTemplate, score, maxScore);
    }

}
