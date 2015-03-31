public class RespawnEvent : GameEvent{
	public StateObj checkpoint;
	public float maxHealth;
	public RespawnEvent(StateObj checkpoint, float maxHealth) : base("OnRespawn"){
		this.checkpoint = checkpoint;
		this.maxHealth = maxHealth;
	}
}
