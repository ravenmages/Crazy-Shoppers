using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigureOutlineOnRun : MonoBehaviour
{

    Outline thisOutline;

    void Start()
    {
        thisOutline = GetComponent<Outline>();


    }

    private void LateUpdate() {
        thisOutline.enabled = false;

        Destroy(this);
    }

}
