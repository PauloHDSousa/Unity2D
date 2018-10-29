using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;
    public GameObject Floor;
    public AudioSource audioSource;
    public AudioClip JumpSound;
    public AudioClip DeathSound;

    private Vector2 touchOrigin = - Vector2.one;

    //Para evitar pulo Duplo
    bool jumped = false;
    bool IsHurt = false;
    public float jumpForce = 300f;

    float horizontalMove = 0;
    public float runSpeed = 20f;
    bool jump = false;
    bool crouch = false;
    public Joystick joystick;

    void Update()
    {
        //if(Input.touchCount > 0)
        //{
        //    Touch myTouch = Input.touches[0];
        //    if(myTouch.phase == TouchPhase.Began)
        //        touchOrigin = myTouch.position;
        //    else if(myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
        //    {
        //        Vector2 touchEnd = myTouch.position;
        //        float x = touchEnd.x - touchOrigin.x;
        //        float y = touchEnd.y - touchOrigin.y;
        //        touchOrigin.x = -1;
        //        if(Mathf.Abs(x) > Mathf.Abs(y))
        //            horizontalMove = x > 0 ? 1 : -1;
        //        else
        //            vertical = y > 0 ? 1 : -1;
        //    }
        //} 
        //else {
            //if(joystick.Horizontal <= 0 )
            //    horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
            //else
                horizontalMove = joystick.Horizontal * runSpeed;
            //}

        //if()

        //Vector3 moveVector = (Vector3.right * joystick.Horizontal + Vector3.up * joystick.Vertical);

        //if (moveVector != Vector3.zero)
        //{ 
        //    transform.rotation = Quaternion.LookRotation(Vector3.forward, moveVector);
        //    transform.Translate(moveVector * runSpeed * Time.deltaTime, Space.World);
        //}

       
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

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(2); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
