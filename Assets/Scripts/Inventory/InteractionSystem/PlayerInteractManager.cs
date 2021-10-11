using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerInteractManager : MonoBehaviour
{

    [SerializeField] private LayerMask interactLayer;

    List<Action<GameObject>> subs = new List<Action<GameObject>>();

    [SerializeField] private float searchRadius;

    public String PICKUPKEY;

    private Outline oldInteractableOutline;

    private void Update() {
        searchForInteractable();
    }


    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, searchRadius);

    }

    private void searchForInteractable() {

        int maxObjects = 20;    
        Collider[] interactables = new Collider[maxObjects];
        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, searchRadius, interactables, interactLayer);

        if (oldInteractableOutline != null)
            oldInteractableOutline.enabled = false;

        if (numColliders > 0) {

            Collider closest = findClosest(interactables);

            Outline outline = closest.GetComponent<Outline>();
            outline.enabled = true;

            oldInteractableOutline = outline;

            onKeyPress(closest);

        }

    }

    private void onKeyPress(Collider col) {

        if (Input.GetKeyDown(PICKUPKEY)) {
            onFoundInteractable(col.gameObject);
        }

    }

    private Collider findClosest(Collider[] cols) {

        Collider closest = cols[0];

        Vector3 currentPos = transform.position;
        
        float smallestDis = Vector3.Distance(closest.transform.position, currentPos);

        foreach (Collider col in cols) {

            if(col != null) {

                float checkNewDis = Vector3.Distance(col.transform.position, currentPos);

                if (checkNewDis < smallestDis) {
                    smallestDis = checkNewDis;
                    closest = col;
                }

            }
        }
        
        return closest;
    }


    public void addInteractCallback(Action<GameObject> task) {
        subs.Add(task);
    }

    private void onFoundInteractable(GameObject obj) {
        foreach(Action<GameObject> task in subs) {
            task?.Invoke(obj);
        }
    }

}
