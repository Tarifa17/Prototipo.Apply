using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Contenedor : MonoBehaviour
{
    [SerializeField] private TipoTarea tipo; // Tipo de objeto aceptado por este contenedor.
    private GameObject objetoActual = null; // Objeto actualmente dentro del contenedor.
    private GameManager gameManager; // Referencia al GameManager para registrar tareas.

    [SerializeField] private float rechazoVelocidad = 0.5f; // Velocidad con que se rechazan objetos incorrectos.
    [SerializeField] private float tiempoDetenerRechazo = 1f; // Tiempo antes de detener un objeto rechazado.

    // Conjunto para rastrear los objetos ya rechazados, evitando rechazos repetidos.
    private HashSet<GameObject> objetosRechazados = new HashSet<GameObject>();

    private void Start()
    {
        // Encuentra el GameManager al inicio del juego.
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Intentamos obtener información del tipo de objeto que entró.
        ObjetosEnum objeto = other.GetComponent<ObjetosEnum>();

        if (objeto != null)
        {
            // ✅ Si el objeto es del tipo correcto y no hay otro objeto en el contenedor.
            if (objeto.Tipo == tipo && objetoActual == null)
            {
                objetoActual = other.gameObject; // Guardamos referencia al objeto.
                gameManager.RegistrarTarea(this); // Registramos la tarea como completada.
            }
            // ❌ Si el objeto es del tipo incorrecto y no ha sido rechazado recientemente.
            else if (objeto.Tipo != tipo && !objetosRechazados.Contains(other.gameObject))
            {
                objetosRechazados.Add(other.gameObject); // Marcamos el objeto como rechazado.

                // 1. Si el objeto está siendo agarrado, lo soltamos.
                AgarrarObjeto agarrarScript = FindObjectOfType<AgarrarObjeto>();
                if (agarrarScript != null)
                {
                    agarrarScript.ForzarSoltar(other.gameObject, false); // No reproducir sonido de "drop".
                }

                // 2. Reproducimos un sonido de error (solo una vez por objeto).
                if (AudioManager.Instancia != null)
                {
                    AudioManager.Instancia.PlayErrorSound();
                }

                // 3. Rechazamos físicamente el objeto.
                Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.isKinematic = false; // Permitimos que el objeto sea afectado por la física.

                    // Calculamos la dirección en la que rechazaremos el objeto.
                    Vector2 direccionRechazo = (other.transform.position - transform.position).normalized;
                    rb.velocity = direccionRechazo * rechazoVelocidad; // Aplicamos una velocidad al objeto.

                    // Detenemos el movimiento del objeto después de un tiempo.
                    StartCoroutine(DetenerObjeto(rb, tiempoDetenerRechazo));
                }

                // 4. Permitimos que el objeto sea rechazado nuevamente después de un tiempo.
                StartCoroutine(RemoverRechazoTemporal(other.gameObject));

                Debug.Log($"❌ Objeto incorrecto: {objeto.Tipo} no va en {tipo}");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Si el objeto actual sale del contenedor, lo removemos.
        if (other.gameObject == objetoActual)
        {
            objetoActual = null; // Quitamos la referencia al objeto.
            gameManager.RemoverTarea(this); // Informamos al GameManager que la tarea ya no está completada.
        }
    }

    // Corrutina que detiene el movimiento de un objeto rechazado después de cierto tiempo.
    private IEnumerator DetenerObjeto(Rigidbody2D rb, float delay)
    {
        yield return new WaitForSeconds(delay); // Esperamos el tiempo indicado.

        if (rb != null)
        {
            rb.velocity = Vector2.zero; // Detenemos el movimiento lineal.
            rb.angularVelocity = 0f; // Detenemos el movimiento rotacional.
        }
    }

    // Corrutina que permite rechazar un objeto nuevamente después de un tiempo.
    private IEnumerator RemoverRechazoTemporal(GameObject obj)
    {
        yield return new WaitForSeconds(1f); // Esperamos 1 segundo.
        objetosRechazados.Remove(obj); // Permitimos que el objeto pueda ser rechazado otra vez.
    }
}