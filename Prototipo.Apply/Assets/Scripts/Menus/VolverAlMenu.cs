using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VolverAlMenu : MonoBehaviour
{
    public void VolverAlMenuPrincipal()
    {
        Time.timeScale = 1f; //Reiniciamos la ejecución
        ReiniciarEstadosGlobales();
        SceneManager.LoadScene("MainMenu"); 
    }

    private void ReiniciarEstadosGlobales()
    {
        if (GameManager.Instancia != null)
            Destroy(GameManager.Instancia.gameObject);

        if (GameManagerP.Instancia != null)
            Destroy(GameManagerP.Instancia.gameObject);

        if (Timer.Instancia != null)
            Destroy(Timer.Instancia.gameObject);

        if (AudioManager.Instancia != null)
            Destroy(AudioManager.Instancia.gameObject);
        if(KioskoSaludableManager.Instancia != null)
            Destroy(KioskoSaludableManager.Instancia.gameObject);

        if (NombreIngresoManager.Instancia != null) 
            Destroy(NombreIngresoManager.Instancia.gameObject);

        EstadoKiosko.tareasPrevias = 0;
        EstadoMinijuego.minijuegoKioskoSaludableCompletado = false;
        EstadoMinijuego.minijuegoLapiceraCompletado = false;

        // También podés reiniciar otros estados si los creaste
    }

}
