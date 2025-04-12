using UnityEngine;

public class Score : MonoBehaviour, IRestartListener
{
	private const int AddOne = 1;

	[SerializeField] private EnemySpawner _spawner;

	private GameLive _gameLive;
	private int _value = 0;

	public event System.Action<int> Updated;

	public int Value => _value;

	private void Start()
	{
		_gameLive = FindAnyObjectByType<GameLive>();

		if (_gameLive != null)
			_gameLive.RegisterListener(this);
	}

	private void OnDestroy()
	{
		if (_gameLive != null)
			_gameLive.UnregisterListener(this);
	}

	private void OnEnable()
	{
		_spawner.Killed += Add;
	}

	private void OnDisable()
	{
		_spawner.Killed -= Add;
	}

	public void GameRestart()
	{
		_value = 0;
		Updated?.Invoke(_value);
	}

	private void Add()
	{
		_value += AddOne;
		Updated?.Invoke(_value);
	}
}
