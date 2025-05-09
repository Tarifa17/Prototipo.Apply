using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MinijuegoManager : MonoBehaviour
{
    [SerializeField] private List<Dropeable> espaciosVacios; //Referencias a los espacios vacíos
    [SerializeField] private Button botonValidar;
    [SerializeField] private GameObject panelMinijuego;
    [SerializeField] private GameObject panelLapiceras;
    [SerializeField] private GameObject panelCorrecto;
    [SerializeField] private GameObject panelIncorrecto;
    [SerializeField] private Button botonReintentar;

    [Header("Configuración")]
    [SerializeField] private float tiempoEspera = 5f;

    private string palabraCorrecta = "LAPICERA";
    private bool lapiceraSeleccionada = false;

    private void Start()
    {
        botonValidar.onClick.AddListener(ValidarPalabra);
        if (botonReintentar != null)
            botonReintentar.onClick.AddListener(Reintentar);

        // Agregar sonido de click a los botones
        AddButtonClickSound(botonValidar);
        AddButtonClickSound(botonReintentar);

        // Desactivar paneles al iniciar
        panelMinijuego.SetActive(false);
        panelLapiceras.SetActive(false);
        panelCorrecto.SetActive(false);
        panelIncorrecto.SetActive(false);
    }

    // Método para agregar sonido de click a cualquier botón
    private void AddButtonClickSound(Button boton)
    {
        if (boton != null)
        {
            boton.onClick.AddListener(() => {
                if (MinijuegoAudio.Instancia != null)
                {
                    MinijuegoAudio.Instancia.SonidoBoton();
                }
            });
        }
    }

    public void ActivarMinijuego()
    {
        panelMinijuego.SetActive(true);
    }

    private void FinalizarMinijuego()
    {
        panelMinijuego.SetActive(false);
        lapiceraSeleccionada = false;
    }

    private void ValidarPalabra()
    {
        string palabraFormada = "";

        foreach (Dropeable espacio in espaciosVacios) //Recorremos la lista de espacios vacios
        {
            if (espacio.transform.childCount > 0)
            {
                palabraFormada += espacio.transform.GetChild(0).name[0]; //Como nuestros prefabs se llaman "L, A , P..." le asignamos estos nombres a cada variable
            }
            else
            {
                Debug.Log("No todas las letras fueron colocadas");

                // Reproducir sonido de error
                if (MinijuegoAudio.Instancia != null)
                {
                    MinijuegoAudio.Instancia.SonidoError();
                }
                return;
            }
        }

        if (palabraFormada == palabraCorrecta)
        {
            Debug.Log("¡Palabra correcta!");

            // Reproducir sonido de éxito
            if (MinijuegoAudio.Instancia != null)
            {
                MinijuegoAudio.Instancia.SonidoPalabraCorrecta();
            }

            panelLapiceras.SetActive(true);
        }
        else
        {
            Debug.Log("Palabra incorrecta");

            // Reproducir sonido de error
            if (MinijuegoAudio.Instancia != null)
            {
                MinijuegoAudio.Instancia.SonidoPalabraIncorrecta();
            }

            panelIncorrecto.SetActive(true);
        }
    }

    public void SeleccionarLapicera(string color)
    {
        if (lapiceraSeleccionada) return;
        lapiceraSeleccionada = true;

        if (color == "Azul")
        {
            Debug.Log("¡Correcto! Elegiste la lapicera azul.");

            // Reproducir sonido de éxito
            if (MinijuegoAudio.Instancia != null)
            {
                MinijuegoAudio.Instancia.SonidoLapiceraCorrecta();
            }

            panelCorrecto.SetActive(true);

            StartCoroutine(CerrarMinijuego());
        }
        else
        {
            Debug.Log("Lapicera incorrecta. El NPC quería la azul.");

            // Reproducir sonido de error
            if (MinijuegoAudio.Instancia != null)
            {
                MinijuegoAudio.Instancia.SonidoLapiceraIncorrecta();
            }

            panelIncorrecto.SetActive(true);
        }
    }

    private void Reintentar()
    {
        lapiceraSeleccionada = false;
        panelIncorrecto.SetActive(false);
        panelLapiceras.SetActive(false);

        foreach (Dropeable espacio in espaciosVacios)
        {
            if (espacio.transform.childCount > 0)
                Destroy(espacio.transform.GetChild(0).gameObject);
        }
    }

    private IEnumerator CerrarMinijuego()
    {
        yield return new WaitForSeconds(tiempoEspera);

        EstadoMinijuego.minijuegoLapiceraCompletado = true;

        GameManagerP instancia = FindObjectOfType<GameManagerP>();

        if (instancia != null)
        {
            instancia.SumarEstrellaMinijuego();
            Debug.Log("⭐ Estrella sumada correctamente desde el kiosko.");

            // Reproducir sonido de estrella
            if (MinijuegoAudio.Instancia != null)
            {
                MinijuegoAudio.Instancia.SonidoEstrella();
            }
        }
        else
        {
            Debug.LogError("❌ No se encontró GameManagerP en la escena. No se pudo sumar estrella.");
        }

        // Cerrar panel de minijuego
        panelMinijuego.SetActive(false);

        // Resetear flag
        lapiceraSeleccionada = false;

        Debug.Log("🧩 Panel del minijuego cerrado correctamente.");
    }
}