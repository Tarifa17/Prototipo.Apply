using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> flechas; //Flechas a mostrar
    [SerializeField] private float duracionMostrar = 10f; // Variable para los segundos
    [SerializeField] private bool mostrarAlInicio = true; //Variable para marcar el inicio del juego

    private void Start()
    {
        if (mostrarAlInicio)
        {
            StartCoroutine(MostrarFlechasTemporales()); //Iniciamos la corrutina
        }
    }

    private IEnumerator MostrarFlechasTemporales() //Corrutina para mostrar las flechas
    {
        foreach (GameObject flecha in flechas) // Activamos todas las flechas
        {
            flecha.SetActive(true);
        }

        yield return new WaitForSeconds(duracionMostrar); //Esperamos los 5 segundos

        foreach (GameObject flecha in flechas)  //Desactivamos todas las flechas
        {
            flecha.SetActive(false);
        }
    }
}
