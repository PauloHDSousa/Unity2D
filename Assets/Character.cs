using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    string[] falasAleatorias = { "Outra vez?", "O que está acontecendo?", "Isto é um sonho?!", "Eu sou um gato ou uma raposa?", "Se eu cair da plataforma, será que eu morro?", "Quantas vidas será que eu tenho?", "E se eu dormir?", "Se um gato tem 7 vidas,ele pode morrer 7 ou 8 vezes?", "Quero minha dona" };
    public CharacterController2D controller;
    public Animator animator;
    public GameObject Floor;
    public AudioSource audioSource;
    public AudioClip JumpSound;
    public AudioClip DeathSound;
    public Canvas canvas;
    private Vector2 touchOrigin = -Vector2.one;
    public Text dialog;
    bool primeiraQueda = true;
    //Para evitar pulo Duplo
    bool jumped = false;
    bool IsHurt = false;
    public float jumpForce = 300f;

    float horizontalMove = 0;
    public float runSpeed = 20f;
    bool jump = false;
    bool crouch = false;
    public Joystick joystick;

    void Start()
    {
        animator.SetBool("IsHurt", true);
    }

    void Update()
    {


        horizontalMove = joystick.Horizontal * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if ((Input.GetButtonDown("Jump") || joystick.Vertical > 0) && !jumped && !IsHurt)
        {
            jumped = true;
            jump = true;
            animator.SetBool("IsJumping", true);
            audioSource.PlayOneShot(JumpSound);
            //Instantiate(effect, transform.position, Quaternion.identity);

            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
        }

        if (Input.GetButtonDown("Crouch"))
            crouch = true;
        else if (Input.GetButtonUp("Crouch"))
            crouch = false;

        animator.SetBool("IsCrouching", crouch);
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Placa"))
        {
            var randomInt = Random.Range(0,falasAleatorias.Length);
            DefineMensagemDialogo(falasAleatorias[randomInt]);
            other.gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == Floor.tag)
        {
            jumped = false;
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsHurt", IsHurt);
            if (primeiraQueda)
            {
                DefineMensagemDialogo("Aonde estou?");
                primeiraQueda = false;
            }
        }
    }

    public void GameOver()
    {
        IsHurt = true;
        animator.SetBool("IsHurt", IsHurt);
        audioSource.Stop();
        audioSource.PlayOneShot(DeathSound);
        StartCoroutine(Restart());
    }

    public void Pular()
    {

        if (!jumped && !IsHurt)
        {
            jumped = true;
            jump = true;
            animator.SetBool("IsJumping", true);
            audioSource.PlayOneShot(JumpSound);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
        }
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void DefineMensagemDialogo(string mensagem)
    {
        dialog.text = mensagem;
        dialog.gameObject.SetActive(true);
        StartCoroutine(EscondeMensagemDialogo());
    }

    IEnumerator EscondeMensagemDialogo()
    {
        yield return new WaitForSeconds(3);
        dialog.gameObject.SetActive(false);
    }
}
