using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instancia;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip grabSound;
    [SerializeField] private AudioClip dropSound;
    [SerializeField] private AudioClip errorSound;

    [Header("Music")]
    [SerializeField] private AudioClip backgroundMusic;

    private AudioSource sfxSource;
    private AudioSource musicSource;

    private bool isErrorSoundPlaying = false; // Control para evitar múltiples reproducciones del errorSound
    private float errorSoundCooldown = 1f; // Tiempo mínimo entre reproducciones del errorSound (en segundos)

    private void Awake()
    {
        if (Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject);

            sfxSource = gameObject.AddComponent<AudioSource>();
            musicSource = gameObject.AddComponent<AudioSource>();

            musicSource.loop = true;
            musicSource.clip = backgroundMusic;
            musicSource.volume = 0.4f;
            musicSource.Play();
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
        if (!isErrorSoundPlaying) // Solo reproducir si no se está reproduciendo ya
        {
            sfxSource.PlayOneShot(errorSound);
            isErrorSoundPlaying = true;
            Invoke(nameof(ResetErrorSound), errorSoundCooldown); // Restablecer después del cooldown
        }
    }

    private void ResetErrorSound()
    {
        isErrorSoundPlaying = false;
    }
}