using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditosManager : MonoBehaviour
{
    [SerializeField] private GameObject panelCreditos;

    public void MostrarCreditos()
    {
        if (panelCreditos != null)
            panelCreditos.SetActive(true);
    }

    public void OcultarCreditos()
    {
        if (panelCreditos != null)
            panelCreditos.SetActive(false);
    }
}
