using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConnectionButton : MonoBehaviour
{
	[SerializeField] private Button button;

	private void Start()
	{
		button.onClick.AddListener(LoadScene);
	}

	private void LoadScene()
	{
		SceneManager.LoadScene("MainScene");
	}
}
