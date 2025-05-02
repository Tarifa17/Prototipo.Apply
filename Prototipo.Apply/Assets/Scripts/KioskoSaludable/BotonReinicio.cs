using UnityEngine;
using UnityEngine.UI;

public class BotonReinicio : MonoBehaviour
{
    private Button botonReinicio;
    private KioskoSaludableManager kioskoManager;

    private void Start()
    {
        kioskoManager = FindObjectOfType<KioskoSaludableManager>();
        botonReinicio = GetComponent<Button>();

        if (botonReinicio != null && kioskoManager != null)
        {
            botonReinicio.onClick.AddListener(kioskoManager.ReiniciarKiosko);
        }
        else
        {
            Debug.LogError("No se encontró el botón o el KioskoSaludableManager");
        }
    }
}