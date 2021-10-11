using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthBarScript : MonoBehaviour
{


    private Health health;
    [SerializeField] private Slider slider;

    private void Start() {

        health = GetComponent<Health>();
        SetMaxHealth(health.getHealth());
        health.onDamage += setHealth;
    }


    public void SetMaxHealth(float health) {
        slider.maxValue = health;
        slider.value = health;
    }

    public void setHealth(float health) {
        slider.value = health;
    }
 
}
