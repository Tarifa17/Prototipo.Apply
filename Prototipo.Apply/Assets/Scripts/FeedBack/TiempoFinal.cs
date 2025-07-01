using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TiempoFinal : MonoBehaviour
{
    [SerializeField] private TMP_Text textoTiempo;

    public void MostrarTiempo()
    {
        if (Timer.Instancia != null)
        {
            float tiempo = Timer.Instancia.TiempoTotal;
            int minutos = Mathf.FloorToInt(tiempo / 60f);
            int segundos = Mathf.FloorToInt(tiempo % 60f);

            textoTiempo.text = $"Tiempo total: {minutos:00}:{segundos:00}";
        }
        else
        {
            textoTiempo.text = "Tiempo no disponible.";
        }
    }
}
