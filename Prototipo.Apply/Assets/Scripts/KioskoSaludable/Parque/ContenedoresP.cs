using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContenedoresP : MonoBehaviour
{
    [SerializeField] private TipoTarea tipo; // Tipo de tarea aceptado por el contenedor
    private GameObject objetoActual = null; // Objeto actualmente dentro del contenedor
    private GameManagerP gameManager; // Referencia al GameManager

    private void Start()
    {
        gameManager = FindObjectOfType<GameManagerP>(); // Buscar el GameManager en la escena
    }

    private void OnTriggerEnter2D(Collider2D other) // Detectar colisión de objetos
    {
        ObjetosEnum objeto = other.GetComponent<ObjetosEnum>();

        if (objeto != null)
        {
            if (objeto.Tipo == tipo && objetoActual == null) // Objeto correcto
            {
                objetoActual = other.gameObject;
                gameManager.RegistrarTarea(this);
            }
            else if (objeto.Tipo != tipo) // Objeto incorrecto
            {
                // Reproducir sonido de error
                AudioManager.Instancia.PlayErrorSound();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) // Detectar salida de objetos
    {
        if (other.gameObject == objetoActual)
        {
            objetoActual = null;
            gameManager.RemoverTarea(this);
        }
    }
}
