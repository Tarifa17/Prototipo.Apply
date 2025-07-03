using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NombreIngresoManager : MonoBehaviour
{
    public static NombreIngresoManager Instancia { get; private set; }

    public string NombreJugador { get; private set; }

    private void Awake()
    {
        if (Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EstablecerNombre(string nombre)
    {
        NombreJugador = nombre;
    }
}
