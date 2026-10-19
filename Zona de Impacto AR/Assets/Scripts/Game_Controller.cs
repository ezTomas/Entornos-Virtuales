using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Game_Controller : MonoBehaviour
{
    [SerializeField] private ARPlaneManager planeManager;
    [SerializeField] private GameObject Blanco_Prefab;

    [Range(0f, 0.45f)]
    [SerializeField] private float margenProporcional = 0.20f;

    [SerializeField] private GameObject BotonComenzar;

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

        // Comprobar que haya planos detectados antes de comenzar.
        if (planeManager.trackables.count == 0)
        {
            Debug.LogWarning(
                "Todavía no hay paredes detectadas. Escaneá la habitación."
            );
            return;
        }

        // Ocultar el botón.
        if (BotonComenzar != null)
        {
            BotonComenzar.SetActive(false);
        }

        juegoIniciado = true;

        // Ocultar los rectángulos sin eliminar los planos.
        OcultarPlanosDetectados();

        // Detener la detección de nuevos planos.
        planeManager.enabled = false;

        Debug.Log("Detección de planos detenida.");

        // Generar el primer blanco utilizando los planos detectados.
        SpawnearBlanco();
    }
    void OcultarPlanosDetectados()
    {
        foreach (ARPlane plano in planeManager.trackables)
        {
            // Ocultar la malla visual del plano.
            ARPlaneMeshVisualizer visualizador =
                plano.GetComponent<ARPlaneMeshVisualizer>();

            if (visualizador != null)
            {
                visualizador.enabled = false;
            }

            // Ocultar también los renderizadores asociados al plano.
            Renderer[] renderers =
                plano.GetComponentsInChildren<Renderer>(true);

            foreach (Renderer r in renderers)
            {
                r.enabled = false;
            }
        }

        Debug.Log("Visualización de planos ocultada.");
    }

    private void SpawnearBlanco()
    {
        if (blancoActual != null)
            return;

        if (planeManager == null || Blanco_Prefab == null)
        {
            Debug.LogError(
                "No se puede generar el blanco: faltan referencias."
            );
            juegoIniciado = false;
            return;
        }

        // Recopilar los planos detectados.
        List<ARPlane> planos = new List<ARPlane>();

        foreach (ARPlane plano in planeManager.trackables)
        {
            planos.Add(plano);
        }

        if (planos.Count == 0)
        {
            Debug.LogWarning(
                "Todavía no hay planos detectados. Escaneá las paredes."
            );
            juegoIniciado = false;
            return;
        }

        // Elegir un plano aleatorio.
        int indice = Random.Range(0, planos.Count);
        ARPlane planoElegido = planos[indice];

        // Obtener las dimensiones del plano.
        Vector2 tamano = planoElegido.size;

        // Aplicar márgenes proporcionales a ambos ejes.
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

        // El centro del plano ya está en coordenadas de sesión.
        // Sumamos desplazamientos siguiendo sus ejes locales,
        // expresados como direcciones en el mundo.
        Vector3 posicionMundo = planoElegido.center
            + planoElegido.transform.right * desplazamientoX
            + planoElegido.transform.forward * desplazamientoY;

        // Mantener la orientación que ya funcionaba.
        Vector3 normal = planoElegido.normal;
        Quaternion rotacion = Quaternion.LookRotation(normal);

        // Instanciar un único blanco.
        blancoActual = Instantiate(
            Blanco_Prefab,
            posicionMundo,
            rotacion
        );

        Debug.Log(
            "Blanco generado en el plano: "
            + planoElegido.trackableId
        );
    }

    // Preparado para conectarlo con la mecánica de disparo.
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