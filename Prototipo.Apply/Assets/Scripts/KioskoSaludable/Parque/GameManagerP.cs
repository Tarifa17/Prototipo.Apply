using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerP : MonoBehaviour
{
    public static GameManagerP Instancia { get; private set; }

    private List<ContenedoresP> contenedoresCompletos = new List<ContenedoresP>();

    private int estrellasMinijuegos = 0;
    [SerializeField] private int totalTareas = 3; //2 contenedores + 1 minijuego
    private bool juegoGanado = false;

    [SerializeField] private UIManagerP uiManager;

    public bool JuegoGanado => juegoGanado;

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

    private void Start()
    {
        uiManager = FindObjectOfType<UIManagerP>();

        if (uiManager == null)
        {
            Debug.LogWarning("UIManagerP no encontrado. Verifica que el Canvas esté activo.");
            return;
        }

        // Ya no usamos EstadoKiosko.minijuegoCompletado, porque ahora se queda en la misma escena.
        ActualizarEstrellasTotales();
    }

    public void RegistrarTarea(ContenedoresP contenedorp)
    {
        if (juegoGanado || contenedorp == null || contenedoresCompletos.Contains(contenedorp)) return;

        if (contenedorp != null && !contenedoresCompletos.Contains(contenedorp))
        {
            contenedoresCompletos.Add(contenedorp);
            EstadoKiosko.tareasPrevias = contenedoresCompletos.Count;
            ActualizarEstrellasTotales();
        }
    }

    public void RemoverTarea(ContenedoresP contenedorp)
    {
        if (contenedoresCompletos.Contains(contenedorp))
        {
            contenedoresCompletos.Remove(contenedorp);
            EstadoKiosko.tareasPrevias = contenedoresCompletos.Count;
            ActualizarEstrellasTotales();
        }
    }
    public void SumarEstrellaMinijuego()
    {
        if (estrellasMinijuegos == 0)
        {
            estrellasMinijuegos = 1;
            ActualizarEstrellasTotales();
        }
    }
    private void ActualizarEstrellasTotales()
    {
        int total = contenedoresCompletos.Count + estrellasMinijuegos;
        if (uiManager != null)
            uiManager.ActualizarEstrellas(total);
        else
            Debug.LogWarning("uiManager es null al intentar actualizar estrellas.");

        if (total >= totalTareas && !juegoGanado)
        {
            juegoGanado = true;
            Time.timeScale = 0f;

            if (uiManager != null)
                uiManager.MostrarPantallaFinal();
        }
    }


}
