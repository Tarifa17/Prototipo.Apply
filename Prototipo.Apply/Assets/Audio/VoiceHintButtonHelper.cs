using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VoiceHintButtonHelper : MonoBehaviour
{
    private bool reproduciendoHint = false;

    public void ReproducirVoiceHint()
    {
        if (AudioManager.Instancia == null)
        {
            Debug.LogWarning("AudioManager no encontrado.");
            return;
        }

        AudioSource voiceSource = AudioManager.Instancia.GetVoiceSource();
        AudioClip hintClip = AudioManager.Instancia.ObtenerVoiceHintActual();

        // Verificamos que no se esté reproduciendo ya un hint
        if (reproduciendoHint || (voiceSource != null && voiceSource.isPlaying))
        {
            Debug.Log("VoiceHint ya en reproducción.");
            return;
        }

        if (voiceSource == null || hintClip == null)
        {
            Debug.LogWarning("Datos faltantes para reproducir el VoiceHint.");
            return;
        }

        StartCoroutine(ReproducirHint(voiceSource, hintClip));
    }

    private IEnumerator ReproducirHint(AudioSource voiceSource, AudioClip clip)
    {
        reproduciendoHint = true;

        voiceSource.clip = clip;
        voiceSource.Play();

        yield return new WaitForSeconds(clip.length);

        reproduciendoHint = false;
    }
}
