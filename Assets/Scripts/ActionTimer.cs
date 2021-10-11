using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTimer {

    private float timer;
    private float initalTimerVal;
    private Action OnFinished;
    bool resetable;
    bool done;

    public ActionTimer(float seconds, Action callBack, bool reset = false) {
        timer = seconds;
        initalTimerVal = timer;
        OnFinished = callBack;
        resetable = reset;

        done = false;
    }

    public void reset() {
        timer = initalTimerVal;
    }

    public void countDown() {

        if (timer > 0) {
            timer -= Time.deltaTime;
        }
        else if(!done) {

            if (resetable)
                timer = initalTimerVal;
            else
                done = true;

            OnFinished();
        }


    }
    
}
