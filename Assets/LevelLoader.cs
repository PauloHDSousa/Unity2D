using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
    public string levelToLoad = "";
    public Animator animTransition;
    public Animator musicTransition;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
             StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        if(musicTransition != null)
            musicTransition.SetTrigger("FadeOut");
        
        animTransition.SetTrigger("end");
        yield return new WaitForSeconds(2f);
         SceneManager.LoadScene(levelToLoad);
    }
}
