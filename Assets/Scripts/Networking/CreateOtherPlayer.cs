using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOtherPlayer : MonoBehaviour
{
	[SerializeField] private GameObject otherPlayerPrefab;

	private Dictionary<int, GameObject> otherPlayers = new Dictionary<int, GameObject>();
	private Dictionary<int, Rigidbody> otherRigidbody = new Dictionary<int, Rigidbody>();

	public GameObject OtherPlayerPrefab => otherPlayerPrefab;

	public Dictionary<int, Rigidbody> OtherRigidbody => otherRigidbody;

	public GameObject GetOrCreatePlayerObject(int id, GameObject PlayerOtherPrefab)
	{
		if (otherPlayers.ContainsKey(id))
			return otherPlayers[id];

		GameObject obj = Instantiate(PlayerOtherPrefab, Vector3.zero, Quaternion.identity);

		otherPlayers[id] = obj;
		otherRigidbody[id] = obj.GetComponent<Rigidbody>();

		Debug.Log(otherPlayers.Count);
		return obj;
	}

	public IEnumerator InterpolateTransform(Rigidbody rb, Vector3 newPosition, Quaternion newRotation)
	{
		float elapsedTime = 0f;
		Vector3 startingPos = rb.position;
		Quaternion startingRot = rb.rotation;
		float duration = 0.04f;

		while (elapsedTime < duration)
		{
			rb.MovePosition(Vector3.Lerp(startingPos, newPosition, (elapsedTime / duration)));
			rb.rotation = Quaternion.Slerp(startingRot, newRotation, (elapsedTime / duration));
			elapsedTime += Time.fixedDeltaTime;
			yield return null;
		}

		rb.MovePosition(newPosition);
		rb.rotation = newRotation;
	}
}
