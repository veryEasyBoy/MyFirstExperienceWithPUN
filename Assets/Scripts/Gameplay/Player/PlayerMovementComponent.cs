using Assets.Scripts.Interface;
using UnityEngine;

namespace Assets.Scripts.Player
{
	internal class PlayerMovementComponent
	{
		private float moveSpeed;
		private float rotationSpeed;

		private Transform cameraFollow;
		private IInput desktopInput;

		private Rigidbody rb;

		public PlayerMovementComponent(float moveSpeed, float rotationSpeed, IInput desktopInput, Transform cameraFollow, Rigidbody rb)
		{
			this.moveSpeed = moveSpeed;
			this.rb = rb;
			this.rotationSpeed = rotationSpeed;
			this.desktopInput = desktopInput;
			this.cameraFollow = cameraFollow;
		}

		public void OnEnable()
		{
			desktopInput.InitialInput();
		}

		public bool CheckPressButtons()
		{
			if (desktopInput.DirectionInput != Vector2.zero)
				return true;
			else
				return false;
		}

		public void Move(float fixedDeltaTime)
		{
			Vector3 move = new Vector3(desktopInput.DirectionInput.y, 0, desktopInput.DirectionInput.x).normalized * moveSpeed * fixedDeltaTime;
		    Vector3 moveDirection = cameraFollow.transform.forward * move.x + cameraFollow.transform.right * move.z;

			Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

			rb.MovePosition(rb.position + moveDirection);
			rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * fixedDeltaTime);
		}
	}
}