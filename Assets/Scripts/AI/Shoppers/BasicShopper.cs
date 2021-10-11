using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicShopper : MonoBehaviour
{
    ItemManager itemManager;
    private Item targetItem;
    private Transform targetTransform;
    private Collider targetCollider;
    [SerializeField] private float searchDis;

    private float stoppingDis;

    ActionTimer requestTimer;
    NavMeshAgent self;
    bool targFound;

    [SerializeField] private Animator anim;
    [SerializeField] private GameObject poofEffect;
    private AudioSource poofSound;

    private void Start() {
        itemManager = ItemManager.instance;
        self = GetComponent<NavMeshAgent>();
        targFound = false;
        stoppingDis = self.stoppingDistance;
        poofSound = GetComponent<AudioSource>();
    }

    private void Update() {
        
        if(self.velocity != Vector3.zero) {
            anim.SetBool("isMoving", true);
        }
        else {
            anim.SetBool("isMoving", false);
        }


        if(targFound == true) {

            if (targetItem != null) {

                bool reachedTarget = atTarget(transform.position, targetTransform.position, searchDis);

                if (reachedTarget) {
                    onItemReached();
                }

            }

            else {
                submitPathRequest();
            }

        }
        else {
            submitPathRequest();
        }

    }

    private bool atTarget(Vector3 pos, Vector3 targetPos, float offset) {
        bool result = false;

        Collider[] colliders = Physics.OverlapSphere(pos, offset);

        foreach (Collider col in colliders) {
            if (col == targetCollider) {
                result = true;
                break;
            }
        }

        return result;
    }

    //If it dies, it needs to return the item back to the manager so something else can go to it
    private void OnDestroy() {
        if (targetItem != null) {
            itemManager.returnItem(targetItem);
        }
    }

    private void onTargFound(Item item) {

        if (item != null) {
            targetItem = item;
            targetTransform = item.transform;
            targetCollider = item.GetComponent<Collider>();
            targFound = true;
            self.SetDestination(targetTransform.position);
        }
        else {
            Destroy(gameObject);
        }

    }

    private void submitPathRequest() {
        itemManager.submitTargItemRequest(onTargFound);
    }

    private void onItemReached() {

        Instantiate(poofEffect, targetItem.transform.position, Quaternion.identity);
        poofSound.Play();
        Destroy(targetItem.gameObject);
        targFound = false;
    }
}
