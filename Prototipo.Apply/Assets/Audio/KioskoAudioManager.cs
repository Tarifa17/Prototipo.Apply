using System.Collections;
using UnityEngine;
using TMPro;

public class KioskoAudioManager : MonoBehaviour
{
    public static KioskoAudioManager Instance { get; private set; }

    [Header("Audio Clips")]
    [SerializeField] private AudioClip kioskoTutorialVoice;
    [SerializeField] private AudioClip productoSeleccionadoSound;
    [SerializeField] private AudioClip compraExitosaSound;
    [SerializeField] private AudioClip compraIncorrectaSound;
    [SerializeField] private AudioClip botonClickSound;
    [SerializeField] private AudioClip vueltoSound;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource voiceSource;
    [SerializeField] private AudioSource musicSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Crear fuentes de audio si no est�n asignadas
        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.volume = 0.7f;
        }

        if (voiceSource == null)
        {
            voiceSource = gameObject.AddComponent<AudioSource>();
            voiceSource.volume = 1f;
        }

        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true;
            musicSource.volume = 0.4f;
        }
    }

    private void Start()
    {
        // Reproducir tutorial de voz al iniciar
        if (kioskoTutorialVoice != null)
        {
            StartCoroutine(PlayTutorialWithDelay(0.5f));
        }
    }

    private IEnumerator PlayTutorialWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        voiceSource.clip = kioskoTutorialVoice;
        voiceSource.Play();
    }

    public void PlayProductoSeleccionado()
    {
        sfxSource.PlayOneShot(productoSeleccionadoSound);
    }

    public void PlayCompraExitosa()
    {
        sfxSource.PlayOneShot(compraExitosaSound);
    }

    public void PlayCompraIncorrecta()
    {
        sfxSource.PlayOneShot(compraIncorrectaSound);
    }

    public void PlayBotonClick()
    {
        sfxSource.PlayOneShot(botonClickSound);
    }

    public void PlayVueltoSound(int cantidadVuelto)
    {
        StartCoroutine(AnunciarVuelto(cantidadVuelto));
    }

    private IEnumerator AnunciarVuelto(int cantidadVuelto)
    {
        // Primero reproducir el sonido de vuelto
        sfxSource.PlayOneShot(vueltoSound);

        // Esperar a que termine el sonido de vuelto
        yield return new WaitForSeconds(0.5f);

        // Aqu� podr�as reproducir un clip de voz que anuncie el vuelto
        // Por ahora, dejamos esto preparado para futuras implementaciones
    }
}