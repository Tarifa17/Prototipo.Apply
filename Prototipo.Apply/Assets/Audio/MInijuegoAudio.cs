using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinijuegoAudio : MonoBehaviour
{
    public static MinijuegoAudio Instancia;

    [Header("Sonidos de Interacción")]
    [SerializeField] private AudioClip sonidoAgarrar;
    [SerializeField] private AudioClip sonidoColocar;
    [SerializeField] private AudioClip sonidoBoton;
    [SerializeField] private AudioClip sonidoError;

    [Header("Sonidos de Validación")]
    [SerializeField] private AudioClip sonidoPalabraCorrecta;
    [SerializeField] private AudioClip sonidoPalabraIncorrecta;
    [SerializeField] private AudioClip sonidoLapiceraCorrecta;
    [SerializeField] private AudioClip sonidoLapiceraIncorrecta;
    [SerializeField] private AudioClip sonidoEstrella;

    private AudioSource audioSource;

    private void Awake()
    {
        // Patrón Singleton
        if (Instancia == null)
        {
            Instancia = this;
            audioSource = GetComponent<AudioSource>();

            // Si no tiene AudioSource, lo agregamos
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Método para reproducir un sonido cualquiera
    private void ReproducirSonido(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            // Intentar usar el AudioManager general si el clip no está asignado
            if (AudioManager.Instancia != null && clip == null)
            {
                switch (clip)
                {
                    case var _ when clip == sonidoAgarrar:
                        AudioManager.Instancia.PlayGrabSound();
                        break;
                    case var _ when clip == sonidoColocar:
                        AudioManager.Instancia.PlayDropSound();
                        break;
                    case var _ when clip == sonidoError:
                        AudioManager.Instancia.PlayErrorSound();
                        break;
                    case var _ when clip == sonidoBoton:
                        AudioManager.Instancia.PlayButtonClickSound();
                        break;
                    case var _ when clip == sonidoEstrella:
                        AudioManager.Instancia.PlayStarEarnedSound();
                        break;
                }
            }
        }
    }

    // Métodos públicos para reproducir cada tipo de sonido
    public void SonidoAgarrar()
    {
        ReproducirSonido(sonidoAgarrar);
    }

    public void SonidoColocar()
    {
        ReproducirSonido(sonidoColocar);
    }

    public void SonidoBoton()
    {
        ReproducirSonido(sonidoBoton);
    }

    public void SonidoError()
    {
        ReproducirSonido(sonidoError);
    }

    public void SonidoPalabraCorrecta()
    {
        ReproducirSonido(sonidoPalabraCorrecta);
    }

    public void SonidoPalabraIncorrecta()
    {
        ReproducirSonido(sonidoPalabraIncorrecta);
    }

    public void SonidoLapiceraCorrecta()
    {
        ReproducirSonido(sonidoLapiceraCorrecta);
    }

    public void SonidoLapiceraIncorrecta()
    {
        ReproducirSonido(sonidoLapiceraIncorrecta);
    }

    public void SonidoEstrella()
    {
        ReproducirSonido(sonidoEstrella);
    }
}