using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

[RequireComponent(typeof(PlayerConnection), typeof(CreateOtherPlayer))]
public class RoomEventHandler : MonoBehaviourPunCallbacks, IOnEventCallback
{
	private PlayerConnection playerConnection;
	private CreateOtherPlayer createOtherPlayer;

	private const byte MovementEventCode = 1;

	private void Awake()
	{
		playerConnection = GetComponent<PlayerConnection>();
		createOtherPlayer = GetComponent<CreateOtherPlayer>();
	}

	private void FixedUpdate()
	{
		if (playerConnection.LocalPlayer == null)
			return;

		SendRigidbodyState(playerConnection.LocalPlayer.Rb);
	}

	public void OnEvent(EventData photonEvent)
	{
		if (photonEvent.Code == MovementEventCode)
		{
			object[] data = (object[])photonEvent.CustomData;

			int senderId = (int)data[0];

			Vector3 position = (Vector3)data[1];
			Quaternion rotation = (Quaternion)data[2];
			Vector3 velocity = (Vector3)data[3];
			Vector3 angularVelocity = (Vector3)data[4];

			if (senderId == playerConnection.PlayerId)
				return;

			GameObject obj = createOtherPlayer.GetOrCreatePlayerObject(senderId, createOtherPlayer.OtherPlayerPrefab);

			if (obj != null)
			{
				foreach (var rb in createOtherPlayer.OtherRigidbody)
					if (senderId == rb.Key)
						StartCoroutine(createOtherPlayer.InterpolateTransform(createOtherPlayer.OtherRigidbody[rb.Key], position, rotation));
			}
		}
	}

	private void SendRigidbodyState(Rigidbody rb)
	{
		object[] content = new object[]
		{
			playerConnection.PlayerId,
			rb.position,
			rb.rotation,
			rb.velocity,
			rb.angularVelocity,
		};
		RaiseEventOptions options = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
		SendOptions sendOptions = new SendOptions { Reliability = true };
		PhotonNetwork.RaiseEvent(MovementEventCode, content, options, sendOptions);
	}
}
