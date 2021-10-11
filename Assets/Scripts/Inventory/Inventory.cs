using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public static Inventory instance;


    [SerializeField] private float maxWeight;
    [SerializeField] private int maxSlots;

    public float MaxWeight { get; set; }
    public float CurrentWeight {get; set;}

    public List<StoredItem> heldItems;
    PlayerInteractManager manager;

    public event Action OnPickUp;
    public event Action<float> OnDeposit;

    [SerializeField] private GameObject poofEffect;
    private AudioSource poofSound;

    private void Awake() {

        instance = this;

        CurrentWeight = 0;
        MaxWeight = maxWeight;
    }

    void Start()
    {

        heldItems = new List<StoredItem>();

        manager = GetComponent<PlayerInteractManager>();
        manager.addInteractCallback(pickUpItem);
        manager.addInteractCallback(depositItem);

        poofSound = GetComponent<AudioSource>();
    }


    public bool isEmpty() {
        return (heldItems.Count == 0);
    }

    //clears inventory and returns the point value
    private float clearInventory() {
        float totalPoints = 0;

        foreach (StoredItem item in heldItems) {
            totalPoints += item.itemAmount * item.pointVal; 
        }

        heldItems.Clear();
        CurrentWeight = 0;

        return totalPoints;
    }

    //deposits item into the box and triggers an event to update score + UI
    private void depositItem(GameObject obj) {

        DepositBox depositBox = obj.GetComponent<DepositBox>();

        if(depositBox != null) {
            float points = clearInventory();

            //update the deposit box score count
            OnDeposit?.Invoke(points);
        }


    }

    //Picks up an item and triggers an event that updates UI
    private void pickUpItem(GameObject item) {

        
        Item newItem = item.GetComponent<Item>();

        if (newItem != null) {
            

            //This one assumes you only have a max of 6 items in the game
            bool canCarry = (CurrentWeight + newItem.getWeight() <= MaxWeight);

            if (canCarry) {

                //Store the info of the item so we can revert weight and add points when we drop off items
                StoredItem currentItem = new StoredItem(newItem.getName(), newItem.getPointVal(), newItem.getWeight(), newItem.getIcon());
                bool worked = addToInventory(currentItem);


                if (worked) {
                    
                    //Invoke all subscribes to this event
                    //This updates the UI (which has already subbed to the event) when an item is picked up

                    OnPickUp?.Invoke();

                    //Destroy the item from the game world
                    Instantiate(poofEffect, item.transform.position, Quaternion.identity);
                    poofSound.Play();
                    Destroy(item);
                }
            }
        }

    }

    private StoredItem duplicateItem(StoredItem newItem) {

        StoredItem duplicate = null;

        for (int i = 0; i < heldItems.Count; i++) {

            if (heldItems[i].Equals(newItem)) {
                duplicate = heldItems[i];
                break;
            }

        }

        return duplicate;
    }


    //prevents duplicates in the list for the inventory
    private bool addToInventory(StoredItem newItem) {

        bool success = false;

        StoredItem duplicate = duplicateItem(newItem);

        if (duplicate != null) {
            duplicate.itemAmount += 1;
            success = true;
        }
        else if (heldItems.Count < maxSlots){
            heldItems.Add(newItem);
            success = true;
        }

        if(success)
            CurrentWeight += newItem.weight;

        return success;

    }
}