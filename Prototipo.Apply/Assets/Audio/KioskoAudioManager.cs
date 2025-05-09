using System.Collections;
using UnityEngine;
using TMPro;

public class KioskoAudioManager : MonoBehaviour
{
    public static KioskoAudioManager Instance { get; private set; }

    [Header("Audio Clips")]
    [SerializeField] private AudioClip kioskoBackgroundMusic;  // Nueva variable para la música del kiosko
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

    // Referencias al AudioManager principal
    private AudioManager mainAudioManager;
    private float originalMainMusicVolume;
    private bool kioskoAbierto = false;

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

        // Crear fuentes de audio si no están asignadas
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
            musicSource.volume = 0.0f; // Iniciar en silencio
        }

        // Buscar el AudioManager principal
        mainAudioManager = AudioManager.Instancia;
    }

    private void Start()
    {
        // Iniciar con la música del kiosko cargada pero sin reproducir
        if (kioskoBackgroundMusic != null)
        {
            musicSource.clip = kioskoBackgroundMusic;
        }
    }

    private void OnEnable()
    {
        // Si el panel del kiosko se activa, iniciar la música del kiosko
        if (!kioskoAbierto && gameObject.activeInHierarchy)
        {
            AbrirKiosko();
        }
    }

    private void OnDisable()
    {
        // Si el panel del kiosko se desactiva, restaurar la música principal
        if (kioskoAbierto)
        {
            CerrarKiosko();
        }
    }

    // Método para activar la música del kiosko
    public void AbrirKiosko()
    {
        if (kioskoAbierto) return;

        // Fade out de la música principal y fade in de la música del kiosko
        StartCoroutine(CambiarMusicaKiosko(true));

        // Reproducir tutorial de voz al abrir el kiosko
        if (kioskoTutorialVoice != null)
        {
            StartCoroutine(PlayTutorialWithDelay(0.5f));
        }

        kioskoAbierto = true;
    }

    // Método para restaurar la música principal
    public void CerrarKiosko()
    {
        if (!kioskoAbierto) return;

        // Fade out de la música del kiosko y fade in de la música principal
        StartCoroutine(CambiarMusicaKiosko(false));

        kioskoAbierto = false;
    }

    private IEnumerator CambiarMusicaKiosko(bool abriendo)
    {
        float fadeDuration = 1.0f;
        float targetKioskoVolume = abriendo ? 0.4f : 0f;
        float initialKioskoVolume = musicSource.volume;

        // Comenzar a reproducir música del kiosko si estamos abriendo
        if (abriendo && kioskoBackgroundMusic != null && !musicSource.isPlaying)
        {
            musicSource.Play();
        }

        // Atenuar música principal si existe
        if (mainAudioManager != null)
        {
            // Buscar la fuente de música del AudioManager principal
            AudioSource mainMusicSource = null;
            AudioSource[] sources = mainAudioManager.GetComponents<AudioSource>();
            foreach (AudioSource source in sources)
            {
                if (source.loop) // Asumimos que la música de fondo es la que tiene loop
                {
                    mainMusicSource = source;
                    break;
                }
            }

            if (mainMusicSource != null)
            {
                // Guardar el volumen original solo al abrir el kiosko
                if (abriendo)
                {
                    originalMainMusicVolume = mainMusicSource.volume;
                }

                float targetMainVolume = abriendo ? 0.1f : originalMainMusicVolume;
                float initialMainVolume = mainMusicSource.volume;

                for (float t = 0; t < fadeDuration; t += Time.deltaTime)
                {
                    // Fade in/out música del kiosko
                    musicSource.volume = Mathf.Lerp(initialKioskoVolume, targetKioskoVolume, t / fadeDuration);

                    // Fade out/in música principal
                    mainMusicSource.volume = Mathf.Lerp(initialMainVolume, targetMainVolume, t / fadeDuration);

                    yield return null;
                }

                // Asegurar que llegamos a los valores objetivo
                musicSource.volume = targetKioskoVolume;
                mainMusicSource.volume = targetMainVolume;
            }
        }
        else
        {
            // Si no hay AudioManager principal, solo hacemos fade de la música del kiosko
            for (float t = 0; t < fadeDuration; t += Time.deltaTime)
            {
                musicSource.volume = Mathf.Lerp(initialKioskoVolume, targetKioskoVolume, t / fadeDuration);
                yield return null;
            }
            musicSource.volume = targetKioskoVolume;
        }

        // Detener la música del kiosko si estamos cerrando
        if (!abriendo && musicSource.volume < 0.01f)
        {
            musicSource.Stop();
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

        // Aquí podrías reproducir un clip de voz que anuncie el vuelto
        // Por ahora, dejamos esto preparado para futuras implementaciones
    }
}