using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instancia { get; private set; }

    private List<Contenedores> contenedoresCompletos = new List<Contenedores>(); // Lista para llevar el control de qué contenedores ya recibieron su objeto correcto

    [SerializeField] private int totalTareas = 3; //Total de tareas (puede cambiar a futuro)
    private bool juegoGanado = false; //Variable en falso para manejar si el juego es ganado

    [SerializeField] private UIManager uiManager; //Variable para el uimanager

    public bool JuegoGanado => juegoGanado; // Propiedad pública de solo lectura que permite saber si el juego terminó

    private void Awake() //Inicializamos solo un manager
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

    public void RegistrarTarea(Contenedores contenedor)
    {
        if (GameManager.Instancia.JuegoGanado) return; //Si el juego esta ganado detiene la logica


        if (!contenedoresCompletos.Contains(contenedor)) // Si este contenedor no estaba registrado aún, lo agregamos
        {
            contenedoresCompletos.Add(contenedor);
            uiManager.ActualizarEstrellas(contenedoresCompletos.Count); // Actualizamos la UI con la cantidad actual de tareas completadas

            if (contenedoresCompletos.Count >= totalTareas) //Si los objetos en los contenedores son iguales a las tareas
            {
                juegoGanado = true;
                Time.timeScale = 0f;
                uiManager.MostrarPantallaFinal();
            }
        }
    }

    public void RemoverTarea(Contenedores contenedor)
    {
        if (contenedoresCompletos.Contains(contenedor)) //Si los contenedores estan registrados
        {
            contenedoresCompletos.Remove(contenedor); //Remueve la tarea completa de un contenedor
            uiManager.ActualizarEstrellas(contenedoresCompletos.Count);
        }
    }
}
