using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxVida = 100;
    public int vidaActual;
    public static event System.Action OnJugadorMuerto;


    [SerializeField] private Slider barraVida;  // Arrastrar el slider de la UI aquí

    private void Start()
    {
        vidaActual = maxVida;
        ActualizarBarraVida();
    }

    // Método para recibir daño
    public void RecibirDaño(int cantidad)
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
        Debug.Log("Jugador murió");
        OnJugadorMuerto?.Invoke();
        // Aquí poné la lógica de game over, reiniciar nivel, etc.
    }
}
