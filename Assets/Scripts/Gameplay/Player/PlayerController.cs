using Assets.Scripts.Interface;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Player
{
	[RequireComponent(typeof(Rigidbody))]
	internal class PlayerController : MonoBehaviour
	{
		[SerializeField] private Transform cameraTransform;

		[SerializeField] private float moveSpeed = 5f;
		[SerializeField] private float rotationSpeed = 5f;

		[Inject] private IInput desktopInput;

		private Fsm fsm;

		private PlayerMovementComponent playerMovement;

		private Rigidbody rb;

		public Rigidbody Rb { get => rb; set { rb = value; } }

		public Transform CameraTransform { get => cameraTransform; set { cameraTransform = value; } }

		private void Awake()
		{
			rb = GetComponent<Rigidbody>();
		}

		public void InitPlayer()
		{
			playerMovement = new PlayerMovementComponent(moveSpeed, rotationSpeed, desktopInput, cameraTransform, rb);

			playerMovement.OnEnable();
		}

		public void InitState()
		{
			fsm = new Fsm();
			fsm.AddState(new PlayerIdle(fsm, playerMovement));
			fsm.AddState(new PlayerRun(fsm, playerMovement, Time.fixedDeltaTime));
			fsm.SetState<PlayerIdle>();
		}

		private void FixedUpdate()
		{
			if (fsm != null)
				fsm.FixedUpdate();
		}
	}
}
