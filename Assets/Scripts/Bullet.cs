using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] GameObject hitEffect;
    [SerializeField] LayerMask bullet;

    [SerializeField] Transform hitEffectLocation;

    private GameObject initalShooter;
    private float bulletDamage;
    private Vector3 direction;
    private float speed;


    public void Update() {
        transform.position += direction * speed * Time.deltaTime;
    }

    public void setBullet(float dmg, float _speed, GameObject shooter, Vector3 dir) {

        bulletDamage = dmg;
        speed = _speed;
        initalShooter = shooter;
        direction = dir;

    }

    private void destroyBullet() {
        Destroy(gameObject);
    }

    private void createHitEffect(Vector3 point, Vector3 normal) {

        Quaternion rot = Quaternion.LookRotation(normal);

        Instantiate(hitEffect, point, rot);
    }


    private void OnTriggerEnter(Collider other) {

        //If its not another bullet and not the initial shooter
        if (other.gameObject != initalShooter) { 
            Health target = other.GetComponent<Health>();

            if (target != null) {
                target.takeDamage(bulletDamage);
            }

            Ray ray = new Ray(transform.position - transform.forward, transform.forward);
            RaycastHit hit;

            Vector3 normal;
            
            if (Physics.Raycast(ray, out hit, speed, bullet)) {
                normal = hit.normal;
                createHitEffect(hit.point, normal);
            }

            destroyBullet();
            
        }

    }


}
