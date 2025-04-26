using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instancia { get; private set; }

    private List<Contenedores> contenedoresCompletos = new List<Contenedores>(); // Lista para llevar el control de qué contenedores ya recibieron su objeto correcto

    [SerializeField] private int totalTareas = 3; //Total de tareas (puede cambiar a futuro)
    private bool juegoGanado = false; //Variable en falso para manejar si el juego es ganado

    [SerializeField] private UIManager uiManager; //Variable para el uimanager

    public bool JuegoGanado => juegoGanado;

    private void Awake() //Inicializamos solo un manager
    {
        if (Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instancia != this)
        {
            Destroy(gameObject); 
        }
    }

    public void RegistrarTarea(Contenedores contenedor)
    {
        if (juegoGanado) return; //Si el juego esta ganado detiene la logica


        if (!contenedoresCompletos.Contains(contenedor)) // Si este contenedor no estaba registrado aún, lo agregamos
        {
            contenedoresCompletos.Add(contenedor);
            Debug.Log($"Tarea registrada. Total completadas: {contenedoresCompletos.Count}");
            uiManager.ActualizarEstrellas(contenedoresCompletos.Count); // Actualizamos la UI con la cantidad actual de tareas completadas

            if (uiManager != null) //Actualizamos el UI 
                uiManager.ActualizarEstrellas(contenedoresCompletos.Count);


            if (contenedoresCompletos.Count >= totalTareas) //Si los objetos en los contenedores son iguales a las tareas
            {
                juegoGanado = true;
                StartCoroutine(Ganar(2f)); // Espera 2 segundos antes de mostrar victoria


            }
        }
    }

    public void RemoverTarea(Contenedores contenedor)
    {
        if (contenedoresCompletos.Contains(contenedor)) //Si los contenedores estan registrados
        {
            contenedoresCompletos.Remove(contenedor); //Remueve la tarea completa de un contenedor
            Debug.Log($"Tarea removida. Total ahora: {contenedoresCompletos.Count}");

            if (uiManager != null)
                uiManager.ActualizarEstrellas(contenedoresCompletos.Count);
            else
            {
                Debug.LogWarning("UIManager no asignado al registrar/remover tarea.");
            }
        }
    }

    private IEnumerator Ganar(float espera)
    {
        yield return new WaitForSeconds(espera);

        juegoGanado = true;
        Time.timeScale = 0f;

        if (uiManager != null)
            uiManager.MostrarPantallaFinal();
    }

    // Logica para recuperar el UI y no perder la informacion
    //Nos suscribimos al evento de cambio de escenas del objeto, y al desactivarse nos desuscribimos para evitar errores
    private void OnEnable() //Llama cuando un objeto se activa
    {
        SceneManager.sceneLoaded += OnSceneLoaded; //Se ejecuta automaticamente al cargar una nueva escena y llama a la funcion OnSceneLoaded
    }

    private void OnDisable() //Llama cuando un objeto se desactiva
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; //Se ejecuta automaticamente al cargar una nueva escena y llama a la funcion OnSceneLoaded
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) //Guardamos la escena recien cargado y el modo en que se cargo
    {
        // Buscar el nuevo UIManager cuando se cargue una nueva escena
        if (uiManager == null)
        {
            uiManager = FindObjectOfType<UIManager>(); //buscamos el uimanager

            if (uiManager != null)
                uiManager.ActualizarEstrellas(contenedoresCompletos.Count);
        }
    }

}
