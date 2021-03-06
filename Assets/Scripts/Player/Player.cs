﻿using UnityEngine;

public class Player : MonoBehaviour {

    //Variaveis e fatores de movimento
    [SerializeField] private float runSpeed = 40f;
    [SerializeField] private float jumpForce = 550f;
    [Range(0, 1)] [SerializeField] private float crouchSpeed = .36f;
    [Range(0, .3f)] public float movementSmoothing = .05f;
    [SerializeField] private bool airControl = true;

    //SpriteRenderer utilizado para aplicar o "flip" na sprite
    [Space]
    [SerializeField] private SpriteRenderer sprite;

    //Variaveis para verificar se jogador está no chao
    [Space]
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform groungCheckRight;
    [SerializeField] private Transform groungCheckLeft;
    [SerializeField] private Collider2D colliderStand;
    [SerializeField] private Collider2D colliderCrouch;

    private State currentState;
    private Animator animator;
    private Rigidbody2D rigidbody2D;
    private Vector3 velocity = Vector3.zero;
    private RaycastHit2D[] hit = new RaycastHit2D[2];
    private Vector3 respawnPoint;

    void Awake()
    {
        respawnPoint = transform.position;
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        SetState(new IdleState(this));
    }

	void Update ()
    {
        currentState.Update();
        //TODO: Just for debug purpose
        if (Input.GetKeyDown(KeyCode.K))
        {
            KillPlayer();
        }
	}

    void FixedUpdate()
    {
        currentState.FixedUpdate();
    }

    /// <summary>
    /// Devine qual é o estado atual
    /// </summary>
    /// <param name="newState">Qual estado deverá ser chamado</param>
    public void SetState(State newState)
    {
        if (currentState != null)
            currentState.OnStateExit();

        currentState = newState;
        gameObject.name = "Player - " + currentState;

        if (currentState != null)
            currentState.OnStateEnter();
    }

    /// <summary>
    /// Move o player
    /// </summary>
    /// <param name="speed">Velocidade de movimento</param>
    public void Move(float speed)
    {
        Vector3 targetVelocity = new Vector2(speed * 10f * runSpeed, GetVelocity().y);
        rigidbody2D.velocity = Vector3.SmoothDamp(rigidbody2D.velocity, targetVelocity, ref velocity, movementSmoothing);

        if (speed > 0 && FacingLeft())
            Flip(false);
        else if (speed < 0 && !FacingLeft())
            Flip(true);
    }

    /// <summary>
    /// Aplica a força para fazer o jogador pular
    /// </summary>
    public void Jump()
    {
        rigidbody2D.AddForce(new Vector2(0, jumpForce));
    }

    /// <summary>
    /// Verifica se o jogador está em contato com o chao
    /// </summary>
    /// <returns>TRUE se o jogador estiver no chão</returns>
    public bool IsGrounded()
    {
        if (Physics2D.RaycastNonAlloc(groundCheck.position, Vector2.down, hit, 0.2f, whatIsGround) > 0)
            return true;

        return false;
    }

    /// <summary>
    /// Verifica se o jogador está na extremidade de alguma plataforma.
    /// Utilizado principalmente para disparar a animação do jogador se equilibrando
    /// </summary>
    /// <returns>TRUE se o jogador estiver na beirada</returns>
    public bool OnEdge()
    {
        bool onEdge = false;

        if (FacingLeft())
            onEdge = Physics2D.RaycastNonAlloc(groungCheckLeft.position, Vector2.down, hit, 0.2f, whatIsGround) <= 0;
        else
            onEdge = Physics2D.RaycastNonAlloc(groungCheckRight.position, Vector2.down, hit, 0.2f, whatIsGround) <= 0;

        return onEdge;
    }

    /// <summary>
    /// Verifica se o player está "olhando para a direita
    /// </summary>
    /// <returns>TRUE se o jogador está olhando para a direita</returns>
    public bool FacingLeft()
    {
        return sprite.flipX;
    }

    /// <summary>
    /// Rotaciona a sprite do jogador no eixo X
    /// </summary>
    /// <param name="faceLeft">TRUE para fazer a sprite apontar para a esquerda</param>
    private void Flip(bool faceLeft)
    {
        sprite.flipX = faceLeft;
    }

    /// <summary>
    /// Utilizada para ligar e desligar animações
    /// </summary>
    /// <param name="name">O nome da animação definida no Animator</param>
    /// <param name="value">TRUE para ligar e FALSE para desligar animação</param>
    public void SetAnimation(string name, bool value)
    {
        animator.SetBool(name, value);
    }

    /// <summary>
    /// Utilizada para passar valores ao animator
    /// </summary>
    /// <param name="name">O nome da animação definida no Animator</param>
    /// <param name="value">float a ser passado ao animator</param>
    public void SetAnimation(string name, float value)
    {
        animator.SetFloat(name, value);
    }

    /// <summary>
    /// Controla opção de controlar jogador no ar
    /// </summary>
    /// <returns>TRUE se a opção está ligada no inspector</returns>
    public bool CanControlOnAir()
    {
        return airControl;
    }

    /// <summary>
    /// "Get" o multiplicador de velocidade para quando o jogador estiver abaixado
    /// </summary>
    /// <returns>Quantos % da velocidade sera utilizado qdo abaixado</returns>
    public float GetCrouchMultiplier()
    {
        return crouchSpeed;
    }

    /// <summary>
    /// "Get" a velocidade do Rigidbody
    /// </summary>
    /// <returns>Vector 2 da velocidade do player</returns>
    public Vector2 GetVelocity()
    {
        return rigidbody2D.velocity;
    }

    /// <summary>
    /// Liga o collider do jogador em pé
    /// </summary>
    public void StandCollider()
    {
        colliderStand.enabled = true;
        colliderCrouch.enabled = false;
    }

    /// <summary>
    /// Liga o collider do jogador abaixado
    /// </summary>
    public void CrouchCollider()
    {
        colliderStand.enabled = false;
        colliderCrouch.enabled = true;
    }

    /// <summary>
    /// Função utilizada para chamar o estado de morte do player
    /// </summary>
    public void KillPlayer()
    {
        SetState(new DeadState(this));
    }

    public void Respawn()
    {
        rigidbody2D.position = respawnPoint;
        SetState(new IdleState(this));
    }
}
