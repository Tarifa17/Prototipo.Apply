using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NpcInteract : MonoBehaviour
{
    [SerializeField] private KeyCode teclaInteraccion = KeyCode.E; // Tecla para interactuar
    [SerializeField] private MinijuegoManager minijuegoManager;
    private bool jugadorCerca = false; // Para detectar si el jugador está cerca

    private void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(teclaInteraccion))
        {
            InteractuarConNpc();
        }
    }

    private void InteractuarConNpc()
    {
        if (EstadoMinijuego.minijuegoLapiceraCompletado)
        {
            Debug.Log("¡Gracias por tu ayuda con la lapicera! Ya la encontré.");
            // Podrías agregar un diálogo más completo si usás sistema de diálogos.
        }
        else
        {
            MostrarDialogo();
            CargarMinijuego();
        }
    }

    private void MostrarDialogo()
    {
        Debug.Log("¡Ayuda, necesito encontrar mi lapicera!"); // Después podemos hacer que salga en pantalla.
    }

    private void CargarMinijuego()
    {
        if (minijuegoManager != null)
        {
            minijuegoManager.ActivarMinijuego(); // Llama al método que activa el panel
        }
        else
        {
            Debug.LogWarning("MinijuegoManager no asignado en el Inspector.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorCerca = true;
            Debug.Log("Presiona 'E' para hablar con el NPC.");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorCerca = false;
        }
    }
}
