using System.Collections;
using UnityEngine;

public class Flecha : MonoBehaviour
{
    public GameObject sumaFlecha;
    public bool flechaEnVuelo;
    Quaternion rotar;
    public float potenciaFinal;

    private bool impactoRegistrado = false;

    void Start()
    {
        sumaFlecha = GameObject.FindGameObjectWithTag("arco");
        flechaEnVuelo = false;
        rotar = Quaternion.Euler(0, 0, -100);
    }

    void Update()
    {
        if (flechaEnVuelo == true && potenciaFinal <= 25)
        {
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                rotar,
                2f * Time.deltaTime
            );
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        flechaEnVuelo = false;

        // Evitar registrar más de un impacto.
        if (impactoRegistrado)
            return;

        // Comprobar si chocó con el blanco.
        BlancoObjetivo blanco =
            collision.collider.GetComponentInParent<BlancoObjetivo>();

        if (blanco != null)
        {
            impactoRegistrado = true;

            Game_Controller controlador =
                FindFirstObjectByType<Game_Controller>();

            if (controlador != null)
            {
                controlador.EliminarBlancoActual();
            }

            // Destruir la flecha al impactar el blanco.
            Destroy(gameObject);
            return;
        }

        // Si chocó con otra cosa, mantener
        // el comportamiento original.
        StartCoroutine(tiempoDestruccion());
    }

    IEnumerator tiempoDestruccion()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}