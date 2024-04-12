using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlFuego : MonoBehaviour
{
    private Transform padre;
    private ParticleSystem.EmissionModule emission1, emission2, emission3, 
        emission4;
    private bool encender;
    private float valor;
    // Start is called before the first frame update
    void Start()
    {
        encender = false;
        valor = 0;
        padre = transform.parent;
        emission1 = GetComponent <ParticleSystem>().emission;
        emission2 = padre.GetChild(1).GetComponent<ParticleSystem>().emission;
        emission3 = padre.GetChild(1).GetComponent<ParticleSystem>().emission;
        emission4 = padre.GetChild(1).GetComponent<ParticleSystem>().emission;
    }

    // Update is called once per frame
  private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("Player")&& !encender)
        {
            emission1.enabled = true;
            emission2.enabled = true;
            emission3.enabled = true;
            emission4.enabled = true;
            encender = true;
            ActivarFuego();
        }
       
    }

    public void ActivarFuego()
    {
        valor++;
        StartCoroutine(Opacidad(valor));

    }
    IEnumerator Opacidad(float valor)
    {
        emission1.rateOverTime = valor;
        emission2.rateOverTime = valor;
        emission3.rateOverTime = valor;
        emission4.rateOverTime = valor;
        yield return new WaitForSeconds(2f);
        if (valor <18)
        {
            ActivarFuego();
        }
        else
        {
            emission1.rateOverTime = 16f;
            emission2.rateOverTime = 16f;
            emission3.rateOverTime = 16f;
            emission4.rateOverTime = 50f;
        }
    }
}
