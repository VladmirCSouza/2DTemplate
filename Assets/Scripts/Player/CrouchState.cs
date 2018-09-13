using UnityEngine;

public class CrouchState : State {

    private float horizontalMove = 0f;

    public CrouchState(Player player) : base(player)
    {
        //empty
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        player.SetAnimation("Crouch", true);
    }

    public override void Update()
    {
        base.Update();
        horizontalMove = Input.GetAxisRaw("Horizontal") * Time.fixedDeltaTime;

        if (Input.GetButtonUp("Crouch"))
            player.SetState(new IdleState(player));

    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        player.SetAnimation("xSpeed", Mathf.Abs(player.GetVelocity().x));
        player.Move(horizontalMove * player.GetCrouchMultiplier());
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
        player.SetAnimation("Crouch", false);
    }
}
