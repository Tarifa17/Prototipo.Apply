using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorFinalizador : MonoBehaviour
{
    private void Start()
    {
        if (Timer.Instancia != null)
        {
            Timer.Instancia.DetenerTimer();
        }
        string nombreJugador = NombreIngresoManager.Instancia?.NombreJugador ?? "SinNombre";
        float tiempo = Timer.Instancia.TiempoTotal;

        RegistroJugadores registro = FindObjectOfType<RegistroJugadores>();
        if (registro != null)
        {
            registro.GuardarJugador(nombreJugador, tiempo);
        }
        else
        {
            Debug.LogWarning("No se encontró el RegistroJugadores en la escena final.");
        }
    }
}
