using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Health : MonoBehaviour
{

    DepositBox box;
    public event Action<float> onDamage;
    [SerializeField] private float health;
    [SerializeField] private GameObject poofEffect;
    private float maxHealth;

    private AudioSource poofSound;

    private void Start() {
        box = DepositBox.instance;
        poofSound = GetComponent<AudioSource>();
        maxHealth = health;

    }

    public float getHealthPercent() {
        return (health / maxHealth);
    }

    public float getHealth() {
        return health;
    }

    public void takeDamage(float dmg) {
        health -= dmg;

        if (isDead()) {
            onDie();
        }

        onDamage?.Invoke(health);

    }

    private bool isDead() {
        return (health <= 0);
    }


    private void onDie() {

        Instantiate(poofEffect, transform.position, Quaternion.identity);
        poofSound.Play();

        Destroy(gameObject);

        if (gameObject.tag == "Player")
            box.levelLost();

    }

}
