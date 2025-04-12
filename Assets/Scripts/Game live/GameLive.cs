using UnityEngine;

public class GameLive : MonoBehaviour
{
	private const float ScaleTimeDefault = 1;

	[SerializeField] private Menu _mainMenu;
	[Space]
	[SerializeField] private Player _player;
	[SerializeField] private Transform _playerDefaultPosition;
	[SerializeField] private EnemySpawner _enemySpawner;
	[SerializeField] private Score _score;
	[SerializeField] private BulletSpawner[] _bulletSpawners;

	private void OnEnable()
	{
		_player.Died += StopGame;
	}

	private void OnDisable()
	{
		_player.Died -= StopGame;
	}

	public void RestartGame()
	{
		_mainMenu.Close();

		Time.timeScale = ScaleTimeDefault;
		_player.transform.position = _playerDefaultPosition.position;
		_player.ResetState();
		_score.Reset();
		_enemySpawner.Reset();

		foreach (BulletSpawner spawner in _bulletSpawners)
		{
			spawner.Reset();
		}
	}

	private void StopGame()
	{
		Time.timeScale = 0f;
		_mainMenu.Open();
	}
}
