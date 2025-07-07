using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxVida = 100;
    public int vidaActual;
    public static event System.Action OnJugadorMuerto;


    [SerializeField] private Slider barraVida;  // Arrastrar el slider de la UI aqu�

    private void Start()
    {
        vidaActual = maxVida;
        ActualizarBarraVida();
    }

    // M�todo para recibir da�o
    public void RecibirDanio(int cantidad)
    {
        vidaActual -= cantidad;
        vidaActual = Mathf.Clamp(vidaActual, 0, maxVida);
        ActualizarBarraVida();

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    private void ActualizarBarraVida()
    {
        if (barraVida != null)
        {
            barraVida.value = (float)vidaActual / maxVida;
        }
    }

    private void Morir()
    {
        Debug.Log("Jugador muri�");
        OnJugadorMuerto?.Invoke();
        // Aqu� pon� la l�gica de game over, reiniciar nivel, etc.
    }
}
