using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject panelDerrota;

    private void OnEnable()
    {
        PlayerHealth.OnJugadorMuerto += ActivarGameOver;
    }

    private void OnDisable()
    {
        PlayerHealth.OnJugadorMuerto -= ActivarGameOver;
    }

    private void ActivarGameOver()
    {
        Time.timeScale = 0f;  // Pausa el juego

        if (panelDerrota != null)
        {
            panelDerrota.SetActive(true);
        }
        else
        {
            Debug.LogWarning("⚠️ PanelDerrota no está asignado en el inspector.");
        }
        PlayerDisparo disparo = FindObjectOfType<PlayerDisparo>();
        if (disparo != null)
            disparo.enabled = false;
    }
}
