using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILevelSlot : MonoBehaviour
{

    [SerializeField] private float levelNum;

    void Start() {

        if (locked()) {
            gameObject.SetActive(false);
        }
        else {
            gameObject.SetActive(true);
        }

    }

    bool locked() {
        return (levelNum > CurrentLevel.level);
    }

}
