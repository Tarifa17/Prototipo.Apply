using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KioskoSaludableManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI textoDineroDisponible;
    [SerializeField] private TextMeshProUGUI textoPrecioProducto;
    [SerializeField] private GameObject panelVuelto;
    [SerializeField] private TextMeshProUGUI textoVuelto;
    [SerializeField] private GameObject panelIncorrecto;
    [SerializeField] private GameObject panelCorrecto;
    [SerializeField] private Button botonComprar;
    [SerializeField] private GameObject contenedorVacios;
    [SerializeField] private GameObject panelKioskoPrincipal;

    [Header("Productos")]
    [SerializeField] private ProductoUI[] productosUI;

    [Header("Configuraciones")]
    [SerializeField] private float tiempoEspera = 3f;

    private int dineroDisponible = 4000;
    private Producto productoSeleccionado;
    private AudioManager audioManager;

    public static KioskoSaludableManager Instancia { get; private set; }

    private void Awake()
    {
        if (Instancia == null)
        {
            Instancia = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        audioManager = AudioManager.Instancia;
        if (audioManager == null)
        {
            Debug.LogWarning("AudioManager no encontrado.");
        }

        ActualizarUIInicial();
        botonComprar.onClick.AddListener(ComprarProducto);

        if (productosUI == null || productosUI.Length == 0)
        {
            productosUI = FindObjectsOfType<ProductoUI>();
        }
    }

    private void ActualizarUIInicial()
    {
        textoDineroDisponible.text = $"Dinero disponible: ${dineroDisponible}";
        textoPrecioProducto.text = "Precio: $0";
        panelVuelto.SetActive(false);
        panelIncorrecto.SetActive(false);
        if (panelCorrecto != null) panelCorrecto.SetActive(false);
        if (contenedorVacios != null) contenedorVacios.SetActive(true);
    }

    public void SeleccionarProducto(Producto producto)
    {
        productoSeleccionado = producto;
        textoPrecioProducto.text = $"Precio: ${producto.precio}";

        if (audioManager != null)
        {
            audioManager.PlayProductoSeleccionado();
        }

        if (contenedorVacios != null)
            contenedorVacios.SetActive(false);
    }

    private void ComprarProducto()
    {
        if (audioManager != null)
        {
            audioManager.PlayButtonClickSound();
        }

        if (productoSeleccionado == null)
        {
            Debug.Log("No has seleccionado ningún producto.");
            return;
        }

        int vuelto = dineroDisponible - productoSeleccionado.precio;

        if (vuelto < 0)
        {
            Debug.Log("No tienes suficiente dinero para comprar este producto.");
            return;
        }

        dineroDisponible = vuelto;
        textoDineroDisponible.text = $"Dinero disponible: ${dineroDisponible}";
        textoVuelto.text = $"Vuelto: ${vuelto}";
        panelVuelto.SetActive(true);

        if (audioManager != null)
        {
            audioManager.PlayVueltoSound(vuelto);
        }

        if (!productoSeleccionado.esSaludable)
        {
            Debug.Log("¡Perdiste! Elegiste un producto no saludable.");
            panelIncorrecto.SetActive(true);

            if (audioManager != null)
            {
                audioManager.PlayCompraIncorrecta();
            }
        }
        else
        {
            Debug.Log("¡Producto comprado! Producto saludable.");

            if (panelCorrecto != null)
                panelCorrecto.SetActive(true);

            if (audioManager != null)
            {
                audioManager.PlayCompraExitosa();
            }

            StartCoroutine(CerrarKioskoPanel());
        }
    }

    private IEnumerator CerrarKioskoPanel()
    {
        EstadoKiosko.minijuegoCompletado = true;

        yield return new WaitForSeconds(tiempoEspera);

        if (panelKioskoPrincipal != null)
        {
            panelKioskoPrincipal.SetActive(false);
        }

        if (audioManager != null)
        {
            audioManager.CerrarKiosko();
            yield return new WaitForSeconds(1.0f);
            audioManager.CambiarMusicaDeFondo("Parque");
        }

        GameManagerP instancia = FindObjectOfType<GameManagerP>();
        if (instancia != null)
        {
            instancia.SumarEstrellaMinijuego();
            Debug.Log("⭐ Estrella sumada correctamente desde el kiosko.");
        }
        else
        {
            Debug.LogError("❌ No se encontró GameManagerP en la escena. No se pudo sumar estrella.");
        }

        if (panelKioskoPrincipal != null)
        {
            panelKioskoPrincipal.SetActive(false);
        }
        else
        {
            Debug.LogWarning("⚠️ No se asignó el panelKioskoPrincipal en el Inspector.");
        }

        EstadoMinijuego.minijuegoKioskoSaludableCompletado = true;
    }

    public void ReiniciarKiosko()
    {
        if (audioManager != null)
        {
            audioManager.PlayButtonClickSound();
        }

        dineroDisponible = 4000;
        productoSeleccionado = null;
        ActualizarUIInicial();
    }
}
