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
        bool inicioPresion = false;
        bool manteniendoPresion = false;
        bool soltoPresion = false;

        // 1. Detectar clic izquierdo del Mouse (para probar en el editor de PC)
        if (Mouse.current != null)
        {
            inicioPresion = Mouse.current.leftButton.wasPressedThisFrame;
            manteniendoPresion = Mouse.current.leftButton.isPressed;
            soltoPresion = Mouse.current.leftButton.wasReleasedThisFrame;
        }

        // 2. Detectar toque en la pantalla (para jugar en el celular)
        if (Touchscreen.current != null)
        {
            inicioPresion = inicioPresion || Touchscreen.current.primaryTouch.press.wasPressedThisFrame;
            manteniendoPresion = manteniendoPresion || Touchscreen.current.primaryTouch.press.isPressed;
            soltoPresion = soltoPresion || Touchscreen.current.primaryTouch.press.wasReleasedThisFrame;
        }

        // 3. Lógica de disparo
        if (inicioPresion)
        {
            flecha = Instantiate(
                flechaPrefab,
                spawn_flecha.position,
                spawn_flecha.rotation
            );

            flecha.transform.SetParent(spawn_flecha);
            potencia_flecha = 0f;
            cargando = true;
        }

        if (manteniendoPresion && cargando)
        {
            potencia_flecha += (20f / 5f) * Time.deltaTime;
            potencia_flecha = Mathf.Clamp(potencia_flecha, 0f, 20f);

            flecha.GetComponent<Flecha>().potenciaFinal = potencia_flecha;
        }

        if (soltoPresion && cargando == true)
        {
            flecha.transform.SetParent(null);
            flecha.GetComponent<Rigidbody>().isKinematic = false;
            flecha.GetComponent<Rigidbody>().useGravity = true;
            
            flecha.GetComponent<Rigidbody>().linearVelocity = transform.forward * potencia_flecha;
            flecha.GetComponent<Flecha>().flechaEnVuelo = true;
            
            potencia_flecha = 0;
            cargando = false;
        }
    }



}

