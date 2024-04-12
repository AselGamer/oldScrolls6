using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlJugador : MonoBehaviour
{

    [Range(1, 30)]
    public float mov_vel = 5f;

    public Transform tr_cam;

    public GameObject g_sword;

    public GameObject g_swordHitbox;

    private float f_runSpeed = 1f;

    //Colision y check suelo
    public Transform tr_gCheck;
    public float f_groundDist = 0.1f;
    public LayerMask lm_groundMsk;

    public float f_grav = -9.81f;

    public bool isGrounded = false;

    private float mov_x, mov_y;

    private float mov_vel_private = 0;

    private Animator animtr;
    private Transform tr;
    private CharacterController cc_controller;

    private Vector3 velocity;

    // Espada y ataques
    public float f_animLayerWeight = 0.7f;
    private bool isSwordDrawn = false;
    private int atkProg = -1;


    private void Start()
    {
        cc_controller = GetComponent<CharacterController>();
        animtr = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        tr = transform;
        mov_vel_private = mov_vel;
    }


    private void FixedUpdate()
    {
        Vector3 v3_move = tr.right * mov_x + tr.forward * mov_y;
        Vector3 v3_anim = Vector3.right * mov_x + Vector3.forward * mov_y;
        Vector3 v3_moveDelta = v3_move * mov_vel_private * Time.deltaTime;
        cc_controller.Move(v3_moveDelta);

        // Gravedad
        velocity.y += f_grav * Time.deltaTime;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1.86f;
        }

        cc_controller.Move(velocity * Time.deltaTime);

        animtr.SetFloat("vel_y", Mathf.Clamp(v3_anim.z, -1 ,1));
        animtr.SetFloat("vel_x", Mathf.Clamp(v3_anim.x, -1, 1));
        animtr.SetFloat("runSpeed", f_runSpeed);
        animtr.SetInteger("atkProg", atkProg);
        animtr.SetBool("isDrawn", isSwordDrawn);
    }

    private void Update()
    {
        mov_x = Input.GetAxis("Horizontal");
        mov_y = Input.GetAxis("Vertical");

        isGrounded = Physics.CheckSphere(tr_gCheck.position, f_groundDist, lm_groundMsk);

        // Rotar personaje
        tr.forward = Vector3.ProjectOnPlane(tr_cam.forward, Vector3.up);

        if (Input.GetKeyDown(KeyCode.F) && animtr.GetCurrentAnimatorClipInfo(1)[0].clip.name != "drawn2")
        {
            isSwordDrawn = !isSwordDrawn;
        }

        if (Input.GetButtonDown("Fire1") && canAttack())
        {
            atkProg++;
            animtr.SetLayerWeight(2, 1);
            mov_vel_private = 0;
            g_swordHitbox.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            f_runSpeed = 2f;
            mov_vel_private = mov_vel_private * 2;
        } else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (mov_vel_private > mov_vel)
            {
                mov_vel_private = mov_vel;
            }
            f_runSpeed = 1f;
        }
    }

    private bool canAttack()
    {
        if (animtr.GetLayerWeight(2) < 1)
        {
            return animtr.GetCurrentAnimatorStateInfo(1).normalizedTime >= 0.8f && isSwordDrawn;
        }
        else 
        {
            return animtr.GetCurrentAnimatorStateInfo(2).normalizedTime >= 0f && isSwordDrawn && atkProg+1+"" == animtr.GetCurrentAnimatorClipInfo(2)[0].clip.name.Substring(6);
        }  
    }

    private void resetAttack()
    {
        animtr.SetLayerWeight(2, 0);
        atkProg = -1;
        mov_vel_private = mov_vel;
        g_swordHitbox.SetActive(false);
    }

    public void SwordWeightToggle()
    {
        g_sword.SetActive(isSwordDrawn);
        animtr.SetLayerWeight(1, isSwordDrawn ? 1 : f_animLayerWeight);
    }
}
