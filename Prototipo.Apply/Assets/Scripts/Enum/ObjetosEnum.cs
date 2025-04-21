using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TipoTarea
{
    Mochila,
    RopaSucia,
    Almohada,
    Matematica,
    Pregunta
}
public class ObjetosEnum : MonoBehaviour
{
    // El tipo de tarea realizable
    [SerializeField] private TipoTarea tipo;

    // Propiedad pública de solo lectura para acceder al tipo
    public TipoTarea Tipo => tipo;
}
