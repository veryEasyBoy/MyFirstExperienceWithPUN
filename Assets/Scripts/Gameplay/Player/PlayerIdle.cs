
namespace Assets.Scripts.Player
{
	internal class PlayerIdle : PlayerMovement
	{
		protected PlayerMovementComponent playerMovementComponent;

		public PlayerIdle(Fsm fsm, PlayerMovementComponent playerMovementComponent) : base(fsm)
		{
			this.playerMovementComponent = playerMovementComponent;
		}

		protected override void Move()
		{
			if (playerMovementComponent.CheckPressButtons())
				Fsm.SetState<PlayerRun>();
		}

		public override void FixedUpdate()
		{
			Move();
		}
	}
}
