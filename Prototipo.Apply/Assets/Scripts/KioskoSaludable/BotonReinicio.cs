using UnityEngine;
using UnityEngine.UI;

public class BotonReinicio : MonoBehaviour
{
    private Button botonReinicio;
    private KioskoSaludableManager kioskoManager;
    private KioskoAudioManager audioManager;

    private void Start()
    {
        kioskoManager = FindObjectOfType<KioskoSaludableManager>();
        botonReinicio = GetComponent<Button>();
        audioManager = KioskoAudioManager.Instance;

        if (botonReinicio != null && kioskoManager != null)
        {
            // Añadir primero la reproducción de sonido y luego el reinicio
            botonReinicio.onClick.AddListener(ClickReinicio);
        }
        else
        {
            Debug.LogError("No se encontró el botón o el KioskoSaludableManager");
        }
    }

    private void ClickReinicio()
    {
        // Reproducir sonido de clic
        if (audioManager != null)
        {
            audioManager.PlayBotonClick();
        }

        // Reiniciar el kiosko
        kioskoManager.ReiniciarKiosko();
    }
}