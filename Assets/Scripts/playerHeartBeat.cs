using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHeartBeat : MonoBehaviour
{

    [SerializeField] private AudioSource beat;
    [SerializeField] private float healthPercent;

    private Health health;

    private void Start() {
        health = GetComponent<Health>();
    }


    private void Update() {

        print(health.getHealthPercent());

        if (health.getHealthPercent() <= healthPercent) {
            beat.Play();
        }

    }


}
