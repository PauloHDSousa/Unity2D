using UnityEngine;

public class MusicTransition : MonoBehaviour {

	private static MusicTransition instance = null;

    void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(instance);
        } else 
           Destroy(instance);
    }
}
