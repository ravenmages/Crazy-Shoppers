using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    public Transform itemParent;
    Inventory inventory;

    InventorySlot[] slots;
    [SerializeField] private Text weightCounter;

    private string weightTextTemplate = "";
    private string weightText = "";

    private void Start() {

        inventory = Inventory.instance;
        inventory.OnPickUp += updateUi;
        inventory.OnDeposit += depositUIUpdate;


        slots = itemParent.GetComponentsInChildren<InventorySlot>();

        weightTextTemplate = "Weight {0}/{1}kg";
        updateWeightCounter();

    }

    private void updateWeightCounter() {
        weightText = String.Format(weightTextTemplate, inventory.CurrentWeight, inventory.MaxWeight);
        weightCounter.text = weightText;
    }

    //need this wrapper because that event has the (float) parameter needed 
    private void depositUIUpdate(float val) {
        updateUi();
    }

    private void updateUi() {

        //Update slots
        for (int i = 0; i < slots.Length; i++) {

            if(i < inventory.heldItems.Count) {
                slots[i].addItem(inventory.heldItems[i]);
            }else {
                slots[i].clear();
            }

        }

        //update weight info

        updateWeightCounter();

    }

}
