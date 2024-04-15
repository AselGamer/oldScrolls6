using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControlEnemigo : MonoBehaviour
{
    public GameObject jugador;
    private NavMeshAgent navAgent;
    public float maxDist;
    public float maxDistJugador;
    public GameObject[] puntos;
    public int index = 0;
    private Animator miAnimator;
    private Transform miTransform;

    private float crono = 0;
    public float tiempoActual;
    private bool parado = false;

    public int vida = 3;

    private bool beingHit = false;
    private float navSpeed = 0f;
    public bool attacking = false;

    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        miAnimator = GetComponent<Animator>();
        miTransform = this.transform;
        navAgent.SetDestination(puntos[index].transform.position);
        navSpeed = navAgent.speed;
    }

    private void proximoPunto()
    {
        index++;

        if(index >= puntos.Length)
        {
            index = 0;
        }

        navAgent.SetDestination(puntos[index].transform.position);
        navAgent.speed = 0f;
        parado = true;
    }


    private void Update()
    {
        if (vida <= 0)
        {
            return;
        }

        if (Vector3.Distance(miTransform.position, jugador.transform.position) <= maxDistJugador)
        {
            navAgent.SetDestination(jugador.transform.position);
            navAgent.speed = 6f;
        }
        else if (Vector3.Distance(miTransform.position, navAgent.destination) <= maxDist)
        {
            proximoPunto();
        }
    }

    void FixedUpdate()
    {
        actualizarAnimator();
        if (parado && !attacking)
        {
            crono += Time.deltaTime;
            if(crono >= tiempoActual)
            {
                volverAAndar();
            }
        }
    }

    private void actualizarAnimator()
    {
        if(navAgent.speed < 1)
        {
            miAnimator.SetFloat("VelX", 0);
        }
        else if (navAgent.speed > 1)
        {
            miAnimator.SetFloat("VelX", 1);
        }
        miAnimator.SetInteger("Vidas", vida);
    }

    public void volverAAndar()
    {
        navAgent.speed = 3.5f;
        parado = false;
        crono = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag.Equals("Player"))
        {
            attacking = true;
            parado = true;
            miAnimator.Play("Attack");
        }
    }

    public void GetHit()
    {
        if (!beingHit && vida > 0)
        {
            miAnimator.Play("Head Hit");
            vida--;
            beingHit = true;
        }
    }

    public void StopHit()
    {
        beingHit = false;
        attacking = false;
        parado = false;
    }

    public void StopAttack()
    {
        attacking = false;
    }
}
