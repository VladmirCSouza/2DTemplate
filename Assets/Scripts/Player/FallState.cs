public class FallState : JumpAndFallController
{
    public FallState(Player player) : base(player)
    {
        //empty
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        player.SetAnimation("JumpAndFall", true);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (player.IsGrounded() && player.GetVelocity().y == 0f)
        {
            if (horizontalMove != 0)
                player.SetState(new RunningState(player));
            else
                player.SetAnimation("JumpAndFall", false);
        }
    }
}
