using UnityEngine;
using System.Collections.Generic;

public class AgarrarObjetoUniversal : MonoBehaviour
{
    [SerializeField] private GameObject puntoAgarre;
    private GameObject objetoAgarrado = null;
    private List<GameObject> objetosDetectados = new List<GameObject>();

    private MonoBehaviour gameManager;

    private void Start()
    {
        gameManager = (MonoBehaviour)FindObjectOfType<GameManager>() ?? (MonoBehaviour)FindObjectOfType<GameManagerP>();
    }

    private void Update()
    {
        if (JuegoGanado()) return;

        // Soltar objeto
        if (objetoAgarrado != null && Input.GetKeyDown(KeyCode.Q))
        {
            Rigidbody2D rb = objetoAgarrado.GetComponent<Rigidbody2D>();
            if (rb != null) rb.isKinematic = false;

            objetoAgarrado.transform.SetParent(null);
            objetoAgarrado = null;

            AudioManager.Instancia?.PlayDropSound();
            FlechaDestinoManager.Instancia?.OcultarFlechaDestino();
        }

        // Agarrar objeto
        if (objetoAgarrado == null && objetosDetectados.Count > 0 && Input.GetKeyDown(KeyCode.E))
        {
            GameObject objeto = objetosDetectados[0]; // Siempre tomamos el primero válido
            if (objeto == null) return;

            Rigidbody2D rb = objeto.GetComponent<Rigidbody2D>();
            if (rb != null) rb.isKinematic = true;

            objeto.transform.position = puntoAgarre.transform.position;
            objeto.transform.SetParent(puntoAgarre.transform);
            objetoAgarrado = objeto;

            AudioManager.Instancia?.PlayGrabSound();

            ObjetosEnum tipoObjeto = objetoAgarrado.GetComponent<ObjetosEnum>();
            if (tipoObjeto != null)
            {
                AudioClip clip = AudioManager.Instancia?.ObtenerClipPorTipoTarea(tipoObjeto.Tipo);
                if (clip != null)
                {
                    AudioManager.Instancia.ReproducirVozObjeto(clip);
                }

                FlechaDestinoManager.Instancia?.MostrarFlechaDestino(tipoObjeto.Tipo);
            }

            objetosDetectados.Remove(objeto); // Ya lo estamos usando
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Objetos") && !objetosDetectados.Contains(other.gameObject))
        {
            objetosDetectados.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (objetosDetectados.Contains(other.gameObject))
        {
            objetosDetectados.Remove(other.gameObject);
        }
    }

    private bool JuegoGanado()
    {
        if (gameManager == null) return false;

        if (gameManager is GameManager gm) return gm.JuegoGanado;
        if (gameManager is GameManagerP gmp) return gmp.JuegoGanado;

        return false;
    }
}
