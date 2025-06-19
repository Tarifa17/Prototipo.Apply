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

    [SerializeField] private Transform contenedorLetras; 
    [SerializeField] private List<GameObject> prefabsLetras; 


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
        InstanciarLetrasIniciales();
    }

    private void FinalizarMinijuego()
    {
        panelMinijuego.SetActive(false);
        lapiceraSeleccionada = false;
    }

    private void ValidarPalabra()
    {
        string palabraCorrecta = "LAPICERA";
        string palabraFormada = "";
        int consonanteIndex = 0;

        char[] plantilla = { 'L', 'A', 'P', 'I', 'C', 'E', 'R', 'A' };

        for (int i = 0; i < plantilla.Length; i++)
        {
            if (EsConsonante(plantilla[i]))
            {
                if (consonanteIndex >= espaciosVacios.Count)
                {
                    Debug.LogError("❌ Faltan espacios vacíos para validar todas las consonantes.");
                    return;
                }

                Dropeable espacio = espaciosVacios[consonanteIndex];

                if (espacio.transform.childCount > 0)
                {
                    palabraFormada += espacio.transform.GetChild(0).name[0];
                }
                else
                {
                    Debug.Log("❌ No todas las letras fueron colocadas");

                    if (MinijuegoAudio.Instancia != null)
                        MinijuegoAudio.Instancia.SonidoError();

                    return;
                }

                consonanteIndex++;
            }
            else
            {
                // Agregar vocal directamente (ya está puesta visualmente en UI)
                palabraFormada += plantilla[i];
            }
        }

        if (palabraFormada == palabraCorrecta)
        {
            Debug.Log("✅ ¡Palabra correcta!");

            if (MinijuegoAudio.Instancia != null)
                MinijuegoAudio.Instancia.SonidoPalabraCorrecta();

            panelLapiceras.SetActive(true);
        }
        else
        {
            Debug.Log("❌ Palabra incorrecta");

            if (MinijuegoAudio.Instancia != null)
                MinijuegoAudio.Instancia.SonidoPalabraIncorrecta();

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
        InstanciarLetrasIniciales();
    }

    private IEnumerator CerrarMinijuego()
    {
        yield return new WaitForSeconds(tiempoEspera);

        EstadoMinijuego.minijuegoLapiceraCompletado = true;

        GameManagerP instancia = FindObjectOfType<GameManagerP>();

        if (instancia != null)
        {
            instancia.SumarEstrellaMinijuego();
            Debug.Log("Estrella sumada correctamente desde el kiosko.");

            // Reproducir sonido de estrella
            if (MinijuegoAudio.Instancia != null)
            {
                MinijuegoAudio.Instancia.SonidoEstrella();
            }
        }
        else
        {
            Debug.LogError("No se encontró GameManagerP en la escena. No se pudo sumar estrella.");
        }

        // Cerrar panel de minijuego
        panelMinijuego.SetActive(false);

        // Resetear flag
        lapiceraSeleccionada = false;

        Debug.Log("Panel del minijuego cerrado correctamente.");
    }

    private void InstanciarLetrasIniciales()
    {
        foreach (Transform hijo in contenedorLetras)
        {
            Destroy(hijo.gameObject); //Si ya hay letras
        }

        foreach (GameObject prefabLetra in prefabsLetras)
        {
            GameObject letra = Instantiate(prefabLetra, contenedorLetras);
            letra.name = prefabLetra.name; //Aseguramos que mantenga su nombre (por la validación con .name[0])
        }
        Debug.Log("Letras instanciadas para nuevo intento.");
    }
    private bool EsConsonante(char letra)
    {
        letra = char.ToUpper(letra);
        return !"AEIOU".Contains(letra);
    }

}