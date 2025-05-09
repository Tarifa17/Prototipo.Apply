using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dropeable : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0) // Solo acepta una letra
        {
            GameObject letra = eventData.pointerDrag; // Obtenemos el objeto soltado (la letra)
            if (letra != null)
            {
                letra.transform.SetParent(transform); // La nueva posicion de la letra es el slot
                letra.GetComponent<RectTransform>().anchoredPosition = Vector2.zero; // Centramos la letra en su nuevo espacio

                // Reproducir sonido al colocar en un slot válido
                if (MinijuegoAudio.Instancia != null)
                {
                    MinijuegoAudio.Instancia.SonidoColocar();
                }
            }
        }
    }
}