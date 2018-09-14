using UnityEngine;

public class SneakState : State
{
    private float horizontalMove = 0f;

    public SneakState(Player player) : base(player)
    {
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        player.SetAnimation("Edge", player.OnEdge());
        player.SetAnimation("Sneak", true);
    }

    public override void Update()
    {
        base.Update();

        horizontalMove = Input.GetAxisRaw("Horizontal") * Time.fixedDeltaTime;

        if (horizontalMove < 0 && player.FacingLeft() && player.OnEdge())
            horizontalMove = 0;
        else if (horizontalMove > 0 && !player.FacingLeft() && player.OnEdge())
            horizontalMove = 0;

        if (Input.GetButtonUp("Sneak"))
        {
            if (horizontalMove == 0)
                player.SetState(new IdleState(player));
            else
                player.SetState(new RunningState(player));
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        player.SetAnimation("xSpeed", Mathf.Abs(player.GetVelocity().x));
        player.Move(horizontalMove * player.GetCrouchMultiplier());

        player.SetAnimation("Edge", player.OnEdge());
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
        player.SetAnimation("Sneak", false);
    }
}
