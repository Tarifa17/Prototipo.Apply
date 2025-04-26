using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogoUI : MonoBehaviour
{
    [SerializeField] private GameObject panelDialogo; //Variable para el panel del dialogo
    [SerializeField] private TextMeshProUGUI textoDialogo; //Variable para contener el texto
    [SerializeField] private Image imagenAvatar; //Cara del personaje que habla
    [SerializeField] private Sprite avatarInicial; //Sprite que se muestra al inicio

    [TextArea(2, 4)] //Area del texto
    [SerializeField] private string mensajeInicio = "Aqui va el texto."; //Mensaje q se muestra


    [SerializeField] private float duracionMensaje = 10f; //Duracion del mensaje
    [SerializeField] private AudioClip audioMensajeInicio;


    private void Start()
    {
        MostrarDialogo(mensajeInicio, avatarInicial, duracionMensaje);
    }

    public void MostrarDialogo(string mensaje, Sprite avatar, float duracion)
    {
        panelDialogo.SetActive(true); // Activamos el panel del dialogo
        textoDialogo.text = mensaje;  //El mensaje correspondiente
        imagenAvatar.sprite = avatar; //Imagen del personaje

        Invoke(nameof(OcultarDialogo), duracion); //Mediante invoke llamamos al metodo para ocultar el dialogo, usamos nameof para que no haya errores de tipeo
    }

    private void OcultarDialogo() 
    {
        panelDialogo.SetActive(false); //Desactivamos el panel de dialogo

    }
}
