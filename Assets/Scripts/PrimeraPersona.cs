using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimeraPersona : MonoBehaviour
{
    private CharacterController cc;
    public float velocidad = 12;

    public float Gravedad = -9.81f;
    public Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask floorMask;
    bool isGrounded;
    private Animator miAnimator;
    private float x, z;
    public bool mover = true;
    // Start is called before the first frame update
    void Start()
    {
        miAnimator = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance,
            floorMask);
        if(isGrounded && velocity.y <0)
        {
            velocity.y = -2f;
        } 
        if(Input.GetButtonDown("Jump")&& isGrounded)
        {
            velocity.y = Mathf.Sqrt(3 * -2 * Gravedad);
            miAnimator.SetTrigger("Saltar");
        }

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        cc.Move(velocity * Time.deltaTime);

        cc.Move(move * velocidad * Time.deltaTime);
    }
}
