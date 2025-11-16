using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonCallbacksHandler : MonoBehaviourPunCallbacks
{
	private static int uu = 0;
	private void Start()
	{
		Connect();
	}
	public void Connect()
	{
		PhotonNetwork.ConnectUsingSettings();
	}

	public override void OnConnectedToMaster()
	{
		Debug.Log("Connected to Master");
		PhotonNetwork.JoinRandomRoom();
	}
	public override void OnJoinedLobby()
	{
		Debug.Log("Joined Lobby");
		PhotonNetwork.JoinOrCreateRoom("roomName", new RoomOptions(), TypedLobby.Default);
	}

	public override void OnJoinRandomFailed(short returnCode, string message)
	{
		Debug.Log("Joining random room failed, creating new room");
		PhotonNetwork.CreateRoom(null, new RoomOptions());
	}

	public override void OnJoinedRoom()
	{
		uu++;
		Debug.LogError("Joined room " + uu);
		// Создаем или инициализируем игрока
	}
}