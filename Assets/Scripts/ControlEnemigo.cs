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

    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        miAnimator = GetComponent<Animator>();
        miTransform = this.transform;
        navAgent.SetDestination(puntos[index].transform.position); 

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

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Vector3.Distance(miTransform.position, jugador.transform.position)<= maxDistJugador)
        {
            navAgent.SetDestination(jugador.transform.position);
            navAgent.speed = 6f;
        }
        else if (Vector3.Distance(miTransform.position, navAgent.destination)<= maxDist)
        {
            proximoPunto();
        }

        actualizarAnimator();
        if (parado)
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
    }

    public void volverAAndar()
    {
        navAgent.speed = 3.5f;
        parado = false;
        crono = 0;
    }
}
