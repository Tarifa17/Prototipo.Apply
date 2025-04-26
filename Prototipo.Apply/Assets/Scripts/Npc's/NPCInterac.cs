using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class NPCInterac : MonoBehaviour
{
    [SerializeField] private GameObject globoDialogo; //Objeto del globo de dialogo
    [SerializeField] private string mensaje = "¡Necesito ayuda! Debo encontrar mi LAPICERA."; //Gaurdamos el mensaje en una variable
    private bool jugadorCerca = false; //Variable para detectar si el jugador esta cerca del npc y pueda interactuar

    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E)) //
        {
            globoDialogo.SetActive(true); //Activamos el globo de dialogo
            globoDialogo.GetComponentInChildren<TextMeshProUGUI>().text = mensaje; //Emparentamos el texto

            // Bloqueamos movimiento del jugador si hace falta
            //GameManager.Instancia.BloquearJugador(true);

            StartCoroutine(IniciarMinijuego());
        }
    }
    private IEnumerator IniciarMinijuego()
    {
        yield return new WaitForSeconds(5f); //Esperamos 5 seg
                                           
        GameObject.FindWithTag("Player").GetComponent<Controller>().enabled = false;

        SceneManager.LoadScene("MinijuegoOrdenar", LoadSceneMode.Additive); //Llamamos a la escena del minijuego
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) jugadorCerca = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) jugadorCerca = false;
    }

}
