using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public enum TipoTarea
{
    Mochila,
    RopaSucia,
    Almohada,
    Matematica,
    Pregunta,
    Basura,
    Flor
}

public class ObjetosEnum : MonoBehaviour
{
    // El tipo de tarea realizable
    [SerializeField] private TipoTarea tipo;

    // Propiedad pública de solo lectura para acceder al tipo
    public TipoTarea Tipo => tipo;
}
