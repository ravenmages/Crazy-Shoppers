using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{

    public Image image;
    public Text text;

    public float weight { get; }

    StoredItem item;

    public void addItem(StoredItem newItem) {

        item = newItem;

        image.sprite = item.icon;
        text.text = "x" +  newItem.itemAmount.ToString();

        image.enabled = true;
        text.enabled = true;

    }

    public void clear() {

        item = null;
        image.enabled = false;
        text.enabled = false;

    }

}
