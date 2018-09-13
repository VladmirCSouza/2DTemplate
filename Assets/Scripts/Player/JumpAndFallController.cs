using UnityEngine;

public class JumpAndFallController : State {

    protected float horizontalMove = 0f;

    public JumpAndFallController(Player player) : base(player)
    {
        //empty
    }

    public override void Update()
    {
        base.Update();
        horizontalMove = Input.GetAxisRaw("Horizontal") * Time.fixedDeltaTime;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.SetAnimation("ySpeed", player.GetVelocity().y);

        if (player.CanControlOnAir())
            player.Move(horizontalMove);
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
        player.SetAnimation("JumpAndFall", false);
    }
}
