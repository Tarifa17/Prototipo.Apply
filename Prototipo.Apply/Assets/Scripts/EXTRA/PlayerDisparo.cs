using UnityEngine;

public class PlayerDisparo : MonoBehaviour
{
    public GameObject prefabBala;
    public Transform puntoDisparo;
    public float velocidadBala = 10f;

    public AudioClip sonidoDisparo; // Sonido del disparo
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direccion = (mousePos - puntoDisparo.position);
            direccion.Normalize();

            GameObject bala = Instantiate(prefabBala, puntoDisparo.position, Quaternion.identity);
            Bala scriptBala = bala.GetComponent<Bala>();
            if (scriptBala != null)
            {
                scriptBala.velocidad = velocidadBala;
                scriptBala.Inicializar(direccion);
            }

            // Reproducir el sonido de disparo
            if (sonidoDisparo != null)
            {
                audioSource.volume = 0.3f;
                audioSource.PlayOneShot(sonidoDisparo);
            }
        }
    }
}
