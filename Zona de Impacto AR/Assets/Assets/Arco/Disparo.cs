using UnityEngine;
using UnityEngine.InputSystem;

public class Disparo : MonoBehaviour
{
    public Transform spawn_flecha;

    public GameObject flecha;

    public float fuerza_flecha = 1500f;
    public float fuerza_velocidad = 1.0f;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            GameObject newflecha;

            newflecha = Instantiate(flecha, spawn_flecha.position, spawn_flecha.rotation);


        }
    }
}
