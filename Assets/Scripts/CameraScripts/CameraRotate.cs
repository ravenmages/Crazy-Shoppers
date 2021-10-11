using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{

    Transform self;
    [SerializeField] string KEY_LEFTROT;
    [SerializeField] string KEY_RIGHTROT;

    [SerializeField] private float speed;
    private float moveStepVal;

    private void Start() {
        self = transform;
        moveStepVal = speed;
    }

    private void Update() {
        rotate();
    }


    void rotate() {

        float step = 0;  

        if (Input.GetKey(KEY_LEFTROT)) {
            step = moveStepVal * -1;
        }

        else if (Input.GetKey(KEY_RIGHTROT)) {
            step = moveStepVal;
        }

        step *= Time.deltaTime;

        self.Rotate(0, step, 0);

    }

}
