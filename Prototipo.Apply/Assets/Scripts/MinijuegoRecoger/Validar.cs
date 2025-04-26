using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Validar : MonoBehaviour
{
    [SerializeField] private ZonaDrop[] zonas;
    [SerializeField] private GameObject panelLapiceras;
    [SerializeField] private string palabraCorrecta = "LAPICERA";

    public void ValidarPalabra()
    {
        string palabraFormada = "";

        foreach (ZonaDrop zona in zonas)
        {
            palabraFormada += zona.LetraActual;
        }

        if (palabraFormada == palabraCorrecta)
        {
            Debug.Log("¡Correcto!");
            panelLapiceras.SetActive(true);
        }
        else
        {
            Debug.Log("Incorrecto. Intenta de nuevo.");
            // Opcional: feedback visual de error
        }
    }
}
