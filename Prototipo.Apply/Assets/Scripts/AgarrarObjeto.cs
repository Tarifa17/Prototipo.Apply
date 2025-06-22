using UnityEngine;

public class AgarrarObjetoUniversal : MonoBehaviour
{
    [SerializeField] private GameObject puntoAgarre; // Punto donde se agarra el objeto
    private GameObject objetoAgarrado = null;

    private MonoBehaviour gameManager; // Puede ser GameManager o GameManagerP

    private void Start()
    {
        // Detectar si existe un GameManager común o variante
        gameManager = (MonoBehaviour)FindObjectOfType<GameManager>() ?? (MonoBehaviour)FindObjectOfType<GameManagerP>();
    }

    void Update()
    {
        if (JuegoGanado()) return;

        if (objetoAgarrado != null && Input.GetKey("q"))
        {
            Rigidbody2D rb = objetoAgarrado.GetComponent<Rigidbody2D>();
            if (rb != null) rb.isKinematic = false;

            objetoAgarrado.transform.SetParent(null);
            objetoAgarrado = null;

            AudioManager.Instancia?.PlayDropSound();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (JuegoGanado()) return;

        if (other.CompareTag("Objetos") && Input.GetKey("e") && objetoAgarrado == null)
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null) rb.isKinematic = true;

            other.transform.position = puntoAgarre.transform.position;
            other.transform.SetParent(puntoAgarre.transform);
            objetoAgarrado = other.gameObject;

            AudioManager.Instancia?.PlayGrabSound();

            ObjetosEnum tipoObjeto = objetoAgarrado.GetComponent<ObjetosEnum>();
            if (tipoObjeto != null)
            {
                AudioClip clip = AudioManager.Instancia?.ObtenerClipPorTipoTarea(tipoObjeto.Tipo);
                if (clip != null)
                {
                    AudioManager.Instancia.PlaySoundConFade(clip);
                }
            }
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
