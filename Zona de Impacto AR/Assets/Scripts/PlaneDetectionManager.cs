using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaneDetectionManager : MonoBehaviour
{
    public ARPlaneManager planeManager;

    private void OnEnable()
    {
        planeManager.trackablesChanged.AddListener(OnPlanesChanged);
    }

    private void OnDisable()
    {
        planeManager.trackablesChanged.RemoveListener(OnPlanesChanged);
    }

    private void OnPlanesChanged(ARTrackablesChangedEventArgs<ARPlane> args)
    {
        foreach (ARPlane plane in args.added)
        {
            Debug.Log("Nuevo plano detectado: " + plane.alignment);
        }
    }
}
