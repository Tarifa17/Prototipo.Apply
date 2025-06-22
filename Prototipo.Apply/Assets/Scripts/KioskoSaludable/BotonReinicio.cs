using UnityEngine;
using UnityEngine.UI;

public class BotonReinicio : MonoBehaviour
{
    private Button botonReinicio;
    private KioskoSaludableManager kioskoManager;
    private AudioManager audioManager;

    private void Start()
    {
        kioskoManager = FindObjectOfType<KioskoSaludableManager>();
        botonReinicio = GetComponent<Button>();
        audioManager = AudioManager.Instancia;

        if (botonReinicio != null && kioskoManager != null)
        {
            botonReinicio.onClick.AddListener(ClickReinicio);
        }
        else
        {
            Debug.LogError("No se encontró el botón o el KioskoSaludableManager");
        }
    }

    private void ClickReinicio()
    {
        if (audioManager != null)
        {
            audioManager.PlayButtonClickSound(); // Ya adaptado en AudioManager
        }

        kioskoManager.ReiniciarKiosko();
    }
}
