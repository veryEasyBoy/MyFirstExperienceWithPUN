using Assets.Scripts.Interface;
using UnityEngine;

namespace Assets.Scripts.InputHandling
{
	internal class DesktopInput : IInput
	{
		private PlayerInputActions playerInput;

		public Vector2 DirectionInput { get; private set; }

		public void PlayerInput(PlayerInputActions playerInput)
		{
			this.playerInput = playerInput;
		}

		public void InitialInput()
		{
			playerInput = new PlayerInputActions();

			playerInput.Move.WASD.performed += ctx => DirectionInput = ctx.ReadValue<Vector2>();
			playerInput.Move.WASD.canceled += ctx => DirectionInput = Vector2.zero;

			playerInput.Enable();
		}
	}
}
