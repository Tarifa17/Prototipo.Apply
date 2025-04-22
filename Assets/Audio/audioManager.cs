using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Esta clase maneja los efectos de sonido y m�sica del juego.
public class AudioManager : MonoBehaviour
{
    // Instancia est�tica que permite acceder a este script desde cualquier otro.
    public static AudioManager Instancia;

    [Header("Sound Effects")] // Agrupa sonidos de efectos en el inspector de Unity.
    [SerializeField] private AudioClip grabSound; // Sonido para "agarrar" un objeto.
    [SerializeField] private AudioClip dropSound; // Sonido para "soltar" un objeto.
    [SerializeField] private AudioClip errorSound; // Sonido para un error.

    [Header("Music")] // Agrupa m�sica en el inspector de Unity.
    [SerializeField] private AudioClip backgroundMusic; // M�sica de fondo del juego.

    private AudioSource sfxSource; // Fuente para reproducir efectos de sonido.
    private AudioSource musicSource; // Fuente para reproducir m�sica de fondo.

    private void Awake()
    {
        // Implementaci�n del patr�n Singleton: asegura que solo exista un AudioManager.
        if (Instancia == null)
        {
            Instancia = this; // Guardamos esta instancia como la principal.
            DontDestroyOnLoad(gameObject); // Evita que el objeto se destruya al cambiar de escena.

            // Creamos componentes de AudioSource para efectos de sonido y m�sica.
            sfxSource = gameObject.AddComponent<AudioSource>();
            musicSource = gameObject.AddComponent<AudioSource>();

            // Configuramos la m�sica de fondo.
            musicSource.loop = true; // La m�sica se repetir� en bucle.
            musicSource.clip = backgroundMusic; // Asignamos el clip de m�sica.
            musicSource.volume = 0.4f; // Bajamos el volumen para que no sea tan fuerte.
            musicSource.Play(); // Reproducimos la m�sica de fondo.
        }
        else
        {
            // Si ya existe un AudioManager, destruimos este objeto duplicado.
            Destroy(gameObject);
        }
    }

    // M�todo para reproducir el sonido de "agarrar" un objeto.
    public void PlayGrabSound()
    {
        sfxSource.PlayOneShot(grabSound); // Reproduce el sonido una sola vez.
    }

    // M�todo para reproducir el sonido de "soltar" un objeto.
    public void PlayDropSound()
    {
        sfxSource.PlayOneShot(dropSound); // Reproduce el sonido una sola vez.
    }

    // M�todo para reproducir el sonido de error.
    public void PlayErrorSound()
    {
        sfxSource.PlayOneShot(errorSound); // Reproduce el sonido una sola vez.
    }
}
