using UnityEngine;

public class GameLive : MonoBehaviour
{
	private const float scaleTimeDefault = 1;

	[SerializeField] private CanvasGroup _mainGroupUI;
	[Space]
	[SerializeField] private Player _player;
	[SerializeField] private Transform _playerDefaultPosition;

	public static event System.Action Reseted;

	private void Start()
	{
		CloseMenu();
	}

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
		CloseMenu();

		Reseted?.Invoke();
		Time.timeScale = scaleTimeDefault;
		_player.transform.position = _playerDefaultPosition.position;
		_player.ResetState();
	}

	private void StopGame()
	{
		Time.timeScale = 0f;
		OpenMenu();
	}

	private void OpenMenu()
	{
		_mainGroupUI.gameObject.SetActive(true);
		_mainGroupUI.interactable = true;
	}

	private void CloseMenu()
	{
		_mainGroupUI.gameObject.SetActive(false);
		_mainGroupUI.interactable = false;
	}
}
