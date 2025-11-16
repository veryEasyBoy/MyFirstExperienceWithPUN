using Assets.Scripts.Interface;
using UnityEngine;

public class CameraFollow : ICameraDirection
{
	public Transform cameraTransform;

	public Vector3 DirectionForward { get; private set; }

	public Vector3 DirectionRight { get; private set; }

	public void GetDirection()
	{
		DirectionForward = cameraTransform.transform.forward;
		DirectionForward.Normalize();

		DirectionRight = cameraTransform.transform.right;
		DirectionRight.Normalize();
	}
}
