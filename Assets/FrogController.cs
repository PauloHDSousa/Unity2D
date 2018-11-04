using UnityEngine;

public class FrogController : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            AudioSource audio =  GetComponent<AudioSource>();
            audio.PlayOneShot(audio.clip);
        }
    }
}
