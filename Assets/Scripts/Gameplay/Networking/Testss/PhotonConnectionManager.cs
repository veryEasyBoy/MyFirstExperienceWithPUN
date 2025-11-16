using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Zenject;

public class PhotonConnectionManager : MonoBehaviour, IInitializable
{
	private readonly SignalBus _signalBus;

	public PhotonConnectionManager(SignalBus signalBus)
	{
		_signalBus = signalBus;
	}

	public void Initialize()
	{
		Connect();
	}

	public void Connect()
	{
		if (!PhotonNetwork.IsConnected)
		{
			PhotonNetwork.ConnectUsingSettings();
		}
	}

	public void OnConnectedToMaster()
	{
		Debug.Log("Connected to Photon Master");
		// Можно инициировать присоединение к комнате
		PhotonNetwork.JoinRandomRoom();
	}

	public void OnJoinRandomFailed(short returnCode, string message)
	{
		Debug.Log("Failed to join room, создаем новую");
		PhotonNetwork.CreateRoom(null, new RoomOptions());
	}

	public void OnJoinedRoom()
	{
		Debug.Log("Присоединились к комнате");
		// Тут можно создать игрока
		// Например, вызываем событие или напрямую создаем объект
	}
}