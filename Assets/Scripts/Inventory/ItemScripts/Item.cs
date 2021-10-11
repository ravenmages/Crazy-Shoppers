using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    [SerializeField] private interactItem itemData;

    public int getPointVal() {
        return itemData.pointVal;
    }

    public float getWeight() {
        return itemData.weight;
    }

    public Sprite getIcon() {
        return itemData.icon;
    }

    public string getName() {
        return itemData.name;
    }

}
