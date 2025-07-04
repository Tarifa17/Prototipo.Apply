using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProductoUI : MonoBehaviour
{
    [SerializeField] private Producto datosProducto;
    [SerializeField] private TextMeshProUGUI textoNombre;
    [SerializeField] private TextMeshProUGUI textoPrecio;
    [SerializeField] private Button botonSeleccionar;
    [SerializeField] private GameObject tildeSeleccion;

    private KioskoSaludableManager kioskoManager;
    private AudioManager audioManager;

    private void Start()
    {
        // Obtener referencia al KioskoManager
        kioskoManager = FindObjectOfType<KioskoSaludableManager>();

        // Obtener referencia al audio manager unificado
        audioManager = AudioManager.Instancia;

        // Configurar UI
        if (textoNombre != null)
            textoNombre.text = datosProducto.nombre;

        if (textoPrecio != null)
            textoPrecio.text = $"${datosProducto.precio}";
        if (tildeSeleccion != null)
            tildeSeleccion.SetActive(false);

        // Configurar bot�n
        botonSeleccionar.onClick.AddListener(SeleccionarEsteProducto);
    }

    private void SeleccionarEsteProducto()
    {
        // Reproducir sonido de clic
        if (audioManager != null)
        {
            audioManager.PlayButtonClickSound(); // M�todo del AudioManager unificado
        }

        if (kioskoManager != null)
        {
            kioskoManager.SeleccionarProducto(datosProducto, this.transform);
        }
        else
        {
            Debug.LogError("No se encontr� el KioskoSaludableManager");
        }
    }

    // M�todo para obtener el producto asignado
    public Producto GetProducto()
    {
        return datosProducto;
    }

    public void MostrarTilde(bool mostrar)
    {
        if (tildeSeleccion != null)
            tildeSeleccion.SetActive(mostrar);
    }

}
