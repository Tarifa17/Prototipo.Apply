using UnityEngine;

public class VoiceHintButtonHelper : MonoBehaviour
{
    private bool reproduciendoHint = false;

    public void ReproducirVoiceHint()
    {
        if (reproduciendoHint)
        {
            Debug.Log("Ya se está reproduciendo un VoiceHint.");
            return;
        }

        if (AudioManager.Instancia != null)
        {
            AudioSource voiceSource = AudioManager.Instancia.GetVoiceSource();
            if (voiceSource != null && !voiceSource.isPlaying)
            {
                StartCoroutine(ReproducirConBloqueo(voiceSource));
            }
            else
            {
                Debug.Log("El VoiceHint ya está en reproducción.");
            }
        }
        else
        {
            Debug.LogWarning("AudioManager no encontrado al intentar reproducir el VoiceHint.");
        }
    }

    private System.Collections.IEnumerator ReproducirConBloqueo(AudioSource voiceSource)
    {
        reproduciendoHint = true;
        AudioManager.Instancia.RepetirVoiceHintActual();
        yield return new WaitWhile(() => voiceSource.isPlaying);
        reproduciendoHint = false;
    }
}
