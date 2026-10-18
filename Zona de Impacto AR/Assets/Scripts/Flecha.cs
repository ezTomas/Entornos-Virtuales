using System.Collections;
using UnityEngine;

public class Flecha : MonoBehaviour
{
    public GameObject sumaFlecha;
    public bool flechaEnVuelo;
    Quaternion rotar;
    public float potenciaFinal;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sumaFlecha = GameObject.FindGameObjectWithTag("arco");
        flechaEnVuelo = false;
        rotar = Quaternion.Euler(0, 0, -100);
    }

    // Update is called once per frame
    void Update()
    {
        if (flechaEnVuelo == true && potenciaFinal <= 25)
        {
            transform.rotation = (Quaternion.Lerp(transform.rotation, rotar, 2f * Time.deltaTime));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        flechaEnVuelo = false;
        StartCoroutine(tiempoDestruccion());
    }

    IEnumerator tiempoDestruccion()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
 