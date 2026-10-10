using System.Collections;
using System.Collections.Generic;
using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.InputSystem;

public class Disparo : MonoBehaviour
{
    public Transform spawn_flecha;

    public GameObject flecha;

    public float fuerza_flecha_maxima = 1500f;

    public float fuerza_flecha_minima = 0f;

    private float tiempo_de_disparo;

    public float rango_disparo = 1.0f;
    private float cooldawn_disparo = 0;

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
        if (Touchscreen.current.primaryTouch.press.isPressed) { return; }
            if(tiempo_de_disparo <= fuerza_flecha_maxima)
        {
            tiempo_de_disparo += 20;
            tiempo_de_disparo += Time.deltaTime;
            print(tiempo_de_disparo);
        }
            if(Touchscreen.current.primaryTouch.press.wasReleasedThisFrame)
        {
            Disparar((int)tiempo_de_disparo);
            tiempo_de_disparo = 0;

        }


        
    }

    private void Disparar(int tiempodeCarga)
    {
        GameObject newflecha;

        Vector3 potencia = new Vector3(tiempodeCarga, tiempodeCarga, tiempodeCarga);
        newflecha = Instantiate(flecha, spawn_flecha.position, spawn_flecha.rotation);

        newflecha.GetComponent<Rigidbody>().AddForce(spawn_flecha.forward * fuerza_flecha_maxima);
        cooldawn_disparo = Time.time + rango_disparo;

        Destroy(newflecha, 3);
    }


    private void Start()
    {
        


    }


}

