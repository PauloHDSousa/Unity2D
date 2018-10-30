using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    bool playerInZone = false;
    public string levelToLoad = "";

    void Start()
    {

    }

    void Update()
    {
        if (/*(Input.GetKeyDown(KeyCode.W) || Input.GetButtonDown("Jump")) || */ playerInZone)
        {
            SceneManager.LoadScene(levelToLoad);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
            playerInZone = true;

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
            playerInZone = false;
    }
}
