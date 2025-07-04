using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RegistroJugadores : MonoBehaviour
{
    private string ruta => Application.persistentDataPath + "/jugadores.json";
    private ListaJugadores datos = new ListaJugadores();

    private void Awake()
    {
        CargarDatos(); 
    }

    public void GuardarJugador(string nombre, float tiempoSegundos)
    {
        DatosJugador nuevo = new DatosJugador
        {
            nombre = nombre,
            tiempoTotal = FormatearTiempo(tiempoSegundos)
        };

        datos.jugadores.Add(nuevo);
        string json = JsonUtility.ToJson(datos, true);
        File.WriteAllText(ruta, json);
        Debug.Log($"✅ Jugador guardado: {nuevo.nombre} - {nuevo.tiempoTotal}");
        Debug.Log($"📁 Ruta del archivo: {ruta}");

    }

    private void CargarDatos()
    {
        if (File.Exists(ruta))
        {
            string contenido = File.ReadAllText(ruta);
            datos = JsonUtility.FromJson<ListaJugadores>(contenido);
        }
    }

    private string FormatearTiempo(float tiempo)
    {
        int minutos = Mathf.FloorToInt(tiempo / 60);
        int segundos = Mathf.FloorToInt(tiempo % 60);
        return $"{minutos:D2}:{segundos:D2}";
    }

    public ListaJugadores ObtenerDatosGuardados() => datos;
}
