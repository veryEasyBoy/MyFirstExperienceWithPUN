
namespace Assets.Scripts.Player
{
	internal class PlayerMovement : FsmState
	{
		public PlayerMovement(Fsm fsm) : base(fsm) { }

		protected virtual void Move() { }
	}
}
