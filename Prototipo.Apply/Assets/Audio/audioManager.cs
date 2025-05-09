using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instancia;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip grabSound;
    [SerializeField] private AudioClip dropSound;
    [SerializeField] private AudioClip errorSound;
    [SerializeField] private AudioClip buttonClickSound;
    [SerializeField] private AudioClip starEarnedSound;

    [Header("Music")]
    [SerializeField] private AudioClip backgroundMusicSampleScene;
    [SerializeField] private AudioClip backgroundMusicIntro;
    [SerializeField] private AudioClip backgroundMusicEscuela;
    [SerializeField] private AudioClip backgroundMusicParque;

    [Header("Voice Hints")]
    [SerializeField] private AudioClip voiceHintSampleScene;
    [SerializeField] private AudioClip voiceHintIntro;
    [SerializeField] private AudioClip voiceHintEscuela;

    private AudioSource sfxSource; // Fuente de efectos de sonido
    private AudioSource musicSource; // Fuente de música

    private void Awake()
    {
        if (Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject);

            sfxSource = gameObject.AddComponent<AudioSource>();
            musicSource = gameObject.AddComponent<AudioSource>();

            musicSource.loop = true;
        }
        else
        {
            Destroy(gameObject); // Evitar duplicados
        }
    }

    private void Start()
    {
        // Configurar la música de fondo inicial para la escena actual
        CambiarMusicaDeFondo(SceneManager.GetActiveScene().name);

        // Reproducir el tutorial de voz inicial con atenuación de la música
        ReproducirVoiceHintConFade(SceneManager.GetActiveScene().name);

        // Escuchar cambios de escena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // Eliminar el evento al destruir el objeto
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Cambiar la música de fondo al cargar una nueva escena
        CambiarMusicaDeFondo(scene.name);

        // Reproducir el tutorial de voz para la nueva escena con fade
        ReproducirVoiceHintConFade(scene.name);
    }

    public void CambiarMusicaDeFondo(string nombreEscena)
    {
        AudioClip nuevaMusica = null;

        switch (nombreEscena)
        {
            case "SampleScene":
                nuevaMusica = backgroundMusicSampleScene;
                break;
            case "intro":
                nuevaMusica = backgroundMusicIntro;
                break;
            case "Escuela":
                nuevaMusica = backgroundMusicEscuela; // La misma música que el minijuego
                break;
            case "Minijuego":
                nuevaMusica = backgroundMusicEscuela; // Usamos la misma música para el minijuego
                break;
            case "Parque":
                nuevaMusica = backgroundMusicParque;
                break;
            default:
                nuevaMusica = null;
                break;
        }

        // Verificar si la música ya está reproduciéndose
        if (nuevaMusica != null && (musicSource.clip != nuevaMusica || !musicSource.isPlaying))
        {
            musicSource.Stop(); // Detener cualquier música en reproducción
            musicSource.clip = nuevaMusica;
            musicSource.volume = 0.4f;
            musicSource.Play();
        }
    }

    // Métodos para reproducir efectos de sonido
    public void PlayGrabSound()
    {
        sfxSource.PlayOneShot(grabSound);
    }

    public void PlayDropSound()
    {
        sfxSource.PlayOneShot(dropSound);
    }

    public void PlayErrorSound()
    {
        sfxSource.PlayOneShot(errorSound);
    }

    public void PlayButtonClickSound()
    {
        sfxSource.PlayOneShot(buttonClickSound);
    }

    public void PlayStarEarnedSound()
    {
        sfxSource.PlayOneShot(starEarnedSound);
    }

    public void PlayCustomSound(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    // Reproducir Voice Hint con fade para cada escena
    public void ReproducirVoiceHintConFade(string nombreEscena)
    {
        AudioClip voiceHint = null;

        switch (nombreEscena)
        {
            case "SampleScene":
                voiceHint = voiceHintSampleScene;
                break;
            case "MainMenu":
                voiceHint = voiceHintIntro;
                break;
            case "Escuela":
                voiceHint = voiceHintEscuela;
                break;
        }

        if (voiceHint != null)
        {
            StartCoroutine(FadeOutMusicAndPlayHint(voiceHint));
        }
    }

    private IEnumerator FadeOutMusicAndPlayHint(AudioClip hintClip)
    {
        // Atenuar la música de fondo (fade out)
        float fadeDuration = 1.0f; // Duración del fade
        float targetVolume = 0.1f; // Volumen reducido para la música
        float originalVolume = musicSource.volume;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            musicSource.volume = Mathf.Lerp(originalVolume, targetVolume, t / fadeDuration);
            yield return null;
        }

        musicSource.volume = targetVolume;

        // Reproducir el hint
        sfxSource.PlayOneShot(hintClip);

        // Esperar hasta que termine el hint
        yield return new WaitForSeconds(hintClip.length);

        // Restaurar la música de fondo (fade in)
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            musicSource.volume = Mathf.Lerp(targetVolume, originalVolume, t / fadeDuration);
            yield return null;
        }

        musicSource.volume = originalVolume;
    }
    public AudioSource GetMusicSource()
    {
        return musicSource;
    }

    // Método para ajustar temporalmente el volumen de la música de fondo
    // (útil para cuando se abre un panel como el kiosko)
    public void AjustarVolumenMusica(float nuevoVolumen)
    {
        if (musicSource != null)
        {
            musicSource.volume = nuevoVolumen;
        }
    }
}