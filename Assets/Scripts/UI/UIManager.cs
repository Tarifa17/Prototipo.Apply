using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Permite cambiar entre escenas.
using UnityEngine.UI; // Permite trabajar con elementos gráficos de la interfaz.

public class UIManager : MonoBehaviour
{
    // Instancia singleton para que este script sea accesible desde cualquier otro.
    public static UIManager Instancia;

    [SerializeField] private Image[] estrellas; // Estrellas mostradas durante el nivel.
    [SerializeField] private Image[] estrellasFinal; // Estrellas mostradas en la pantalla final.
    [SerializeField] private GameObject pantallaFinal; // Referencia al objeto de la pantalla final.

    private int estrellasGanadas = 0; // Número de estrellas ganadas por el jugador.

    private void Awake()
    {
        // Implementación del patrón Singleton: garantiza una sola instancia de este script.
        if (Instancia == null)
            Instancia = this; // Guardamos esta instancia como la principal.
        else
            Destroy(gameObject); // Si ya existe otra instancia, destruimos este objeto.
    }

    // Actualiza las estrellas ganadas y las muestra en pantalla.
    public void ActualizarEstrellas(int cantidad)
    {
        estrellasGanadas = cantidad; // Guardamos la cantidad de estrellas ganadas.

        // Activamos solo las estrellas correspondientes a la cantidad ganada.
        for (int i = 0; i < estrellas.Length; i++)
        {
            estrellas[i].enabled = i < cantidad; // Habilita la estrella si "i" es menor a la cantidad.
        }
    }

    // Muestra la pantalla final del nivel y actualiza las estrellas finales.
    public void MostrarPantallaFinal()
    {
        pantallaFinal.SetActive(true); // Activamos la pantalla final.

        // Ocultamos las estrellas del nivel actual.
        foreach (Image estrella in estrellas)
        {
            estrella.enabled = false;
        }

        // Mostramos las estrellas finales según la cantidad ganada.
        for (int i = 0; i < estrellasFinal.Length; i++)
        {
            estrellasFinal[i].enabled = i < estrellasGanadas;
        }
    }

    // 🔁 Botón 1: Reiniciar el nivel actual.
    public void ReiniciarJuego()
    {
        Time.timeScale = 1f; // Asegura que el tiempo del juego esté normal.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Recarga la escena actual.
    }

    // 🏠 Botón 2: Ir al menú principal (Intro).
    public void IrAlIntro()
    {
        Time.timeScale = 1f; // Asegura que el tiempo del juego esté normal.
        SceneManager.LoadScene("Intro"); // Carga la escena llamada "Intro".
        // Alternativamente, SceneManager.LoadScene(0); si el menú es el índice 0.
    }

    // ⏳ Botón 3: Ir a la pantalla de carga.
    public void IrAPantallaCarga()
    {
        Time.timeScale = 1f; // Asegura que el tiempo del juego esté normal.
        SceneManager.LoadScene("loadingScene"); // Carga la escena llamada "loadingScene".
        // Cambia "loadingScene" por el nombre de tu escena de carga.
    }
}