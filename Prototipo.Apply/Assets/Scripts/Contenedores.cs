using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contenedores : MonoBehaviour
{
    [SerializeField] private TipoTarea tipo; //Variable para el tipo de tarea
    private GameObject objetoActual = null; //Marcamos el objeto actual como nulo
    private GameManager gameManager; //Variable para el GameManager

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); //Buscamos elgamemanager
    }

    private void OnTriggerEnter2D(Collider2D other) //Detectamos cuando un objeto entra en contacto con el contenedor 
    {
        ObjetosEnum objeto = other.GetComponent<ObjetosEnum>(); //
        if (objeto != null && objeto.Tipo == tipo && objetoActual == null) // Si es del tipo correcto y aún no hay otro objeto dentro
        {
            objetoActual = other.gameObject;
            gameManager.RegistrarTarea(this);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == objetoActual)
        {
            objetoActual = null;
            gameManager.RemoverTarea(this);
        }
    }
}
