using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Game_Controller : MonoBehaviour
{
    [SerializeField] private ARPlaneManager planeManager;

    [SerializeField] private GameObject Blanco_Prefab;
    public void ComenzarJuego()
    {
        Debug.Log("El juego ha comenzado.");

        if (planeManager == null)
        {
            Debug.LogError("ARPlaneManager no está asignado.");
            return;
        }

        foreach (ARPlane plano in planeManager.trackables)
        {
            Debug.Log("Plano detectado: " + plano.trackableId);
        }
        SpawnearBlanco();
    }
    private void SpawnearBlanco()
    {
        foreach (ARPlane plano in planeManager.trackables)
        {
            Vector3 posicion = plano.center;
            Instantiate(Blanco_Prefab, posicion, plano.transform.rotation);

            Debug.Log("Blanco generado");
            return;
        }
    }
}
