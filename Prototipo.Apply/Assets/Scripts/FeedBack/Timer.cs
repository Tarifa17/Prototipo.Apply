using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer Instancia { get; private set; }

    private float tiempoTotal = 0f;
    private bool estaContando = true;

    public float TiempoTotal => tiempoTotal; // Propiedad pública para consultar el tiempo

    private void Awake()
    {
        // Singleton
        if (Instancia != null && Instancia != this)
        {
            Destroy(gameObject);
            return;
        }

        Instancia = this;
        DontDestroyOnLoad(gameObject); // Persistir entre escenas
    }

    private void Update()
    {
        if (estaContando)
            tiempoTotal += Time.deltaTime;
    }

    public void DetenerTimer()
    {
        estaContando = false;
        Debug.Log($"Timer detenido: {tiempoTotal} segundos.");
    }

    public void ReiniciarTimer()
    {
        tiempoTotal = 0f;
        estaContando = true;
    }
}
