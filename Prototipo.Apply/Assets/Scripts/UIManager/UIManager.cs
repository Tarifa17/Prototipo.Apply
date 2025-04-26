using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instancia;

    [SerializeField] private Image[] estrellas; //Lista que contiene las estrellas
    [SerializeField] private GameObject pantallaFinal; //Variable para mostrar la pantalla al ganar

    private void Awake() //Creamos unas sola instancia
    {
        if (Instancia == null)
            Instancia = this;
        else
            Destroy(gameObject);
    }

    public void ActualizarEstrellas(int cantidad)
    {
        // Verifica que las referencias no se hayan destruido
        if (estrellas == null || estrellas.Length == 0)
        {
            Debug.LogWarning("No hay estrellas asignadas o se han destruido.");
            return;
        }

        for (int i = 0; i < estrellas.Length; i++)
        {
            if (estrellas[i] == null)
            {
                Debug.LogWarning($"Estrella en posición {i} se ha destruido.");
                continue;
            }

            estrellas[i].enabled = i < cantidad;
        }
    }

    public void MostrarPantallaFinal()
    {
        pantallaFinal.SetActive(true);
    }
}
