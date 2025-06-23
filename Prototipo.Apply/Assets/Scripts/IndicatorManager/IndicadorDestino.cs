using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class FlechaDestinoManager : MonoBehaviour
{
    [SerializeField] private GameObject flechaPrefab;
    private GameObject flechaActual;

    private Dictionary<TipoTarea, Transform> contenedoresPorTipo = new Dictionary<TipoTarea, Transform>();

    public static FlechaDestinoManager Instancia { get; private set; }

    private void Awake()
    {   
        if (Instancia != null && Instancia != this)
        {
            Destroy(gameObject);
            return;
        }
        Instancia = this;

        //Buscamos los contenedores y los mapeamos
        Contenedores[] todos = FindObjectsOfType<Contenedores>();
        foreach (Contenedores cont in todos)
        {
            TipoTarea tipo = cont.Tipo;
            if (!contenedoresPorTipo.ContainsKey(tipo))
                contenedoresPorTipo.Add(tipo, cont.transform);
        }
        ContenedoresP[] contenedoresP = FindObjectsOfType<ContenedoresP>();
        foreach (var contP in contenedoresP)
        {
            contenedoresPorTipo[contP.Tipo] = contP.transform;
        }

        Debug.Log($"FlechaDestinoManager: Cargados {contenedoresPorTipo.Count} contenedores.");
    }

    public void MostrarFlechaDestino(TipoTarea tipo)
    {
        OcultarFlechaDestino(); 

        if (contenedoresPorTipo.TryGetValue(tipo, out Transform contenedor))
        {
            flechaActual = Instantiate(flechaPrefab, contenedor.position + Vector3.left * 1.2f, Quaternion.identity);
            flechaActual.transform.SetParent(contenedor); 
        }
    }

    public void OcultarFlechaDestino()
    {
        if (flechaActual != null)
        {
            Destroy(flechaActual);
            flechaActual = null;
        }
    }
}
