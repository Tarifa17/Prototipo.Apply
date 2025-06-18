using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TiendaIteract : MonoBehaviour
{
    [SerializeField] private AudioClip interactSound;
    [SerializeField] private GameObject canvasKiosko;
    private AudioSource audioSource;
    private bool jugadorEnRango = false;

    private void Start()
    {
        // Crear una fuente de audio si no existe
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        if (canvasKiosko != null)
        {
            canvasKiosko.SetActive(false);
        }
    }

    private void Update()
    {
        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E))
        {
            if (EstadoMinijuego.minijuegoKioskoSaludableCompletado)
            {
                Debug.Log("El minijuego del kiosko saludable ya fue completado.");
                return;
            }
            // Reproducir sonido de interacci�n
            if (interactSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(interactSound);
            }
            if (canvasKiosko != null)
            {
                canvasKiosko.SetActive(true);
            }
            else
            {
                Debug.LogWarning("Panel del kiosko no asignado en el Inspector.");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnRango = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnRango = false;
        }
    }
}