using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour {


    public float maxSpeed = 10f;

    bool facingright = true;

    [SerializeField]
    private Rigidbody2D rigidbody2D;

    [SerializeField]
    private Collider2D playerGroundCollider;

    Animator anim;

    bool grounded = false;

    public Transform groundCheck;

    float groundRadius = 0.2f;

    public LayerMask whatIsGround;

    public float jumpForce = 700;

    private Checkpoint currentCheckpoint;

    private AudioSource audiosource;

    private float HorizontalInput;

    [SerializeField]
    private PhysicsMaterial2D playerMovingPhys, playerStoppingPhys;

	// Use this for initialization
	void Start () {
        rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();
    }
	
	
	// Update is called once per frame
	void FixedUpdate ()
    {
       
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", grounded);

        anim.SetFloat("vSpeed", rigidbody2D.velocity.y);


        float move = Input.GetAxis("Horizontal");

        anim.SetFloat("Speed", Mathf.Abs(move));

        rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);

        UpdatePhysicsMaterial();

        if (move > 0 && !facingright)
            Flip();
        else if(move < 0 && facingright)
            Flip();


	}

    private void Update()
    {
        if(grounded && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("Ground", false);
            rigidbody2D.AddForce(new Vector2(0, jumpForce));
        }
    }

    private void UpdatePhysicsMaterial()
    {
        if(Mathf.Abs(HorizontalInput) > 0)
        {
            playerGroundCollider.sharedMaterial = playerMovingPhys;
        }
        else
        {
            playerGroundCollider.sharedMaterial = playerStoppingPhys;
        }
    }

    void Flip()
    {
        facingright = !facingright;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
           other.gameObject.SetActive(false);
            audiosource.Play();
        }
    }


    public void Respawn()
    {
        if (currentCheckpoint == null)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        else
        {
            rigidbody2D.velocity = Vector2.zero;
            transform.position = currentCheckpoint.transform.position;
        }
    }

    public void SetCurrentCheckpoint(Checkpoint newcurrentChceckpoint)
    {
        if (currentCheckpoint != null)
            currentCheckpoint.setIsActive(false);

        currentCheckpoint = newcurrentChceckpoint;
        currentCheckpoint.setIsActive(true);
    }
}
