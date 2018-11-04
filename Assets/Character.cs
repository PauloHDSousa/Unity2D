using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    bool exibirMensagem = true;
    bool sabeOnome = false;
    string[] falasAleatorias = { 
        "Só tenho medo de água.", 
        "Olha uma latinha com o seu nome! - aff, pai! - que foi Mucilon?",
        "Não roube! O Governo não gosta de concorrência",
        "Sabe o que o Catchup malandro disse para o sal? É nóis nas frita mano",
        "O bom daqui é que não tenho inimigos",
        "Eu sou um gato ou uma raposa?",
        "Essa música me da vontade de dançar",
        "Qual é o cúmulo da força??? R: Dobrar a rua",
        "Onde há fumaça, há alguém com olho lacrimejando.",
        "Aquela cantora preferida do Natal: Fafá de Belém.",
        "Acho que o carro preferido dos ursos, é o Polo.",
        "Dois tomates atravessavam a rua e um diz para o outro: - Olha o carro! ploft! - Aonde? ploft!",
        "Se um gato tem 7 vidas, ele pode morrer 7 ou 8 vezes?",
        "O que o carbono disse quando foi preso? Eu tenho direito a 4 ligações!",
        "Eu adoraria as manhãs, se elas começassem mais tarde!",
        "Prefiro calçar as botas do que fazer exercício.",
        "Não sou gordo. Sou é baixo para o meu peso.",
        "Já sei, estou na Matrix, só tenho que achar um telefone.",
        "Ouvi dizer que 5 patinhos foram passear, sem a mãe deles, que mãe sem noção",
        "LAMBDA LAMBDA LAMBDA NERDS!",
        "Porque uma pessoa adota um cachorro se existem gatos?",
        "Você conhece a piada do gato? claro que não, gato não pia"
    };

    public CharacterController2D controller;
    public Animator animator;
    public GameObject Floor;
    public AudioSource audioSource;
    public AudioClip JumpSound;
    public AudioClip DeathSound;
    public AudioClip FallingSound;
    private Vector2 touchOrigin = -Vector2.one;
    public Text dialog;

    //Para evitar pulo Duplo
    bool jumped = false;
    bool IsHurt = false;
    public float jumpForce = 300f;

    float horizontalMove = 0;
    public float runSpeed = 20f;
    bool jump = false;
    bool crouch = false;
    public Joystick joystick;
    Scene level;
    bool jaCaiu;

    void Start()
    {
        jaCaiu = PlayerPrefs.GetInt("jaCaiu") == 1;
        level = SceneManager.GetActiveScene();
        if (level.name == "Level0")
        {
            animator.SetBool("IsHurt", true);
            audioSource.PlayOneShot(FallingSound);
            DefineMensagemDialogo("?????");
        }
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
        if(!exibirMensagem)
            return;

        if (other.gameObject.CompareTag("Placa"))
        {
            var randomInt = Random.Range(0, falasAleatorias.Length);
            DefineMensagemDialogo(falasAleatorias[randomInt]);
            other.gameObject.GetComponent<Collider2D>().enabled = false;
        }

        if (other.gameObject.CompareTag("PlacaInofensivo"))
        {
            DefineMensagemDialogo("Esse sapo é tão inofensivo");
            other.gameObject.GetComponent<Collider2D>().enabled = false;
        }

        if (other.gameObject.CompareTag("PlacaCasa"))
        {
            DefineMensagemDialogo("Será que dessa vez eu vou pra casa?");
            other.gameObject.GetComponent<Collider2D>().enabled = false;
        }
        if (other.gameObject.CompareTag("PlacaEscuridao"))
        {
            DefineMensagemDialogo("Nada além de escuridão aqui");
            other.gameObject.GetComponent<Collider2D>().enabled = false;
        }

        if (other.gameObject.CompareTag("PlacaNome"))
        {
            sabeOnome = true;
            DefineMensagemDialogo("Meu segredo! meu nome é BOB");
            other.gameObject.GetComponent<Collider2D>().enabled = false;
        }

        if (other.gameObject.CompareTag("PlacaCuriosidade"))
        {
            DefineMensagemDialogo("Curiosidade matou o gato...");
            other.gameObject.GetComponent<Collider2D>().enabled = false;
        }

        if (other.gameObject.CompareTag("PlacaCuriosidade2"))
        {
            DefineMensagemDialogo("Já avisei sobra a curiosidade...");
            other.gameObject.GetComponent<Collider2D>().enabled = false;
        }

        if (other.gameObject.CompareTag("PlacaDuvida"))
        {
            string mensagemDuvida = "Escolha uma a casa e boa sorte";

            if (sabeOnome)
                mensagemDuvida += ", BOB";

            DefineMensagemDialogo(mensagemDuvida);
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
            if (level.name == "Level0")
            {
                
                if(!jaCaiu){
                    PlayerPrefs.SetInt("jaCaiu", 1);
                    jaCaiu = true;
                    DefineMensagemDialogo("Que lugar é esse? Parece que já sonhei com isso");
                }
                
            }
        }
    }

    public void GameOver()
    {
        animator.SetBool("IsJumping", false);
        IsHurt = true;
        animator.SetBool("IsHurt", IsHurt);
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
        exibirMensagem = false;
        yield return new WaitForSeconds(5);
        dialog.gameObject.SetActive(false);
        exibirMensagem = true;
    }
}
