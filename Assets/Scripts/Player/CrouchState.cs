﻿using UnityEngine;

public class CrouchState : State
{

    private float horizontalMove = 0f;

    public CrouchState(Player player) : base(player)
    {
        //empty
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        player.SetAnimation("Crouch", true);
        player.CrouchCollider();
    }

    public override void Update()
    {
        base.Update();
        horizontalMove = Input.GetAxisRaw("Horizontal") * Time.fixedDeltaTime;

        if (horizontalMove < 0 && player.FacingLeft() && player.OnEdge())
            horizontalMove = 0;
        else if (horizontalMove > 0 && !player.FacingLeft() && player.OnEdge())
            horizontalMove = 0;

        if (Input.GetButtonUp("Crouch"))
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
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
        player.SetAnimation("Crouch", false);
        player.StandCollider();
    }
}
