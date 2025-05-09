using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class KioskoSaludableManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI textoDineroDisponible; // Texto para el dinero disponible
    [SerializeField] private TextMeshProUGUI textoPrecioProducto; // Texto para el precio del producto seleccionado
    [SerializeField] private GameObject panelVuelto; // Panel para mostrar el vuelto
    [SerializeField] private TextMeshProUGUI textoVuelto; // Texto del vuelto
    [SerializeField] private GameObject panelIncorrecto; // Panel para mostrar el mensaje de derrota
    [SerializeField] private GameObject panelCorrecto; // Panel para mostrar el mensaje de victoria
    [SerializeField] private Button botonComprar; // Botón de comprar
    [SerializeField] private GameObject contenedorVacios; // Contenedor para mostrar cuando no hay selección
    [SerializeField] private GameObject panelKioskoPrincipal;

    [Header("Productos")]
    [SerializeField] private ProductoUI[] productosUI; // Referencias a los componentes de UI de productos

    [Header("Configuraciones")]
    //[SerializeField] private string escenaPrincipal = "Parque"; // Escena a la que se regresa después del minijuego
    [SerializeField] private float tiempoEspera = 5f; // Tiempo de espera antes de salir del minijuego

    private int dineroDisponible = 4000; // Dinero inicial del jugador
    private Producto productoSeleccionado; // Producto actualmente seleccionado
    private KioskoAudioManager audioManager; // Referencia al audio manager

    private void Start()
    {
        // Obtener referencia al audio manager
        audioManager = KioskoAudioManager.Instance;
        if (audioManager == null)
        {
            Debug.LogWarning("KioskoAudioManager no encontrado");
        }

        // Inicializar la UI
        ActualizarUIInicial();

        // Asignar evento al botón de comprar
        botonComprar.onClick.AddListener(ComprarProducto);

        // Si no hay productos asignados, buscarlos automáticamente
        if (productosUI == null || productosUI.Length == 0)
        {
            productosUI = FindObjectsOfType<ProductoUI>();
        }
    }
    private void OnEnable()
    {
        // Al activar el panel del kiosko, asegurarse de que el audio manager también se active
        if (audioManager != null)
        {
            // La llamada a AbrirKiosko inicia la transición de música
            audioManager.AbrirKiosko();
        }
    }

    private void ActualizarUIInicial()
    {
        textoDineroDisponible.text = $"Dinero disponible: ${dineroDisponible}";
        textoPrecioProducto.text = "Precio: $0";
        panelVuelto.SetActive(false);
        panelIncorrecto.SetActive(false);

        if (panelCorrecto != null)
            panelCorrecto.SetActive(false);

        if (contenedorVacios != null)
            contenedorVacios.SetActive(true);
    }

    public void SeleccionarProducto(Producto producto)
    {
        productoSeleccionado = producto;
        textoPrecioProducto.text = $"Precio: ${producto.precio}";

        // Reproducir sonido de selección de producto
        if (audioManager != null)
        {
            audioManager.PlayProductoSeleccionado();
        }

        // Ocultar el contenedor de vacios al seleccionar un producto
        if (contenedorVacios != null)
            contenedorVacios.SetActive(false);
    }

    private void ComprarProducto()
    {
        // Reproducir sonido de clic en botón
        if (audioManager != null)
        {
            audioManager.PlayBotonClick();
        }

        if (productoSeleccionado == null)
        {
            Debug.Log("No has seleccionado ningún producto.");
            return;
        }

        // Calcular el vuelto independientemente de si es saludable o no
        int vuelto = dineroDisponible - productoSeleccionado.precio;

        if (vuelto < 0)
        {
            Debug.Log("No tienes suficiente dinero para comprar este producto.");
            return;
        }

        // Actualizar el dinero disponible y mostrar vuelto en ambos casos
        dineroDisponible = vuelto;
        textoDineroDisponible.text = $"Dinero disponible: ${dineroDisponible}";
        textoVuelto.text = $"Vuelto: ${vuelto}";
        panelVuelto.SetActive(true);

        // Reproducir sonido de vuelto
        if (audioManager != null)
        {
            audioManager.PlayVueltoSound(vuelto);
        }

        if (!productoSeleccionado.esSaludable)
        {
            // Si el producto no es saludable, mostrar mensaje de derrota
            Debug.Log("¡Perdiste! Elegiste un producto no saludable.");
            panelIncorrecto.SetActive(true);
            //panelCorrecto.SetActive(true);
            //StartCoroutine(CerrarKioskoPanel());

            // Reproducir sonido de compra incorrecta
            if (audioManager != null)
            {
                audioManager.PlayCompraIncorrecta();
            }

            // NO inicia la corrutina para volver a la escena
            // El jugador puede intentar de nuevo
        }
        else
        {
            // Si el producto es saludable, mostrar mensaje de victoria
            Debug.Log($"¡Producto comprado! Producto saludable.");

            if (panelCorrecto != null)
                panelCorrecto.SetActive(true);

            // Reproducir sonido de compra exitosa
            if (audioManager != null)
            {
                audioManager.PlayCompraExitosa();
            }

            // Solo cambia de escena cuando el producto es saludable
            StartCoroutine(CerrarKioskoPanel());

        }
    }

    private IEnumerator CerrarKioskoPanel()
    {
        yield return new WaitForSeconds(tiempoEspera);

        // Marcar que el minijuego fue completado
        EstadoKiosko.minijuegoCompletado = true;

        // Buscar instancia del GameManagerP
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

        // Hacer fade out de la música del kiosko antes de cerrar el panel
        if (audioManager != null)
        {
            audioManager.CerrarKiosko();
            // Esperar un poco para que el fade out termine
            yield return new WaitForSeconds(1.0f);
        }

        // Cerrar el panel contenedor del kiosko
        if (panelKioskoPrincipal != null)
        {
            panelKioskoPrincipal.SetActive(false);
        }
        else
        {
            Debug.LogWarning("⚠️ No se asignó el panelKioskoPrincipal en el Inspector.");
        }
    }



    // Método para reiniciar el kiosko (puede ser llamado desde un botón de reinicio)
    public void ReiniciarKiosko()
    {
        // Reproducir sonido de clic en botón
        if (audioManager != null)
        {
            audioManager.PlayBotonClick();
        }

        ActualizarUIInicial();
        productoSeleccionado = null;
    }
}