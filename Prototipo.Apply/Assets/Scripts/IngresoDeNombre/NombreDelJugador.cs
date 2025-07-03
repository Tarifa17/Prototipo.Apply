using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NombreDelJugador : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nombreJugadorUI;

    private void Start()
    {
        if (NombreIngresoManager.Instancia != null)
        {
            nombreJugadorUI.text = NombreIngresoManager.Instancia.NombreJugador;
        }
        else
        {
            nombreJugadorUI.text = "Jugador"; 
        }
    }
}
