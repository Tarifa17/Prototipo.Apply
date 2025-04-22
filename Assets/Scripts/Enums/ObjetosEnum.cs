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
    // Tipo de este objeto (editable desde el Inspector)
    [SerializeField] private TipoTarea tipo;

    // Propiedad pública de solo lectura para acceder al tipo
    public TipoTarea Tipo => tipo;
}





