using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Restart : MonoBehaviour
{
	[SerializeField] private GameLive _gameLive;

	private Button _button;

	private void Awake()
	{
		_button = GetComponent<Button>();
	}

	private void OnEnable()
	{
		_button.onClick.AddListener(RestartGame);
	}

	private void OnDisable()
	{
		_button.onClick.RemoveListener(RestartGame);
	}

	private void RestartGame()
	{
		_gameLive.RestartGame();
	}
}
