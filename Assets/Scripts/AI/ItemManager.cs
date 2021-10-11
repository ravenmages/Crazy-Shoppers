using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    
    public static ItemManager instance;
    Queue<Item> ItemHolder = new Queue<Item>();
    private static System.Random rand = new System.Random();

    private void Awake() {
        instance = this;
    }

    private void Start() {
        getItemsInScene();
    }

    private bool managerIsEmpty() {
        return (ItemHolder.Count == 0);
    }



    private void randomizeList(List<Item> list) {
        int n = list.Count;

        while(n > 1) {
            n--;
            int k = rand.Next(n + 1);
            Item item = list[k];
            list[k] = list[n];
            list[n] = item;
        }

    }

    public void returnItem(Item item) {
        ItemHolder.Enqueue(item);
    }

    public void submitTargItemRequest(Action<Item> callback) {

        //If the list of items isnt empty, call the callback function and give it the new item
        if (ItemHolder.Count > 0) {
            Item current = ItemHolder.Dequeue();

            callback(current);
        }

    }

    void getItemsInScene() {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("item");

        List<Item> temp = new List<Item>();

        foreach (GameObject obj in objects) {

            Item item = obj.GetComponent<Item>();

            if(item !=null)
                temp.Add(item);
        }

        randomizeList(temp);

        foreach(Item item in temp) {
            ItemHolder.Enqueue(item);
        }

    }


}
