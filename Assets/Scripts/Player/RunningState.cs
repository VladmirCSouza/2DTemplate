using UnityEngine;

public class RunningState : State {

    private float horizontalMove = 0f;

    public RunningState(Player player) : base(player)
    {
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        player.SetAnimation("Run", true);
    }

    public override void Update()
    {
        base.Update();
        horizontalMove = Input.GetAxisRaw("Horizontal") * Time.fixedDeltaTime;
        
        if (horizontalMove == 0 && player.GetVelocity().x == 0)
            player.SetState(new IdleState(player));

        if (player.GetVelocity().y != 0)
            player.SetState(new FallState(player));

        if (Input.GetButtonDown("Jump") && player.IsGrounded())
            player.SetState(new JumpState(player));

        if(Input.GetButton("Crouch"))
            player.SetState(new CrouchState(player));

        if (Input.GetButton("Sneak"))
            player.SetState(new SneakState(player));
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        player.Move(horizontalMove);
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
        player.SetAnimation("Run", false);
    }
}
