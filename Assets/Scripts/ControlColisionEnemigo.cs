using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlColisionEnemigo : MonoBehaviour
{
    private ControlEnemigo crEnemigo;

    void Start()
    {
        crEnemigo = GetComponentInParent<ControlEnemigo>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.transform.name);
        if (other.transform.tag == "Sword")
        {
            crEnemigo.GetHit();
        }
    }
}
