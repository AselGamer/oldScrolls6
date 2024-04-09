using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlJugador : MonoBehaviour
{

    [Range(1, 30)]
    public float vert_vel = 5f;

    [Range(1, 30)]
    public float hor_vel = 80f;

    
    //Colision y check suelo
    public Transform tr_gCheck;
    public float f_groundDist = 0.1f;
    public LayerMask lm_groundMsk;

    public bool isGrounded = false;

    private float mov_x, mov_y;

    private Animator animtr;
    private Transform tr;
    private CharacterController cc_controller;


    private void Start()
    {
        cc_controller = GetComponent<CharacterController>();
    }


    private void FixedUpdate()
    {
        cc_controller.Move(new Vector3(mov_x * hor_vel, 0, mov_y * vert_vel) * Time.deltaTime);
    }

    private void Update()
    {
        mov_x = Input.GetAxis("Horizontal");
        mov_y = Input.GetAxis("Vertical");

        isGrounded = Physics.CheckSphere(tr_gCheck.position, f_groundDist, lm_groundMsk);
    }
}
