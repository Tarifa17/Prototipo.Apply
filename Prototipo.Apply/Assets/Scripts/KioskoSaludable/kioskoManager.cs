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
    [SerializeField] private Button botonComprar; // Bot�n de comprar
    [SerializeField] private GameObject contenedorVacios; // Contenedor para mostrar cuando no hay selecci�n

    [Header("Productos")]
    [SerializeField] private ProductoUI[] productosUI; // Referencias a los componentes de UI de productos

    [Header("Configuraciones")]
    [SerializeField] private string escenaPrincipal = "Parque"; // Escena a la que se regresa despu�s del minijuego
    [SerializeField] private float tiempoEspera = 5f; // Tiempo de espera antes de salir del minijuego

    private int dineroDisponible = 4000; // Dinero inicial del jugador
    private Producto productoSeleccionado; // Producto actualmente seleccionado

    private void Start()
    {
        // Inicializar la UI
        ActualizarUIInicial();

        // Asignar evento al bot�n de comprar
        botonComprar.onClick.AddListener(ComprarProducto);

        // Si no hay productos asignados, buscarlos autom�ticamente
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

        if (panelCorrecto != null)
            panelCorrecto.SetActive(false);

        if (contenedorVacios != null)
            contenedorVacios.SetActive(true);
    }

    public void SeleccionarProducto(Producto producto)
    {
        productoSeleccionado = producto;
        textoPrecioProducto.text = $"Precio: ${producto.precio}";

        // Ocultar el contenedor de vacios al seleccionar un producto
        if (contenedorVacios != null)
            contenedorVacios.SetActive(false);
    }

    private void ComprarProducto()
    {
        if (productoSeleccionado == null)
        {
            Debug.Log("No has seleccionado ning�n producto.");
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

        if (!productoSeleccionado.esSaludable)
        {
            // Si el producto no es saludable, mostrar mensaje de derrota
            Debug.Log("�Perdiste! Elegiste un producto no saludable.");
            panelIncorrecto.SetActive(true);
            // NO inicia la corrutina para volver a la escena
            // El jugador puede intentar de nuevo
        }
        else
        {
            // Si el producto es saludable, mostrar mensaje de victoria
            Debug.Log($"�Producto comprado! Producto saludable.");

            if (panelCorrecto != null)
                panelCorrecto.SetActive(true);

            // Solo cambia de escena cuando el producto es saludable
            StartCoroutine(VolverAEscena());
        }
    }

    private IEnumerator VolverAEscena()
    {
        yield return new WaitForSeconds(tiempoEspera);
        SceneManager.LoadScene(escenaPrincipal);
    }

    // M�todo para reiniciar el kiosko (puede ser llamado desde un bot�n de reinicio)
    public void ReiniciarKiosko()
    {
        ActualizarUIInicial();
        productoSeleccionado = null;
    }
}