using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    public AudioClip  AudioClip;

    private static AudioSource instance = null;
    public static AudioSource Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else 
            instance = gameObject.AddComponent<AudioSource>();

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        instance.clip = AudioClip;
        instance.Play();
    }
}
