using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DepositBox : MonoBehaviour
{

    [SerializeField] int levelNum;
    [SerializeField] private float winPointAmount;
    public static DepositBox instance;
    private float totalPoints;
    Inventory inventory;

    public event Action<float, float> onScoreUpdate;

    //Its in minutes and we will convert it to seconds on Start()
    [SerializeField] private float loseTimer;

    public event Action onWin;
    public event Action onLose;

    [SerializeField] private GameObject poofEffect;
    private AudioSource poofSound;


    private void Awake() {
        instance = this;

        //Unfreezes the time when/if the scene is loaded again
        Time.timeScale = 1;
    }

    void Start()
    {

        loseTimer *= 60;
        totalPoints = 0;
        inventory = Inventory.instance;
        inventory.OnDeposit += updatePoints;
        poofSound = GetComponent<AudioSource>();

    }

    private void Update() {
        loseTimerTick();
    }

    private void loseTimerTick() {
        loseTimer -= Time.deltaTime;

        bool won = totalPoints >= winPointAmount;

        if (loseTimer <= 0 && !(won)) {
            loseTimer = 0;
            levelLost();
        }

    }

    public bool won() {
        return (totalPoints >= winPointAmount);
    }

    private void updatePoints(float points) {

        if (points > 0) {
            Instantiate(poofEffect, transform.position, Quaternion.identity);
            poofSound.Play();
        }

        totalPoints += points;
        totalPoints = Mathf.Clamp(totalPoints, 0, winPointAmount);
        onScoreUpdate?.Invoke(totalPoints, winPointAmount);

        if (won()) {
            levelWon();
        }

    }

    public void freezeTime() {
        Time.timeScale = 0;
    }

    public void levelLost() {
        onLose?.Invoke();
        freezeTime();
    }

    private void levelWon() {

        //If statement is needed to prevent the ability to just play level one over and over to unlock all the levels
        if(CurrentLevel.level <= levelNum) {
            CurrentLevel.level += 1;
        }

        onWin?.Invoke();
        
        freezeTime();

    }

    public float getMaxPoints() {
        return winPointAmount;
    }

    public float getTimerVal() {
        return loseTimer;
    }

}
