using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditosCascada : MonoBehaviour
{
    [SerializeField] private RectTransform contenedorTexto;
    [SerializeField] private float velocidadScroll = 50f;
    [SerializeField] private float alturaFinal = 800f;

    private bool activado = false;
    private float alturaInicial;

    private void Start()
    {
        alturaInicial = contenedorTexto.anchoredPosition.y;
        IniciarScroll();
    }

    public void IniciarScroll()
    {
        activado = true;
        contenedorTexto.anchoredPosition = new Vector2(0, alturaInicial);
    }

    public void DetenerScroll()
    {
        activado = false;
    }

    private void Update()
    {
        if (activado)
        {
            contenedorTexto.anchoredPosition += new Vector2(0, velocidadScroll * Time.deltaTime);

            if (contenedorTexto.anchoredPosition.y >= alturaFinal)
            {
                DetenerScroll();
            }
        }
    }
}
