using Assets.Scripts.Interface;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Manager
{
	internal class SpawnManager : MonoBehaviour
	{
		[Inject] private ICharacterFactory _characterFactory;

		public void SpawnCharacter()
		{
			var newCharacter = _characterFactory.Create(new Vector3(0, 0, 0));
		}
	}
}
