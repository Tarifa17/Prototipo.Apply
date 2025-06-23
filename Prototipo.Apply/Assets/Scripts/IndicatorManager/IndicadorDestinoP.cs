using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicadorDestinoP : MonoBehaviour
{
    [SerializeField] private GameObject flechaPrefab; // Prefab de flecha que se va a instanciar
    private GameObject flechaActual;

    private Dictionary<TipoTarea, Transform> contenedoresPorTipo = new Dictionary<TipoTarea, Transform>();

    public static IndicadorDestinoP Instancia { get; private set; }

    private void Awake()
    {
        // Singleton básico
        if (Instancia != null && Instancia != this)
        {
            Destroy(gameObject);
            return;
        }
        Instancia = this;

        // Buscar todos los contenedores de la escena y mapearlos por tipo
        ContenedoresP[] todos = FindObjectsOfType<ContenedoresP>();
        foreach (ContenedoresP cont in todos)
        {
            TipoTarea tipo = cont.Tipo; 
            if (!contenedoresPorTipo.ContainsKey(tipo))
                contenedoresPorTipo.Add(tipo, cont.transform);
        }
    }

    /// <summary>
    /// Llama esto cuando el jugador agarra un objeto.
    /// </summary>
    public void MostrarFlechaDestino(TipoTarea tipo)
    {
        OcultarFlechaDestino(); // Eliminar flecha anterior si la hay

        if (contenedoresPorTipo.TryGetValue(tipo, out Transform contenedor))
        {
            flechaActual = Instantiate(flechaPrefab, contenedor.position + Vector3.left * 1.2f, Quaternion.identity); // Ajustar altura si hace falta
            flechaActual.transform.SetParent(contenedor); // Para que siga al contenedor si se mueve
        }
    }

    /// <summary>
    /// Llama esto al soltar el objeto.
    /// </summary>
    public void OcultarFlechaDestino()
    {
        if (flechaActual != null)
        {
            Destroy(flechaActual);
            flechaActual = null;
        }
    }
}
