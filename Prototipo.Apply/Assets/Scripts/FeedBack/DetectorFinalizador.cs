using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorFinalizador : MonoBehaviour
{
    private void Start()
    {
        if (Timer.Instancia != null)
        {
            Timer.Instancia.DetenerTimer();
        }
    }
}
