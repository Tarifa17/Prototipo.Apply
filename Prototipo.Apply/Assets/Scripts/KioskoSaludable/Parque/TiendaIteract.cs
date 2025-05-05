using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TiendaIteract : MonoBehaviour
{
    [SerializeField] private AudioClip interactSound;
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
    }

    private void Update()
    {
        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E))
        {
            // Reproducir sonido de interacción
            if (interactSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(interactSound);
                // Esperar un poco para que se escuche el sonido antes de cambiar de escena
                StartCoroutine(CambiarEscenaDespuesDeSonido());
            }
            else
            {
                SceneManager.LoadScene("kioskoSaludable");
            }
        }
    }

    private IEnumerator CambiarEscenaDespuesDeSonido()
    {
        // Esperar un poco para que se escuche el sonido
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("kioskoSaludable");
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