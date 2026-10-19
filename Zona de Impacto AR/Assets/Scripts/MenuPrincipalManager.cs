using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalManager : MonoBehaviour
{
    [Tooltip("Nombre exacto de la escena donde ocurre el juego AR")]
    public string nombreEscenaJuego = "Deteccion_planos";

    public void IniciarJuego()
    {
        // Carga la escena del juego de tiro al blanco
        SceneManager.LoadScene("Deteccion_planos");
    }

    public void SalirJuego()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
        
        // Esto permite probar que el botón funciona dentro del editor de Unity
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}