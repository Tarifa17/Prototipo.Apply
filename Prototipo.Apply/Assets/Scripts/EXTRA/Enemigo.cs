using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemigo : MonoBehaviour
{
    [SerializeField] private float velocidad = 8f;
    public float erraticidad = 0.5f;
    public int vida = 3;

    public Transform objetivo;
    private Rigidbody2D rb;
    private Vector2 direccionErratica = Vector2.zero;
    private bool jugadorVivo = true;

    private void Start()
    {
        objetivo = GameObject.FindGameObjectWithTag("Player")?.transform;
        rb = GetComponent<Rigidbody2D>();

        if (objetivo != null)
        {
            InvokeRepeating(nameof(ActualizarDesviacion), 0f, 0.3f);
        }

        PlayerHealth.OnJugadorMuerto += DetenerMovimiento;
    }

    private void OnDestroy()
    {
        PlayerHealth.OnJugadorMuerto -= DetenerMovimiento;
    }

    private void FixedUpdate()
    {
        if (!jugadorVivo || objetivo == null || TimerManager.tiempoFinalizado) return;

        Vector2 direccion = (objetivo.position - transform.position).normalized;
        Vector2 direccionFinal = (direccion + direccionErratica).normalized;
        Vector2 destino = rb.position + direccionFinal * velocidad * Time.fixedDeltaTime;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direccionFinal, 0.5f, LayerMask.GetMask("Obstaculo"));

        if (!hit)
        {
            rb.MovePosition(destino);
        }
        else
        {
            Vector2 lateral = Vector2.Perpendicular(direccionFinal).normalized;
            rb.MovePosition(rb.position + lateral * velocidad * 0.5f * Time.fixedDeltaTime);
        }
    }

    private void ActualizarDesviacion()
    {
        if (!jugadorVivo || objetivo == null) return;

        float randomSign = Random.value > 0.5f ? 1f : -1f;
        Vector2 perpendicular = Vector2.Perpendicular((objetivo.position - transform.position).normalized);
        direccionErratica = perpendicular * erraticidad * randomSign;
    }

    public void RecibirDisparo()
    {
        vida--;

        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!jugadorVivo) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth ph = collision.gameObject.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.RecibirDaño(10);
            }
        }
    }

    private void DetenerMovimiento()
    {
        jugadorVivo = false;
    }
}
