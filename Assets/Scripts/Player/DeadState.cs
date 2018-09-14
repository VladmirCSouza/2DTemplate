using UnityEngine;

public class DeadState : State
{
    private float respawnTimer;
    private float timeToRespawn = 5f;

    public DeadState(Player player) : base(player)
    {
        //empty
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        player.SetAnimation("Dead", true);
        respawnTimer = 0;
    }

    public override void Update()
    {
        base.Update();

        respawnTimer += Time.deltaTime;

        if (respawnTimer >= timeToRespawn)
            player.Respawn();
    }

    //public override void FixedUpdate()
    //{
    //    base.FixedUpdate();
    //}

    public override void OnStateExit()
    {
        base.OnStateExit();
        player.SetAnimation("Dead", false);
    }
}
