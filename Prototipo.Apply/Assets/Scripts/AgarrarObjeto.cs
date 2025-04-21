using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgarrarObjeto : MonoBehaviour
{
    [SerializeField] private GameObject puntoAgarre; //Variable para el punto de agarre del objeto
    private GameObject objetoAgarrado = null; //Al inicio no hay ningun objeto agarrado
    private GameManager gameManager; //Variable para el gamemanager

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (gameManager.JuegoGanado) return; //Si el juego fue ganado se detiene la logica

        if (objetoAgarrado != null) //Si un objeto esta agarrado
        {
            if (Input.GetKey("q")) //Apretar Q
            {
                Rigidbody2D rb = objetoAgarrado.GetComponent<Rigidbody2D>(); //Asignamos el objeto con su rigidbody
                if (rb != null)
                {
                    rb.isKinematic = false; //Lo dejamos de hacer kinematico
                }

                objetoAgarrado.transform.SetParent(null); //Quitamo el objeto de la mano del jugador
                objetoAgarrado = null;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (gameManager.JuegoGanado) return; //Si el juego fue ganado detiene la logica

        if (other.gameObject.CompareTag("Objetos")) //Busca el el objeto asociado al tag 
        {
            if (Input.GetKey("e") && objetoAgarrado == null) //Al apretar E y si no hay objeto agarrado
            {
                Rigidbody2D rb = other.GetComponent<Rigidbody2D>(); //Asignamos el objeto con su rigidbody
                if (rb != null)
                {
                    rb.isKinematic = true; //El objeto se vuelve kinematico
                }

                other.transform.position = puntoAgarre.transform.position; //El objeto toma la posicion del punto de agarre
                other.transform.SetParent(puntoAgarre.transform); //El objeto se vuelve hijo del punto de agarre
                objetoAgarrado = other.gameObject; //Guardamos el objeto agarrado en una variable
            }
        }
    }
}
