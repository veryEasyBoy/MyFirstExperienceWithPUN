using UnityEngine;

namespace Assets.Scripts.Interface
{
	internal interface IInput
	{
		public Vector2 DirectionInput { get; }

		public void InitialInput();
	}
}
