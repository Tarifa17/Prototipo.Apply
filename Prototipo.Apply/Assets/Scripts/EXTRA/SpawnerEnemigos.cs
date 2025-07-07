using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefabEnemigo;   // Prefab del enemigo
    public Transform jugador;          // Referencia al jugador
    public float tiempoEntreSpawns = 3f;

    private float timer;
    private bool activo = true;        // Bandera para saber si debe seguir spawneando

    private void Start()
    {
        // Buscar al jugador si no fue asignado desde el inspector
        if (jugador == null)
        {
            jugador = GameObject.FindWithTag("Player")?.transform;
            if (jugador == null)
            {
                Debug.LogError("Jugador no encontrado. Asegurate que el Player tenga tag 'Player'.");
            }
        }

        // Suscribirse al evento de muerte del jugador
        PlayerHealth.OnJugadorMuerto += DesactivarSpawner;
    }

    private void OnDestroy()
    {
        // Siempre desuscribirse al destruir el objeto para evitar errores
        PlayerHealth.OnJugadorMuerto -= DesactivarSpawner;
    }

    private void Update()
    {
        if (!activo) return;  // Si el jugador murió, no hacer nada

        timer += Time.deltaTime;

        if (timer >= tiempoEntreSpawns)
        {
            timer = 0f;
            SpawnEnemigo();
        }
        if (TimerManager.tiempoFinalizado) return;
    }

    private void SpawnEnemigo()
    {
        GameObject nuevoEnemigo = Instantiate(prefabEnemigo, transform.position, Quaternion.identity);

        Enemigo enemigoScript = nuevoEnemigo.GetComponent<Enemigo>();
        if (enemigoScript != null)
        {
            enemigoScript.objetivo = jugador;
        }
        else
        {
            Debug.LogError("El prefab enemigo no tiene el script 'Enemigo'.");
        }
    }

    private void DesactivarSpawner()
    {
        activo = false;
    }
}
