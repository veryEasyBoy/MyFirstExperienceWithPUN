using Photon.Realtime;
using UnityEngine;

public class PlayerMovementComponent
{
	private float moveSpeed = 5f;
	private float rotationSpeed = 5f;

	private CameraFollowComponent cameraFollow;
	private ControllerInputComponent controllerInput;

	private Rigidbody rb;
	private Transform cameraTransform;

	public PlayerMovementComponent(float moveSpeed, float rotationSpeed, Rigidbody rb, Transform cameraTransform)
	{
		this.moveSpeed = moveSpeed;
		this.rb = rb;
		this.rotationSpeed = rotationSpeed;
		this.cameraTransform = cameraTransform;
	}

	public void Awake()
	{
		cameraFollow = new CameraFollowComponent(cameraTransform);
		controllerInput = new ControllerInputComponent();
	}

	public void OnEnable()
	{
		controllerInput.OnEnable();
	}

	public bool CheckPressButtons()
	{
		if (controllerInput.MoveVertical != 0f || controllerInput.MoveHorizontal != 0f)
			return true;
		else
			return false;
	}

	public void FixedUpdate(float fixedDeltaTime)
	{
		cameraFollow.FixedUpdate();

		Vector3 move = new Vector3(controllerInput.MoveVertical, 0, controllerInput.MoveHorizontal).normalized * moveSpeed * fixedDeltaTime;
		Vector3 moveDirection = cameraFollow.CameraForward * move.x + cameraFollow.CameraRight * move.z;

		Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

		rb.MovePosition(rb.position + moveDirection);
		rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * fixedDeltaTime);
	}
}