using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MinijuegoManager : MonoBehaviour
{
    [SerializeField] private List<Dropeable> espaciosVacios; //Referencias a los espacios vacíos
    [SerializeField] private Button botonValidar;
    [SerializeField] private GameObject panelLapiceras;
    [SerializeField] private GameObject panelCorrecto;
    [SerializeField] private GameObject panelIncorrecto;
    [SerializeField] private Button botonReintentar;
    [SerializeField] private string escenaPrincipal = "Escuela";
    [SerializeField] private float tiempoEspera = 5f;

    private string palabraCorrecta = "LAPICERA";
    private bool lapiceraSeleccionada = false;

    private void Start()
    {
        botonValidar.onClick.AddListener(ValidarPalabra); //El evento al hacer click en el boton validar es asociado a la funcion Validar Palabra
        panelLapiceras.SetActive(false);
        panelCorrecto.SetActive(false);
        panelIncorrecto.SetActive(false);
        if (botonReintentar != null)
            botonReintentar.onClick.AddListener(Reintentar);
    }

    private void ValidarPalabra()
    {
        string palabraFormada = "";

        foreach (Dropeable espacio in espaciosVacios) //Recorremos la lista de espacios vacios
        {
            if (espacio.transform.childCount > 0)
            {
                palabraFormada += espacio.transform.GetChild(0).name[0]; //Como nuestros prefabs se llaman "L, A , P..." le asignamos estos nombres a cada variable
            }
            else
            {
                Debug.Log("No todas las letras fueron colocadas");
                return;
            }
        }

        if (palabraFormada == palabraCorrecta)
        {
            Debug.Log("¡Palabra correcta!");
            panelLapiceras.SetActive(true);
        }
        else
        {
            Debug.Log("Palabra incorrecta");
        }
    }
    public void SeleccionarLapicera(string color)
    {
        if (lapiceraSeleccionada) return;
        lapiceraSeleccionada = true;

        if (color == "Azul")
        {
            Debug.Log("¡Correcto! Elegiste la lapicera azul.");
            panelCorrecto.SetActive(true);
            //GameManager.Instancia.RegistrarTarea(null);
            EstadoMinijuego.minijuegoLapiceraCompletado = true;
            StartCoroutine(VolverAEscena());
        }
        else
        {
            Debug.Log("Lapicera incorrecta. El NPC quería la azul.");
            panelIncorrecto.SetActive(true);
        }
        
    }
    private void Reintentar()
    {
        SceneManager.LoadScene(escenaPrincipal);
    }


    private IEnumerator VolverAEscena()
    {
        yield return new WaitForSeconds(tiempoEspera);
        SceneManager.LoadScene(escenaPrincipal);
    }

}
