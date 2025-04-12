using System.Collections.Generic;
using UnityEngine;

public class GameLive : MonoBehaviour
{
	private const float ScaleTimeDefault = 1;

	[SerializeField] private Menu _mainMenu;
	[Space]
	[SerializeField] private Player _player;
	[SerializeField] private Transform _playerDefaultPosition;

	private List<IRestartListener> _listeners = new List<IRestartListener>();

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}

	private void OnEnable()
	{
		_player.Died += StopGame;
	}

	private void OnDisable()
	{
		_player.Died -= StopGame;
	}

	public void RegisterListener(IRestartListener listener)
	{
		if (_listeners.Contains(listener) == false)
		{
			_listeners.Add(listener);
		}
	}

	public void UnregisterListener(IRestartListener listener)
	{
		_listeners.Remove(listener);
	}

	public void RestartGame()
	{
		_mainMenu.Close();

		Time.timeScale = ScaleTimeDefault;
		_player.transform.position = _playerDefaultPosition.position;
		_player.ResetState();

		foreach (var listener in _listeners)
		{
			listener.GameRestart();
		}
	}

	private void StopGame()
	{
		Time.timeScale = 0f;
		_mainMenu.Open();
	}
}
