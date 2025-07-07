using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instancia;

    [Header("Sound Effects Generales")]
    [SerializeField] private AudioClip grabSound;
    [SerializeField] private AudioClip dropSound;
    [SerializeField] private AudioClip errorSound;
    [SerializeField] private AudioClip buttonClickSound;
    [SerializeField] private AudioClip starEarnedSound;

    [Header("Música de Fondo")]
    [SerializeField] private AudioClip backgroundMusicSampleScene;
    [SerializeField] private AudioClip backgroundMusicIntro;
    [SerializeField] private AudioClip backgroundMusicEscuela;
    [SerializeField] private AudioClip backgroundMusicParque;
    [SerializeField] private AudioClip backgroundMusicKiosko;
    [SerializeField] private AudioClip backgroundMusicExtra;

    [Header("Voice Hints por Escena")]
    [SerializeField] private AudioClip voiceHintSampleScene;
    [SerializeField] private AudioClip voiceHintIntro;
    [SerializeField] private AudioClip voiceHintEscuela;
    [SerializeField] private AudioClip voiceHintParque;
    [SerializeField] private AudioClip kioskoTutorialVoice; // Nueva

    [Header("SFX por Tipo de Tarea")]
    [SerializeField] private AudioClip sonidoMochila;
    [SerializeField] private AudioClip sonidoRopaSucia;
    [SerializeField] private AudioClip sonidoAlmohada;
    [SerializeField] private AudioClip sonidoBasura;
    [SerializeField] private AudioClip sonidoFlor;
    [SerializeField] private AudioClip sonidoTarea;
    [SerializeField] private AudioClip sonidoHeces;

    [Header("Sonidos Especiales Kiosko")]
    [SerializeField] private AudioClip productoSeleccionadoSound;
    [SerializeField] private AudioClip compraExitosaSound;
    [SerializeField] private AudioClip compraIncorrectaSound;
    [SerializeField] private AudioClip vueltoSound;

    private AudioSource sfxSource;
    private AudioSource musicSource;
    private AudioSource voiceSource;
    public AudioSource GetVoiceSource() => voiceSource;
    private bool kioskoAbierto = false;

    private void Awake()
    {
        if (Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject);

            sfxSource = gameObject.AddComponent<AudioSource>();
            voiceSource = gameObject.AddComponent<AudioSource>();
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        CambiarMusicaDeFondo(SceneManager.GetActiveScene().name);
        ReproducirVoiceHintConFade(SceneManager.GetActiveScene().name);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Parque")
        {
            kioskoAbierto = false;  // Resetear estado
            CambiarMusicaDeFondo("Parque");
        }
        else
        {
            CambiarMusicaDeFondo(scene.name);
        }

        ReproducirVoiceHintConFade(scene.name);
    }

    public void CambiarMusicaDeFondo(string nombreEscena)
    {
        AudioClip nuevaMusica = nombreEscena switch
        {
            "Casa" => backgroundMusicSampleScene,
            "MainMenu" => backgroundMusicIntro,
            "Escuela" or "Minijuego" => backgroundMusicEscuela,
            "Parque" => backgroundMusicParque,
            "Extra" => backgroundMusicExtra,
            _ => null
        };

        if (nuevaMusica != null && (musicSource.clip != nuevaMusica || !musicSource.isPlaying))
        {
            musicSource.Stop();
            musicSource.clip = nuevaMusica;
            musicSource.volume = nombreEscena == "SampleScene" ? 1.0f : 0.4f;
            musicSource.volume = nombreEscena == "Extra" ? 0.5f : 0.4f;
            musicSource.Play();
        }
    }

    public void ReproducirVoiceHintConFade(string nombreEscena)
    {
        AudioClip voiceHint = nombreEscena switch
        {
            "Casa" => voiceHintSampleScene,
            "MainMenu" => voiceHintIntro,
            "Escuela" => voiceHintEscuela,
            "Parque" => kioskoTutorialVoice,
            _ => null
        };

        if (voiceHint != null)
            StartCoroutine(FadeOutMusicAndPlayHint(voiceHint));
    }

    public void PlayGrabSound() => sfxSource.PlayOneShot(grabSound);
    public void PlayDropSound() => sfxSource.PlayOneShot(dropSound);
    public void PlayErrorSound() => sfxSource.PlayOneShot(errorSound);
    public void PlayButtonClickSound() => sfxSource.PlayOneShot(buttonClickSound);
    public void PlayStarEarnedSound() => sfxSource.PlayOneShot(starEarnedSound);
    public void PlayCustomSound(AudioClip clip) => sfxSource.PlayOneShot(clip);

    public void PlaySoundConFade(AudioClip clip)
    {
        if (clip != null)
            StartCoroutine(FadeOutMusicAndPlayHint(clip));
    }

    public AudioClip ObtenerClipPorTipoTarea(TipoTarea tipo) => tipo switch
    {
        TipoTarea.Mochila => sonidoMochila,
        TipoTarea.RopaSucia => sonidoRopaSucia,
        TipoTarea.Almohada => sonidoAlmohada,
        TipoTarea.Basura => sonidoBasura,
        TipoTarea.Flor => sonidoFlor,
        TipoTarea.Tarea => sonidoTarea,
        TipoTarea.Heces => sonidoHeces,
        _ => null
    };

    private IEnumerator FadeOutMusicAndPlayHint(AudioClip clip)
    {
        float fadeDuration = 1f;
        float targetVolume = 0.1f;
        float originalVolume = musicSource.volume;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            musicSource.volume = Mathf.Lerp(originalVolume, targetVolume, t / fadeDuration);
            yield return null;
        }

        musicSource.volume = targetVolume;
        voiceSource.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            musicSource.volume = Mathf.Lerp(targetVolume, originalVolume, t / fadeDuration);
            yield return null;
        }

        musicSource.volume = originalVolume;
    }

    // ----------- Funciones Especiales del Kiosko ----------- //

    public void AbrirKiosko()
    {
        if (kioskoAbierto) return;
        kioskoAbierto = true;
        StartCoroutine(TransicionMusicaKiosko(true));

        if (voiceHintParque != null)
            StartCoroutine(PlayTutorialWithDelay(0.5f));
    }

    public void CerrarKiosko()
    {
        if (!kioskoAbierto) return;
        kioskoAbierto = false;
        StartCoroutine(TransicionMusicaKiosko(false));
        CambiarMusicaDeFondo("Parque");
    }


    private IEnumerator TransicionMusicaKiosko(bool abrir)
    {
        float fadeDuration = 1f;
        float objetivoVolumen = abrir ? 0.4f : 0f;
        float volumenInicial = musicSource.volume;

        if (abrir && backgroundMusicKiosko != null)
        {
            musicSource.clip = backgroundMusicKiosko;
            musicSource.Play();
        }

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            musicSource.volume = Mathf.Lerp(volumenInicial, objetivoVolumen, t / fadeDuration);
            yield return null;
        }

        musicSource.volume = objetivoVolumen;

        if (!abrir)
            musicSource.Stop();
    }

    private IEnumerator PlayTutorialWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        voiceSource.clip = voiceHintParque;
        voiceSource.Play();
    }

    public void PlayProductoSeleccionado() => sfxSource.PlayOneShot(productoSeleccionadoSound);
    public void PlayCompraExitosa() => sfxSource.PlayOneShot(compraExitosaSound);
    public void PlayCompraIncorrecta() => sfxSource.PlayOneShot(compraIncorrectaSound);
    public void PlayVueltoSound(int cantidadVuelto) => StartCoroutine(AnunciarVuelto(cantidadVuelto));

    private IEnumerator AnunciarVuelto(int cantidadVuelto)
    {
        sfxSource.PlayOneShot(vueltoSound);
        yield return new WaitForSeconds(0.5f);
        // Puedes agregar clips personalizados de voz según el monto aquí
    }

    // ----------- Otros ----------- //

    public AudioSource GetMusicSource() => musicSource;
    public void AjustarVolumenMusica(float nuevoVolumen) => musicSource.volume = nuevoVolumen;

    public void RepetirVoiceHintActual()
    {
        string escena = SceneManager.GetActiveScene().name;
        AudioClip hint = escena switch
        {
            "Casa" => voiceHintSampleScene,
            "MainMenu" => voiceHintIntro,
            "Escuela" => voiceHintEscuela,
            "Parque" => kioskoTutorialVoice,
            _ => null
        };

        if (hint != null)
            PlaySoundConFade(hint);
    }
    public void ReproducirVozObjeto(AudioClip clip)
    {
        if (clip == null) return;

        // No reproducir si ya hay un voice hint u otro diálogo en curso
        if (voiceSource.isPlaying) return;

        voiceSource.PlayOneShot(clip);
    }
    public AudioClip ObtenerVoiceHintActual()
    {
        string escena = SceneManager.GetActiveScene().name;

        return escena switch
        {
            "Casa" => voiceHintSampleScene,
            "MainMenu" => voiceHintIntro,
            "Escuela" => voiceHintEscuela,
            "Parque" => kioskoTutorialVoice,
            _ => null
        };
    }

}
