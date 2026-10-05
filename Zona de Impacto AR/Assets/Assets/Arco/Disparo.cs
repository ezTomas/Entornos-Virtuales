using UnityEngine;
using UnityEngine.InputSystem;

public class Disparo : MonoBehaviour
{
    public Transform spawn_flecha;

    public GameObject flecha;

    public float fuerza_flecha = 1500f;

    public float rango_disparo = 1.0f;
    private float cooldawn_disparo = 0;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if(Time.time > cooldawn_disparo)
            {
                GameObject newflecha;

                newflecha = Instantiate(flecha, spawn_flecha.position, spawn_flecha.rotation);

                newflecha.GetComponent<Rigidbody>().AddForce(spawn_flecha.forward * fuerza_flecha);

                cooldawn_disparo = Time.time + cooldawn_disparo;

                Destroy(newflecha,3);
            }



        }
    }
}
