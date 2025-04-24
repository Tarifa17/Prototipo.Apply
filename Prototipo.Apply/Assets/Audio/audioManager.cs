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
            musicSource.clip = backgroundMusic;
            musicSource.volume = 0.4f;
            musicSource.Play();
        }
        else
        {
            Destroy(gameObject); // Evitar duplicados
        }
    }

    public void PlayGrabSound()
    {
        sfxSource.PlayOneShot(grabSound); // Reproducir sonido de agarrar
    }

    public void PlayDropSound()
    {
        sfxSource.PlayOneShot(dropSound); // Reproducir sonido de soltar
    }

    public void PlayErrorSound()
    {
        sfxSource.PlayOneShot(errorSound); // Reproducir sonido de error
    }
}