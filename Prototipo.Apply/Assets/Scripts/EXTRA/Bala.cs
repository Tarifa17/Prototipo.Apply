using UnityEngine;

public class Bala : MonoBehaviour
{
    public float velocidad = 10f;
    private Vector2 direccion;

    // Inicializa la bala con la direcci�n en la que disparar
    public void Inicializar(Vector2 dir)
    {
        direccion = dir.normalized;
        Destroy(gameObject, 3f);
    }

    private void Update()
    {
        // Movimiento en l�nea recta en 2D usando solo x e y
        transform.Translate(direccion * velocidad * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemigo"))
        {
            Enemigo enemigo = other.GetComponent<Enemigo>();
            if (enemigo != null)
            {
                enemigo.RecibirDisparo();
            }
            Destroy(gameObject);  // Destruye la bala al impactar
        }
        else if (other.CompareTag("Pared")) // O cualquier obst�culo
        {
            Destroy(gameObject);
        }
    }
}
