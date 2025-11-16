using Assets.Scripts.Interface;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Camera
{
	internal class CameraFollowComponent : ICharacterFactory
	{
		readonly private GameObject cameraPrefab;


		readonly private DiContainer container;

		public CameraFollowComponent(DiContainer container, [Inject(Id = "CameraPrefab")] GameObject cameraPrefab)
		{
			this.cameraPrefab = cameraPrefab;
			this.container = container;
		}

		public GameObject Create(Vector3 position)
		{
			var camera = container.InstantiatePrefab(cameraPrefab, position, Quaternion.identity, null);
			return camera;
		}
	}
}
