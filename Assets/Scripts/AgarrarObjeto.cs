using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgarrarObjeto : MonoBehaviour
{
    [SerializeField] private GameObject puntoAgarre;
    private GameObject objetoAgarrado = null;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (gameManager.JuegoGanado) return;

        if (objetoAgarrado != null)
        {
            if (Input.GetKeyDown("q")) // Use GetKeyDown for one-time press
            {
                SoltarObjeto();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (gameManager.JuegoGanado) return;

        if (other.gameObject.CompareTag("Objetos"))
        {
            if (Input.GetKeyDown("e") && objetoAgarrado == null)
            {
                Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.isKinematic = true;
                }

                other.transform.position = puntoAgarre.transform.position;
                other.transform.SetParent(puntoAgarre.transform);
                objetoAgarrado = other.gameObject;

                AudioManager.Instancia.PlayGrabSound();
            }
        }
    }

    // ✅ Used by external scripts (like Contenedor) to release held object
    public void ForzarSoltar(GameObject obj, bool playDropSound = true)
    {
        if (objetoAgarrado == obj)
        {
            SoltarObjeto(playDropSound); // Llamar a SoltarObjeto con control del sonido
        }
    }

    private void SoltarObjeto(bool playDropSound = true)
    {
        if (objetoAgarrado == null) return;

        Rigidbody2D rb = objetoAgarrado.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.isKinematic = false;
        }

        objetoAgarrado.transform.SetParent(null);
        objetoAgarrado = null;

        // Reproducir sonido de drop solo si está permitido
        if (playDropSound)
        {
            AudioManager.Instancia.PlayDropSound();
        }
    }
}