using Assets.Scripts.Interface;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Factory
{
	internal class CharacterFactory : ICharacterFactory
	{
		readonly private DiContainer container;
		readonly private GameObject characterPrefab;

		public CharacterFactory(DiContainer container, [Inject(Id = "CharacterPrefab")] GameObject characterPrefab)
		{
			this.container = container;
			this.characterPrefab = characterPrefab;
		}

		public GameObject Create(Vector3 position)
		{
			var character = container.InstantiatePrefab(characterPrefab, position, Quaternion.identity, null);
			return character;
		}
	}
}
