public abstract class State {
    protected Player player;

    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }

    public State(Player player)
    {
        this.player = player;
    }
}
