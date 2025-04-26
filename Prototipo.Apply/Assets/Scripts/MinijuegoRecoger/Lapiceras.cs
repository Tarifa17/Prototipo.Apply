using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lapiceras : MonoBehaviour
{
    [SerializeField] private GameObject panelLapiceras; //Panel contenedor de las lapiceras
    [SerializeField] private GameObject mensajeCorrecto; //Lapicera correcta
    [SerializeField] private GameObject mensajeIncorrecto; //Lapicer Incorrecta
    [SerializeField] private Contenedores contenedorTarea; //Variable para el contenedor
    [SerializeField] private GameObject botonReintentar; //Botón para reintentar


    private bool tareaYaRegistrada = false;

    private void Start()
    {
        // Mostrar en consola si los objetos están bien asignados
        Debug.Log("mensajeCorrecto asignado: " + (mensajeCorrecto != null ? mensajeCorrecto.name : "NO asignado"));
        Debug.Log("mensajeIncorrecto asignado: " + (mensajeIncorrecto != null ? mensajeIncorrecto.name : "NO asignado"));
        Debug.Log("panelLapiceras asignado: " + (panelLapiceras != null ? panelLapiceras.name : "NO asignado"));
    }


    public void EntregarLapiceraAzul()
    {
        if (!tareaYaRegistrada) 
        {
            GameManager.Instancia.RegistrarTarea(contenedorTarea); //Llamamos a la instancia del GameManager para registrar la tarea y sumar la estrella
            tareaYaRegistrada = true;
            Invoke(nameof(VolverAEscuela), 2f);
            mensajeCorrecto.SetActive(false);
            mensajeIncorrecto.SetActive(false);
            panelLapiceras.SetActive(false);


        }

        mensajeCorrecto.SetActive(true); 
        panelLapiceras.SetActive(false); 
    }

    public void EntregarLapiceraRoja()
    {
        mensajeIncorrecto.SetActive(true);
        botonReintentar.SetActive(true);
        panelLapiceras.SetActive(false);  
    }

    private void VolverAEscuela()
    {
        SceneManager.LoadScene("Escuela");
    }

    public void Reintentar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Escuela");
    }

}


