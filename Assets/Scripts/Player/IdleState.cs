using UnityEngine;

public class IdleState : State
{

    public IdleState(Player player) : base(player)
    {
        //empty
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        player.SetAnimation("Edge", player.OnEdge());
        player.SetAnimation("Idle", true);
    }

    public override void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
            player.SetState(new RunningState(player));

        if (Input.GetButtonDown("Jump") && player.IsGrounded())
            player.SetState(new JumpState(player));

        if (Input.GetButton("Crouch"))
            player.SetState(new CrouchState(player));

        if (Input.GetButton("Sneak"))
            player.SetState(new SneakState(player));
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.SetAnimation("Edge", player.OnEdge());
    }


    public override void OnStateExit()
    {
        base.OnStateExit();
        player.SetAnimation("Idle", false);
        player.SetAnimation("Edge", false);
    }
}
