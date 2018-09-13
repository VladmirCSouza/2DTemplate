using UnityEngine;

public class IdleState : State {

    public IdleState(Player player) : base(player)
    {
        //empty
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        player.SetAnimation("Idle", true);
    }

    public override void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
            player.SetState(new RunningState(player));

        if (Input.GetButtonDown("Jump") && player.IsGrounded())
            player.SetState(new JumpState(player));

        if(Input.GetButtonDown("Crouch"))
            player.SetState(new CrouchState(player));
    }


    public override void OnStateExit()
    {
        base.OnStateExit();
        player.SetAnimation("Idle", false);
    }
}
