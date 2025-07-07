using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public float tiempoRestante = 120f; // 2 minutos
    public TMP_Text textoTemporizador;
    public GameObject victoriaUI;

    public static bool tiempoFinalizado = false;

    private void Start()
    {
        tiempoFinalizado = false;
        if (victoriaUI != null)
            victoriaUI.SetActive(false);
    }

    private void Update()
    {
        if (tiempoFinalizado) return;

        tiempoRestante -= Time.deltaTime;
        tiempoRestante = Mathf.Max(tiempoRestante, 0);

        int minutos = Mathf.FloorToInt(tiempoRestante / 60f);
        int segundos = Mathf.FloorToInt(tiempoRestante % 60f);
        textoTemporizador.text = $"{minutos:00}:{segundos:00}";

        if (tiempoRestante <= 0f)
        {
            tiempoFinalizado = true;
            DetenerTodo();
        }
    }

    private void DetenerTodo()
    {
        // Mostrar UI de victoria
        if (victoriaUI != null)
            victoriaUI.SetActive(true);

        // Detener todos los enemigos
        Enemigo[] enemigos = FindObjectsOfType<Enemigo>();
        foreach (Enemigo e in enemigos)
        {
            e.enabled = false;
        }

        // Detener todos los spawners
        Spawner[] spawners = FindObjectsOfType<Spawner>();
        foreach (Spawner s in spawners)
        {
            s.enabled = false;
        }
    }
}
