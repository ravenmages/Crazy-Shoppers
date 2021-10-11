using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu (fileName = "ItemData", menuName = "ScriptableObjects/ItemDataObj")]

public class interactItem : ScriptableObject
{

    public float weight;
    public int pointVal;
    public Sprite icon;

}
