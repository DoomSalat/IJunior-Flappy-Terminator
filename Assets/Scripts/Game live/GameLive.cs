using UnityEngine;

public class GameLive : MonoBehaviour
{
	private const float ScaleTimeDefault = 1;

	[SerializeField] private Menu _mainMenu;
	[Space]
	[SerializeField] private Player _player;
	[SerializeField] private Transform _playerDefaultPosition;

	public static event System.Action Restarted;

	private void OnEnable()
	{
		_player.Died += StopGame;
	}

	private void OnDisable()
	{
		_player.Died -= StopGame;
	}

	public void StartGame()
	{
		_mainMenu.CloseMenu();

		Restarted?.Invoke();
		Time.timeScale = ScaleTimeDefault;
		_player.transform.position = _playerDefaultPosition.position;
		_player.ResetState();
	}

	private void StopGame()
	{
		Time.timeScale = 0f;
		_mainMenu.OpenMenu();
	}
}
