using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Agarrable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private CanvasGroup canvasGroup; //Para controlar las interacciones
    private RectTransform rectTransform; //Variable para modificar la posicion de la letra
    private Transform originalParent; //Posicion original de la letra

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent; //Guardamos la posicion original en el padre para poder volver 
        canvasGroup.blocksRaycasts = false; //Desactivamos los raycast para poder moverlos y volver

        // Reproducir sonido al agarrar
        if (MinijuegoAudio.Instancia != null)
        {
            MinijuegoAudio.Instancia.SonidoAgarrar();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        if (canvas != null)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        } //Arrastramos la letra siguiendo el cursor
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; //Activamos los raycast al soltar la letra y que vuelva a ser tangible

        if (transform.parent == originalParent || transform.parent == null) //Si no lo soltamos en un slot valido vuelve a su posicion original
        {
            transform.SetParent(originalParent);
            rectTransform.anchoredPosition = Vector2.zero;
        }
        else
        {
            // Reproducir sonido al colocar en un slot v�lido
            if (MinijuegoAudio.Instancia != null)
            {
                MinijuegoAudio.Instancia.SonidoColocar();
            }
        }
    }
}
