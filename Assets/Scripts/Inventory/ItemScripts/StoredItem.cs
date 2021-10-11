using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoredItem
{

    public StoredItem(string itemName, float points, float weightVal, Sprite sprite) {
        Name = itemName;
        pointVal = points;
        weight = weightVal;
        icon = sprite;
        itemAmount = 1;
    }

    public string Name { get; }
    public float pointVal { get; }
    public float weight { get; }
    public Sprite icon { get; }
    public int itemAmount { get; set; }

    public bool Equals(StoredItem other) {

        return this.Name.Equals(other.Name);


    }


}
