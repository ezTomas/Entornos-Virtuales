using System.Collections;
using System.Collections.Generic;
using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.InputSystem;

public class Disparo : MonoBehaviour
{
    private float cantidadFlecha;
    public Transform spawn_flecha;

    public GameObject flecha;

    public GameObject flechaPrefab;

    public float fuerza_flecha;

    public float potencia_flecha;

    private bool cargando;

    private float tiempo_de_disparo;



    [SerializeField] private Transform ola;

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (Time.time > cooldawn_disparo)
            {
                GameObject newflecha;

                newflecha = Instantiate(flecha, spawn_flecha.position, spawn_flecha.rotation);

                newflecha.GetComponent<Rigidbody>().AddForce(spawn_flecha.forward * fuerza_flecha_maxima);

                cooldawn_disparo = Time.time + rango_disparo;

                Destroy(newflecha, 3);

            }
        }*/
        if (Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            flecha = Instantiate(flecha, spawn_flecha.position, spawn_flecha.rotation);
            flecha.transform.SetParent(spawn_flecha);
            cargando = false;
        }

        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            
            potencia_flecha += 0.05f + fuerza_flecha * Time.deltaTime;
            potencia_flecha = Mathf.Clamp(potencia_flecha, 0, 30);
            flecha.GetComponent<Flecha>().potenciaFinal = potencia_flecha;
            cargando = true;
        }

        if (Touchscreen.current.primaryTouch.press.wasReleasedThisFrame && cargando == true)
        {
            flecha.GetComponent<Rigidbody>().linearVelocity = transform.forward * potencia_flecha;
            flecha.GetComponent<Rigidbody>().isKinematic = false;
            flecha.GetComponent<Flecha>().flechaEnVuelo = true;
            flecha.transform.SetParent(null);
            potencia_flecha = 0;
            cargando = false;
        }


        
    }



}

