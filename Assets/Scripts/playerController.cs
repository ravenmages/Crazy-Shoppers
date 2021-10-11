using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {


    [SerializeField] private Transform cameraHolder;

    [SerializeField] private float speed;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpSpeed;


    [SerializeField] private Transform feet;
    [SerializeField] private LayerMask ground;

    private CharacterController player;
    private float hInput;
    private float vInput;

    [SerializeField] private Animator anim;


    void Start()
    {
        player = GetComponent<CharacterController>();
    }

    
    void Update()
    {
        movement();

    }

    public void movement() {

        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        if(Mathf.Abs(hInput) > 0.01 || Mathf.Abs(vInput) > 0.01) {
            anim.SetBool("isMoving", true);
        }
        else {
            anim.SetBool("isMoving", false);
        }

        Vector3 step = Vector3.zero;


        step = (vInput * cameraHolder.transform.forward) + (hInput * cameraHolder.transform.right);

        step.y = 0;
        step *= speed;

        if (step != Vector3.zero) {
            transform.rotation = Quaternion.LookRotation(step);
        }

        step += applyGravity();

        player.Move(step * Time.deltaTime);

    }

    public bool grounded() {
        return Physics.CheckSphere(feet.position, 0.1f, ground);
    }


    public Vector3 applyGravity() {
        if (!grounded()) {
            return new Vector3(0, -gravity, 0);
        }
        else {
            return Vector3.zero;
        }
    }

    public Vector3 jump() {

        if (Input.GetButtonDown("Jump") && grounded()) {
            return new Vector3(0, jumpSpeed, 0);
        }
        else {
            return Vector3.zero;
        }

    }

}
