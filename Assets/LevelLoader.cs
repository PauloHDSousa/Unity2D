using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
    public string levelToLoad = "";
    public Animator animTransition;
    public Animator musicTransition;

    public void GoToCredits()
    {
        StartCoroutine(LoadScene());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
             StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        animTransition.SetTrigger("end");
        musicTransition.SetTrigger("FadeOut");
        
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(levelToLoad);
    }
}
