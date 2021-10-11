using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poofOnDestroy : MonoBehaviour
{

    [SerializeField] private GameObject poofEffect;


    private void OnDestroy() {

        if(this.enabled)
            Instantiate(poofEffect, transform.position, Quaternion.identity);
    }


}
