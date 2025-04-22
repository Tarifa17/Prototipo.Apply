using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instancia { get; private set; }

    private List<Contenedor> contenedoresCompletos = new List<Contenedor>(); // Lista para llevar el control de qué contenedores ya recibieron su objeto correcto

    [SerializeField] private int totalTareas = 3;
    private bool juegoGanado = false;

    [SerializeField] private UIManager uiManager;

    public bool JuegoGanado => juegoGanado; // Propiedad pública de solo lectura que permite saber si el juego terminó

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

    public void RegistrarTarea(Contenedor contenedor)
    {
        if (GameManager.Instancia.JuegoGanado) return;


        if (!contenedoresCompletos.Contains(contenedor)) // Si este contenedor no estaba registrado aún, lo agregamos
        {
            contenedoresCompletos.Add(contenedor);
            uiManager.ActualizarEstrellas(contenedoresCompletos.Count); // Actualizamos la UI con la cantidad actual de tareas completadas

            if (contenedoresCompletos.Count >= totalTareas) 
            {
                juegoGanado = true;
                Time.timeScale = 0f;
                uiManager.MostrarPantallaFinal();
            }
        }
    }

    public void RemoverTarea(Contenedor contenedor)
    {
        if (contenedoresCompletos.Contains(contenedor))
        {
            contenedoresCompletos.Remove(contenedor);
            uiManager.ActualizarEstrellas(contenedoresCompletos.Count);
        }
    }
}
