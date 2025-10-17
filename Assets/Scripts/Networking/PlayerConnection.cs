using Cinemachine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PlayerConnection : MonoBehaviourPunCallbacks
{
	[SerializeField] private GameObject playerPrefab;
	[SerializeField] private GameObject virtualCameraPrefab;
	[SerializeField] private GameObject mainCameraPrefab;

	private int playerId;

	private PlayerController localPlayer;

	public PlayerController LocalPlayer => localPlayer;

	public int PlayerId => playerId;

	private void Start()
	{
		PhotonNetwork.ConnectUsingSettings();
	}

	public override void OnConnectedToMaster()
	{
		PhotonNetwork.JoinOrCreateRoom("TestRoom", new RoomOptions(), TypedLobby.Default);
	}

	public override void OnJoinedRoom()
	{
		if (PhotonNetwork.InRoom)
		{
			if (PhotonNetwork.IsConnected)
			{
				CreatePlayer();
				PhotonNetwork.AddCallbackTarget(this);
			}
		}
		else
		{
			Debug.LogWarning("Вы не в комнате! Не удается отправить событие.");
		}
	}

	private void CreatePlayer()
	{
		Vector3 spawnPosition = new Vector3(Random.Range(-2, 2), 0.5f, Random.Range(-2, 2));

		var localVirtualCameraObject = Instantiate(virtualCameraPrefab, spawnPosition, Quaternion.identity);
		var localMainCameraObject = Instantiate(mainCameraPrefab, spawnPosition, Quaternion.identity);
		var localPlayerObject = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);

		var localVirtualCamera = localVirtualCameraObject.GetComponent<CinemachineVirtualCamera>();
		var localPlayerComponent = localPlayerObject.GetComponent<PlayerController>();
		var localMainCamera = localMainCameraObject.transform;

		playerId = PhotonNetwork.LocalPlayer.ActorNumber;

		localPlayerComponent.CameraTransform = localMainCamera;
		localPlayerComponent.InitPlayer();
		localPlayerComponent.InitState();

		localPlayer = localPlayerComponent;

		localVirtualCamera.LookAt = localPlayer.transform;
		localVirtualCamera.Follow = localPlayer.transform;
	}

	private void OnDestroy()
	{
		PhotonNetwork.RemoveCallbackTarget(this);
	}
}
