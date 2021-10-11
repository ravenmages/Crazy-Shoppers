using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret : MonoBehaviour
{

    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject turretHead;
    [SerializeField] private GameObject fireSpot;

    [SerializeField] private float damage;
    [SerializeField] private float shootInterval;

    [SerializeField] private float bulletSpeed;

    private ActionTimer shootTimer;

    [SerializeField] private float targetLockInterval;
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask shootableLayer;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private LayerMask includeInLineOfSight;
    private Transform target;
    private ActionTimer searchForTarget;

    private AudioSource source;

    public bool displayGizmos;

    private void Start() {
        shootTimer = new ActionTimer(shootInterval, shoot, true);
        searchForTarget = new ActionTimer(targetLockInterval, findTarget, true);
        source = GetComponent<AudioSource>();
    }

    private void Update() {

        if (target != null) {
            rotateHeadToTarget();
            shootTimer.countDown();
            searchForTarget.countDown();
        }
        else {
            findTarget();
            searchForTarget.reset();
        }

    }

    private void shoot() {
        GameObject newObj = Instantiate(bullet, fireSpot.transform.position, turretHead.transform.rotation);
        Bullet newBullet = newObj.GetComponent<Bullet>();
        newBullet.setBullet(damage, bulletSpeed, turretHead, turretHead.transform.forward);
        source.Play();
    }

    private void rotateHeadToTarget() {

        if (target == null || !inLineOfSight(target.transform)) {
            findTarget();
            searchForTarget.reset();
            return;
        }

        Vector3 relativePos = target.position - turretHead.transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);

        Quaternion currentRot = turretHead.transform.localRotation;

        turretHead.transform.rotation = Quaternion.Lerp(currentRot, rotation, Time.deltaTime * rotationSpeed);
    }

    private bool inLineOfSight(Transform targ) {
        bool result = false;

        Vector3 direction = targ.transform.position - fireSpot.transform.position;


        Ray ray = new Ray(fireSpot.transform.position, direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, attackRadius, includeInLineOfSight)) {
            if (hit.transform == targ) {
                result = true;
                Debug.DrawRay(fireSpot.transform.position, direction, Color.green);
            }
            else {
                Debug.DrawRay(fireSpot.transform.position, direction, Color.red);  
            }
        }

        return result;
    }

    private void findTarget() {

        target = null;

        Collider[] cols = Physics.OverlapSphere(transform.position, attackRadius, shootableLayer);

        if (cols.Length > 0) {
            Collider closest = findClosest(cols);

            if (closest != null) {
                target = closest.transform;
            }

        }


    }


    private Collider findClosest(Collider[] cols) {

        Collider closest = cols[0];
        Vector3 currentPos = transform.position;
        float smallestDis = Vector3.Distance(closest.transform.position, currentPos);

        foreach (Collider col in cols) {

            if (col != null&& inLineOfSight(col.transform)) {

                float checkNewDis = Vector3.Distance(col.transform.position, currentPos);

                if (checkNewDis < smallestDis) {
                    smallestDis = checkNewDis;
                    closest = col;
                }

            }
        }

        
        if(closest == cols[0] && !inLineOfSight(closest.transform)) {
            closest = null;
        }
        
        return closest;
    }


    private void OnDrawGizmos() {

        if (displayGizmos) {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, attackRadius);
        }
    }

}


