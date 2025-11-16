
using UnityEngine;

namespace Assets.Scripts.Interface
{
	internal interface ICameraDirection
	{
		public Vector3 DirectionForward { get; }

		public Vector3 DirectionRight {  get; }

		public void GetDirection();
	}
}
