
namespace Assets.Scripts.Player
{
	internal class PlayerRun : PlayerMovement
	{
		protected float time;
		protected PlayerMovementComponent playerMovementComponent;

		public PlayerRun(Fsm fsm, PlayerMovementComponent playerMovementComponent, float time) : base(fsm)
		{
			this.playerMovementComponent = playerMovementComponent;
			this.time = time;
		}

		protected override void Move()
		{
			if (!playerMovementComponent.CheckPressButtons())
				Fsm.SetState<PlayerIdle>();

			else
				playerMovementComponent.Move(time);
		}

		public override void FixedUpdate()
		{
			Move();
		}
	}
}
