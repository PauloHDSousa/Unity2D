using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Options()
    {
        SceneManager.LoadScene("Options");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Voltar()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Creditos");
    }


    public void SendEmail()
    {
        string email = "paulo.ti.sousa@gmail.com";
        string subject = MyEscapeURL("Dúvidas, Críticas e Sugestões");
        string body = MyEscapeURL("Olá, eu queria dizer que");
        Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
    }
    string MyEscapeURL(string url)
    {
        return WWW.EscapeURL(url).Replace("+", "%20");
    }
}
