
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameOver : MonoBehaviour
{
    public void ReiniciarPartida()
    {
        SceneManager.LoadScene("Deteccion_planos");
    }

    public void SalirDelJuego()
    {
        Application.Quit();

        // En el Editor, Quit no cierra Play Mode.
        Debug.Log("Solicitud de salida del juego.");
    }
}
