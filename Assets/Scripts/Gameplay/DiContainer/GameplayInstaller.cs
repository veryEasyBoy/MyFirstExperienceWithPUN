using Assets.Scripts.Camera;
using Assets.Scripts.Factory;
using Assets.Scripts.InputHandling;
using Assets.Scripts.Interface;
using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
	public GameObject characterPrefab;
	public GameObject cameraPrefab;

	public override void InstallBindings()
	{
		Container.Bind<IInput>().To<DesktopInput>().AsSingle();

		Container.Bind<ICameraDirection>().To<CameraFollow>().AsSingle();

		Container.BindInterfacesAndSelfTo<PlayerInputActions>().AsSingle();

		Container.BindInstance(characterPrefab).WithId("CharacterPrefab");
		Container.Bind<ICharacterFactory>().WithId("CharacterPrefab").To<CharacterFactory>().AsSingle();

		Container.BindInstance(cameraPrefab).WithId("CameraPrefab");
		Container.Bind<ICharacterFactory>().WithId("CameraPrefab").To<CameraFollowComponent>().AsSingle();


	}
}
