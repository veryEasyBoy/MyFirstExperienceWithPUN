using Zenject;
using Photon.Pun;

public class GameInstaller : MonoInstaller
{
	public override void InstallBindings()
	{
		// Например, привяжем менеджер подключения
		Container.Bind<PhotonConnectionManager>().AsSingle();
		// Другие зависимости
	}
}