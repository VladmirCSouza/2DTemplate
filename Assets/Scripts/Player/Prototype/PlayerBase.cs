using UnityEngine;

public class PlayerBase : MonoBehaviour {

    //Variaveis de controle de pulo
    [SerializeField] private float jumpForce = 400f;
    [SerializeField] private float wallJumpForce = 350f;
    [SerializeField] private bool canDoubleJump = false;
    [SerializeField] private bool airControl = false;

    //Fatores de movimento
    [Range(0, 1)] [SerializeField] private float crouchSpeed = .36f;
    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;

    //Layer utilizado para verificar o que e chao
    [SerializeField] private LayerMask whatIsGround;

    //Variaveis de verificacao de colisao (utilizado nos raycasts)
    [SerializeField] private Transform ceilingCheck;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform groungCheckRight;
    [SerializeField] private Transform groungCheckLeft;
    [SerializeField] private Transform wallCheckRight;
    [SerializeField] private Transform wallCheckLeft;

    [SerializeField] private Collider2D grouchDisableCollider;
    [SerializeField] private SpriteRenderer sprite;

    private bool grounded;
    private Rigidbody2D rgdb2D;
    private bool facingRight = true;

    private Vector3 velocity = Vector3.zero;

    //Raycast utilizado para verificar colisoes com chao, parede e teto
    private RaycastHit2D[] hit = new RaycastHit2D[1];

    void Awake()
    {
        rgdb2D = GetComponent<Rigidbody2D>();
    }

    void Start ()
    {
        
	}
	
	void Update ()
    {
		
	}

    void FixedUpdate()
    {
        
    }

    public void Move(float move, bool crouch, bool jump)
    {

    }


    bool IsGrounded()
    {
        return true;
    }


    void Flip()
    {
        sprite.flipX = facingRight;
        facingRight = !facingRight;
    }
}
