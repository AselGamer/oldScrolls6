using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPerspectiva : MonoBehaviour
{
    public GameObject primeraPersona;
    public GameObject terceraPersona;
    public GameObject cameraBrain;
    public GameObject cameraController;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            CambiarPerspectiva();
        }
    }

    void CambiarPerspectiva()
    {
        if (primeraPersona.activeSelf)
        {
            cameraBrain.SetActive(true);
            cameraController.SetActive(true);
            primeraPersona.SetActive(false);
            terceraPersona.transform.position = primeraPersona.transform.position;
            terceraPersona.transform.rotation = primeraPersona.transform.rotation;
            terceraPersona.SetActive(true);
        }
        else 
        {
            terceraPersona.SetActive(false);
            primeraPersona.transform.position = terceraPersona.transform.position;
            primeraPersona.transform.rotation = terceraPersona.transform.rotation;
            primeraPersona.SetActive(true);
        }
    }
}
