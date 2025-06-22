using UnityEngine;

public class CargarAudioManager : MonoBehaviour
{
    [SerializeField] private GameObject audioManagerPrefab;

    private void Awake()
    {
        if (AudioManager.Instancia == null)
        {
            GameObject obj = Instantiate(audioManagerPrefab);
            obj.name = "AudioManager (Instanciado)";
            DontDestroyOnLoad(obj);
        }
    }
}
