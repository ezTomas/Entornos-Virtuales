using UnityEngine;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    [Header("Referencias UI")]
    public TextMeshProUGUI textoPuntaje;
    public TextMeshProUGUI textoTiempo;
    public TextMeshProUGUI textoInstruccionEscaneo; // Nueva referencia

    [Header("Configuración")]
    public float tiempoRestante = 30f;
    private int puntajeActual = 0;
    private bool juegoActivo = false; 

    void Start()
    {
        // Al arrancar la escena, ocultamos puntaje/tiempo y mostramos el escaneo
        textoPuntaje.gameObject.SetActive(false);
        textoTiempo.gameObject.SetActive(false);
        textoInstruccionEscaneo.gameObject.SetActive(true);

        textoPuntaje.text = "Puntaje: " + puntajeActual.ToString();
        textoTiempo.text = "Tiempo restante: " + Mathf.CeilToInt(tiempoRestante).ToString() + "s";
    }

    void Update()
    {
        if (juegoActivo && tiempoRestante > 0)
        {
            tiempoRestante -= Time.deltaTime;
            ActualizarUI();

            if (tiempoRestante <= 0)
            {
                tiempoRestante = 0;
                juegoActivo = false;
                textoTiempo.text = "Tiempo restante: ¡AGOTADO!"; 
            }
        }
    }

    public void IniciarTemporizador()
    {
        juegoActivo = true;
        
        // Al comenzar, activamos puntaje/tiempo y ocultamos el texto de escaneo
        textoPuntaje.gameObject.SetActive(true);
        textoTiempo.gameObject.SetActive(true);
        textoInstruccionEscaneo.gameObject.SetActive(false);
    }

    public void RegistrarAcierto(int puntos, float tiempoExtra)
    {
        if (!juegoActivo) return;

        puntajeActual += puntos;
        tiempoRestante += tiempoExtra;
        ActualizarUI();
    }

    private void ActualizarUI()
    {
        textoPuntaje.text = "Puntaje: " + puntajeActual.ToString();
        textoTiempo.text = "Tiempo restante: " + Mathf.CeilToInt(tiempoRestante).ToString() + "s"; 
    }
}