using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProductoUI : MonoBehaviour
{
    [SerializeField] private Producto datosProducto;
    [SerializeField] private TextMeshProUGUI textoNombre;
    [SerializeField] private TextMeshProUGUI textoPrecio;
    [SerializeField] private Button botonSeleccionar;

    private KioskoSaludableManager kioskoManager;
    private KioskoAudioManager audioManager;

    private void Start()
    {
        // Obtener referencia al KioskoManager
        kioskoManager = FindObjectOfType<KioskoSaludableManager>();

        // Obtener referencia al audio manager
        audioManager = KioskoAudioManager.Instance;

        // Configurar UI
        if (textoNombre != null)
            textoNombre.text = datosProducto.nombre;

        if (textoPrecio != null)
            textoPrecio.text = $"${datosProducto.precio}";

        // Configurar botón
        botonSeleccionar.onClick.AddListener(SeleccionarEsteProducto);
    }

    private void SeleccionarEsteProducto()
    {
        // Reproducir sonido de clic
        if (audioManager != null)
        {
            audioManager.PlayBotonClick();
        }

        if (kioskoManager != null)
        {
            kioskoManager.SeleccionarProducto(datosProducto);
        }
        else
        {
            Debug.LogError("No se encontró el KioskoSaludableManager");
        }
    }

    // Método para obtener el producto asignado
    public Producto GetProducto()
    {
        return datosProducto;
    }
}