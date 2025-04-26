using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgarrarObjeto : MonoBehaviour
{
    [SerializeField] private GameObject puntoAgarre; // Punto de agarre del objeto
    private GameObject objetoAgarrado = null; // Objeto actualmente agarrado
    private GameManager gameManager; // Referencia al GameManager

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (gameManager.JuegoGanado) return; // Si el juego fue ganado, detiene la l�gica

        if (objetoAgarrado != null)
        {
            if (Input.GetKey("q")) // Soltar el objeto al presionar "Q"
            {
                Rigidbody2D rb = objetoAgarrado.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.isKinematic = false; // Deja de ser kinem�tico
                }

                objetoAgarrado.transform.SetParent(null); // Quitar el objeto del punto de agarre
                objetoAgarrado = null;

                // Reproducir sonido de soltar
                AudioManager.Instancia.PlayDropSound();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (gameManager.JuegoGanado) return; // Si el juego fue ganado, detiene la l�gica

        if (other.gameObject.CompareTag("Objetos")) // Detecta objetos con el tag "Objetos"
        {
            if (Input.GetKey("e") && objetoAgarrado == null) // Agarrar el objeto al presionar "E"
            {
                Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.isKinematic = true; // Hacer el objeto kinem�tico
                }

                other.transform.position = puntoAgarre.transform.position; // Mover al punto de agarre
                other.transform.SetParent(puntoAgarre.transform); // Hacer hijo del punto de agarre
                objetoAgarrado = other.gameObject; // Guardar el objeto como agarrado

                // Reproducir sonido de agarrar
                AudioManager.Instancia.PlayGrabSound();
            }
        }
    }
}