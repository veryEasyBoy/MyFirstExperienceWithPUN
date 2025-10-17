using UnityEngine;

public class CameraFollowComponent
{
	private Transform cameraTransform;

	private Vector3 cameraForward;
	private Vector3 cameraRight;

	public Vector3 CameraForward => cameraForward;
	public Vector3 CameraRight => cameraRight;

	public CameraFollowComponent(Transform cameraTransform)
	{
		this.cameraTransform = cameraTransform;
	}

	public void FixedUpdate()
	{
		cameraForward = cameraTransform.forward;
		cameraForward.y = 0;
		cameraForward.Normalize();

		cameraRight = cameraTransform.right;
		cameraRight.y = 0;
		cameraRight.Normalize();
	}
}
