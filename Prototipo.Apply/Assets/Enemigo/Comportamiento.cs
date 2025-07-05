using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comportamiento: MonoBehaviour
{
    [Header("Configuraci�n")]
    public float speed = 2f;
    public float health = 100f;

    [Header("Detecci�n")]
    public LayerMask playerLayer;
    public float detectionRange = 5f;

    private Transform target;
    private SpriteRenderer spriteRenderer;
    private bool isMoving = true;

    void Start()
    {
        // Obtener componentes
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Buscar al jugador
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }

        // Configurar collider b�sico si no existe
        if (GetComponent<Collider2D>() == null)
        {
            gameObject.AddComponent<BoxCollider2D>();
        }
    }

    void Update()
    {
        if (isMoving && target != null)
        {
            MoveTowardsTarget();
        }
    }

    private void MoveTowardsTarget()
    {
        // Calcular direcci�n hacia el objetivo
        Vector2 direction = (target.position - transform.position).normalized;

        // Mover hacia el objetivo
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // Voltear sprite seg�n la direcci�n
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = direction.x < 0;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Destruir el zombie
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Detectar colisi�n con el jugador
        if (other.CompareTag("Player"))
        {
            // Aqu� puedes agregar l�gica de da�o al jugador
            Debug.Log("Zombie atac� al jugador!");
        }
    }

    void OnDrawGizmosSelected()
    {
        // Dibujar rango de detecci�n en el editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}