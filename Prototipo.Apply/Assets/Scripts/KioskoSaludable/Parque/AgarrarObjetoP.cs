using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgarrarObjetoP : MonoBehaviour
{
    [SerializeField] private GameObject puntoAgarre; // Punto de agarre del objeto
    private GameObject objetoAgarrado = null; // Objeto actualmente agarrado
    private GameManagerP gameManager; // Referencia al GameManager

    private void Start()
    {
        gameManager = FindObjectOfType<GameManagerP>();
    }

    void Update()
    {
        if (gameManager.JuegoGanado) return;

        if (objetoAgarrado != null)
        {
            if (Input.GetKey("q")) // Soltar con "Q"
            {
                Rigidbody2D rb = objetoAgarrado.GetComponent<Rigidbody2D>();
                if (rb != null) rb.isKinematic = false;

                objetoAgarrado.transform.SetParent(null);
                objetoAgarrado = null;

                AudioManager.Instancia.PlayDropSound();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (gameManager.JuegoGanado) return;

        if (other.gameObject.CompareTag("Objetos") && Input.GetKey("e") && objetoAgarrado == null)
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null) rb.isKinematic = true;

            other.transform.position = puntoAgarre.transform.position;
            other.transform.SetParent(puntoAgarre.transform);
            objetoAgarrado = other.gameObject;

            AudioManager.Instancia.PlayGrabSound();

            // Buscar el enum del objeto
            ObjetosEnum tipoObjeto = other.GetComponent<ObjetosEnum>();
            if (tipoObjeto != null)
            {
                AudioClip consejo = AudioManager.Instancia.ObtenerClipPorTipoTarea(tipoObjeto.Tipo);
                if (consejo != null)
                {
                    AudioManager.Instancia.PlaySoundConFade(consejo); // Consejo con fade de música
                }
            }
        }
    }
}
