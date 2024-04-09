using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlJugador : MonoBehaviour
{

    [Range(1, 30)]
    public float mov_vel = 5f;

    
    //Colision y check suelo
    public Transform tr_gCheck;
    public float f_groundDist = 0.1f;
    public LayerMask lm_groundMsk;

    public float f_grav = -9.81f;

    public bool isGrounded = false;

    private float mov_x, mov_y;

    private Animator animtr;
    private Transform tr;
    private CharacterController cc_controller;

    private Vector3 velocity;


    private void Start()
    {
        cc_controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    private void FixedUpdate()
    {
        Vector3 v3_move = new Vector3(mov_x, 0, mov_y);
        v3_move = v3_move.normalized * mov_vel * Time.deltaTime;
        cc_controller.Move(v3_move);

        velocity.y += f_grav * Time.deltaTime;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1.86f;
        }

        cc_controller.Move(velocity * Time.deltaTime);

    }

    private void Update()
    {
        mov_x = Input.GetAxisRaw("Horizontal");
        mov_y = Input.GetAxisRaw("Vertical");

        isGrounded = Physics.CheckSphere(tr_gCheck.position, f_groundDist, lm_groundMsk);
    }
}
