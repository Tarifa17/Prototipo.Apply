using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instancia;

    [SerializeField] private Image[] estrellasHUD; // Estrellas del HUD
    [SerializeField] private GameObject pantallaFinal; // Pantalla final
    [SerializeField] private Image[] estrellasGrandes; // Estrellas grandes en la pantalla final
    [SerializeField] private Button botonMenuPrincipal; // Botón para ir al menú principal
    [SerializeField] private Button botonVolverJuego; // Botón para reiniciar el nivel actual
    [SerializeField] private Button botonSiguienteEscena; // Botón para pasar a la siguiente escena

    private void Awake()
    {
        if (Instancia == null)
        {
            Instancia = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ActualizarEstrellas(int cantidad)
    {
        for (int i = 0; i < estrellasHUD.Length; i++)
        {
            bool isEnabled = i < cantidad;

            // Solo reproducir sonido si una nueva estrella es habilitada
            if (isEnabled && !estrellasHUD[i].enabled)
            {
                AudioManager.Instancia.PlayStarEarnedSound();
            }

            estrellasHUD[i].enabled = isEnabled;
        }

        for (int i = 0; i < estrellasGrandes.Length; i++)
        {
            estrellasGrandes[i].enabled = i < cantidad;
        }
    }

    public void MostrarPantallaFinal()
    {
        // Ocultar las estrellas del HUD
        foreach (var estrella in estrellasHUD)
        {
            estrella.enabled = false;
        }

        // Mostrar la pantalla final
        pantallaFinal.SetActive(true);
    }

    // Botón: Regresar al Menú Principal
    public void IrAlMenuPrincipal()
    {
        Time.timeScale = 1f; // Restablecer el tiempo normal

        SceneManager.LoadScene("MainMenu"); // Cambia a la escena del menú principal
    }

    // Botón: Volver al Juego
    public void ReiniciarNivel()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reinicia la escena actual
    }

    public void IrALaSiguienteEscena()
    {
        Time.timeScale = 1f; 
        int siguienteIndice = SceneManager.GetActiveScene().buildIndex + 1;

        if (siguienteIndice < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(siguienteIndice); // Cambiar a la siguiente escena
        }
        else
        {
            Debug.LogWarning("No hay más escenas disponibles.");
        }
    }
}