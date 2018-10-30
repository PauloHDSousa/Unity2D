using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;
    public GameObject Floor;
    public AudioSource audioSource;
    public AudioClip JumpSound;
    public AudioClip DeathSound;
    public Canvas canvas;
    private Vector2 touchOrigin = - Vector2.one;

    //Para evitar pulo Duplo
    bool jumped = false;
    bool IsHurt = false;
    public float jumpForce = 300f;
    bool goToNextLevel = false;
   

    float horizontalMove = 0;
    public float runSpeed = 20f;
    bool jump = false;
    bool crouch = false;
    public Joystick joystick;
    Text tm;
    void Start()
    {

        //ngo = new GameObject("myTextGO");
        Text tm = canvas.gameObject.AddComponent<Text>();
        tm.text = "put your text here";
        tm.color = new Color(255f, 0f, 0f);
        tm.fontStyle = FontStyle.Bold;
        Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        tm.font = ArialFont;
        tm.fontSize = 20;

        //ngo.transform.SetParent(canvas.transform);

    }

    void Update()
    {
        //ngo.transform.position = new Vector3(0, 0, 0);
        //tm.transform.position = new Vector3(0, 0, 0);


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


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == Floor.tag)
        {
            jumped = false;
            animator.SetBool("IsJumping", false);
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
}
