using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class IngresoNombreJugador : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputNombreJugador;

    public void ConfirmarNombre()
    {
        string nombre = inputNombreJugador.text;

        if (!string.IsNullOrWhiteSpace(nombre))
        {
            NombreIngresoManager.Instancia.EstablecerNombre(nombre);
            SceneManager.LoadScene("Casa"); 
        }
    }
}
