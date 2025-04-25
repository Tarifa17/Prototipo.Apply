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
    [SerializeField] private AudioClip voiceHintSound;

    [Header("Music")]
    [SerializeField] private AudioClip backgroundMusicSampleScene;
    [SerializeField] private AudioClip backgroundMusicIntro;
    [SerializeField] private AudioClip backgroundMusicEscuela;

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
    }

    public void CambiarMusicaDeFondo(string nombreEscena)
    {
        switch (nombreEscena)
        {
            case "SampleScene":
                musicSource.clip = backgroundMusicSampleScene;
                break;
            case "intro":
                musicSource.clip = backgroundMusicIntro;
                break;
            case "Escuela":
                musicSource.clip = backgroundMusicEscuela;
                break;
            default:
                musicSource.clip = null;
                break;
        }

        if (musicSource.clip != null)
        {
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

    public void PlayVoiceHint()
    {
        sfxSource.PlayOneShot(voiceHintSound);
    }

    public void PlayCustomSound(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}