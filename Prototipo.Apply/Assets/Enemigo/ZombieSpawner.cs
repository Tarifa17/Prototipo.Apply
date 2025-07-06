using System.Collections;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [Header("Configuración de Spawn")]
    public GameObject zombiePrefab;          // Prefab del zombie
    public Transform spawnPointLeft;         // Punto de spawn izquierdo
    public Transform spawnPointRight;        // Punto de spawn derecho
    public float spawnInterval = 15f;        // Intervalo de spawn en segundos

    [Header("Configuración del Zombie")]
    public Sprite zombieSprite;              // Sprite del zombie
    public float zombieSpeed = 2f;           // Velocidad del zombie

    private void Start()
    {
        // Iniciar el spawn automático
        StartCoroutine(SpawnZombieRoutine());
    }

    private IEnumerator SpawnZombieRoutine()
    {
        while (true)
        {
            // Esperar el intervalo de spawn
            yield return new WaitForSeconds(spawnInterval);

            // Spawn de zombie aleatorio
            SpawnRandomZombie();
        }
    }

    private void SpawnRandomZombie()
    {
        // Elegir aleatoriamente entre los dos puntos de spawn
        Transform spawnPoint = Random.Range(0, 2) == 0 ? spawnPointLeft : spawnPointRight;

        // Crear el zombie en la posición elegida
        GameObject newZombie = Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);

        // Configurar el sprite del zombie
        SetupZombieSprite(newZombie);

        // Configurar comportamiento del zombie
        SetupZombieBehavior(newZombie);
    }

    private void SetupZombieSprite(GameObject zombie)
    {
        // Obtener el SpriteRenderer del zombie
        SpriteRenderer spriteRenderer = zombie.GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            // Si no tiene SpriteRenderer, agregarlo
            spriteRenderer = zombie.AddComponent<SpriteRenderer>();
        }

        // Asignar el sprite
        if (zombieSprite != null)
        {
            spriteRenderer.sprite = zombieSprite;
        }
    }

    private void SetupZombieBehavior(GameObject zombie)
    {
        // Obtener o agregar el script de comportamiento del zombie
        Comportamiento actuar = zombie.GetComponent<Comportamiento>();

        if (actuar == null)
        {
            actuar = zombie.AddComponent<Comportamiento>();
        }

        // Configurar la velocidad
        actuar.speed = zombieSpeed;
    }

    // Método para spawn manual (opcional)
    public void SpawnZombieManual()
    {
        SpawnRandomZombie();
    }
}