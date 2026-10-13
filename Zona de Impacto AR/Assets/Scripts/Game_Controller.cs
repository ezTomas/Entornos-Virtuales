using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Game_Controller : MonoBehaviour
{
    [SerializeField] private ARPlaneManager planeManager;

    [SerializeField] private GameObject Blanco_Prefab;

    [Range(0f, 0.45f)]
    [SerializeField] private float margenProporcional = 0.20f;

    private GameObject blancoActual;
    private bool juegoIniciado = false;

    public void ComenzarJuego()
    {
        if (juegoIniciado)
            return;

        if (planeManager == null || Blanco_Prefab == null)
        {
            Debug.LogError(
                "Falta asignar el AR Plane Manager o el prefab del blanco."
            );
            return;
        }

        juegoIniciado = true;
        SpawnearBlanco();
    }
    private void SpawnearBlanco()
    {
        if (blancoActual != null)
            return;

        List<ARPlane> planos = new List<ARPlane>();

        foreach (ARPlane plano in planeManager.trackables)
        {
            planos.Add(plano);
        }

        if (planos.Count == 0)
        {
            Debug.LogWarning("Todavía no hay planos detectados.");
            juegoIniciado = false;
            return;
        }

        // Elegir una pared al azar.
        int indice = Random.Range(0, planos.Count);
        ARPlane planoElegido = planos[indice];

        // Calcular una posición aleatoria dentro del plano.
        Vector2 tamano = planoElegido.size;
        Vector3 centro = planoElegido.center;

        float margenX = tamano.x * margenProporcional;
        float margenY = tamano.y * margenProporcional;

        float desplazamientoX = Random.Range(
            -tamano.x / 2f + margenX,
             tamano.x / 2f - margenX
        );

        float desplazamientoY = Random.Range(
            -tamano.y / 2f + margenY,
             tamano.y / 2f - margenY
        );

        Vector3 posicionLocal = centro + new Vector3(
            desplazamientoX,
            0f,
            desplazamientoY
        );

        Vector3 posicionMundo =
            planoElegido.transform.TransformPoint(posicionLocal);

        // Conservar la orientación que ya funcionó en el celular.
        Vector3 normal = planoElegido.normal;
        Quaternion rotacion = Quaternion.LookRotation(normal);

        // Crear un único blanco.
        blancoActual = Instantiate(
            Blanco_Prefab,
            posicionMundo,
            rotacion
        );

        Debug.Log("Blanco generado correctamente.");
    }

    // Función preparada para conectar con la mecánica de disparo.
    public void EliminarBlancoActual()
    {
        if (blancoActual == null)
            return;

        Destroy(blancoActual);
        blancoActual = null;

        if (juegoIniciado)
            SpawnearBlanco();
    }
}
