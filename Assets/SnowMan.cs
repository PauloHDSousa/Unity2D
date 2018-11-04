using UnityEngine;

public class SnowMan : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            GetComponent<Animator>().SetBool("show", true);
            AudioSource audio = GetComponent<AudioSource>();
            audio.PlayOneShot(audio.clip);
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
