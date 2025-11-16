using UnityEngine;

namespace Assets.Scripts.Interface
{
	internal interface ICharacterFactory
	{
		public GameObject Create(Vector3 position);
	}
}
