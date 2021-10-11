using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{

    [SerializeField] private float cameraSpeed;
    [SerializeField] private Transform targ;
    [SerializeField] private Vector3 offset;

    private Transform self;
    private Vector3 destination;

    // Start is called before the first frame update
    void Start()
    {
        self = transform;
        self.position = transform.position + offset;
    }

    // Update is called once per frame
    void Update()
    {
        if(targ != null)
            follow();
    }

    private void follow() {

        destination = targ.position + offset;
        self.position = Vector3.Lerp(self.position, destination, Time.deltaTime * cameraSpeed);

    }



}
