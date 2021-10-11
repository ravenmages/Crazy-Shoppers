using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyTimer : MonoBehaviour
{

    [SerializeField] private float lifetime;
    ActionTimer timer;

    private void Start() {
        timer = new ActionTimer(lifetime, destroySelf);
    }

    private void destroySelf() {
        Destroy(gameObject);
    }

    void Update()
    {
        timer.countDown();
    }
}
