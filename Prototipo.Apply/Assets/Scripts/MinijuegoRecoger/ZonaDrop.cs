using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ZonaDrop : MonoBehaviour, IDropHandler
{
    [SerializeField] private string letraEsperada; // La letra correcta para esta zona
    [SerializeField] private string letraActual = ""; // Se actualiza con la letra soltada

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.transform.SetParent(transform); //Emparentamos el objeto 
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = Vector2.zero; 

            letraActual = eventData.pointerDrag.name; // Asegúrate de que el nombre sea "L", "A", etc.
        }
    }

    public string LetraActual { get => letraActual; set => letraActual = value; }
}
