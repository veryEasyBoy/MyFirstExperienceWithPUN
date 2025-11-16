using Assets.Scripts.Interface;
using Assets.Scripts.Player;
using Cinemachine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Networking
{
	internal class PlayerConnection : MonoBehaviourPunCallbacks
	{
		[SerializeField] private GameObject virtualCameraPrefab;

		[Inject(Id = "CharacterPrefab")]
		private ICharacterFactory characterFactory;

		[Inject(Id = "CameraPrefab")]
		private ICharacterFactory cameraPrefab;

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
				Debug.LogWarning("You are not in room");
			}
		}

		private void CreatePlayer()
		{

			Vector3 spawnPosition = new Vector3(Random.Range(-2, 2), 0.5f, Random.Range(-2, 2));

			var virtualCamera = Instantiate(virtualCameraPrefab, spawnPosition, Quaternion.identity);
			var camera = cameraPrefab.Create(spawnPosition);
			var player = characterFactory.Create(spawnPosition);

			var virtualCameraCaomponent = virtualCamera.GetComponent<CinemachineVirtualCamera>();
			var playerComponent = player.GetComponent<PlayerController>();

			playerId = PhotonNetwork.LocalPlayer.ActorNumber;

			playerComponent.CameraTransform = camera.transform;
			playerComponent.InitPlayer();
			playerComponent.InitState();

			localPlayer = playerComponent;

			virtualCameraCaomponent.LookAt = playerComponent.transform;
			virtualCameraCaomponent.Follow = playerComponent.transform;
		}

		private void OnDestroy()
		{
			PhotonNetwork.RemoveCallbackTarget(this);
		}
	}
}
