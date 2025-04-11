using UnityEngine;

public class Score : MonoBehaviour
{
	private const int AddOne = 1;

	[SerializeField] private EnemySpawner _spawner;

	private int _value = 0;

	public event System.Action<int> Updated;

	public int Value => _value;

	private void OnEnable()
	{
		GameLive.Restarted += Reset;
		_spawner.Killed += Add;
	}

	private void OnDisable()
	{
		GameLive.Restarted -= Reset;
		_spawner.Killed -= Add;
	}

	private void Add()
	{
		_value += AddOne;
		Updated?.Invoke(_value);
	}

	private void Reset()
	{
		_value = 0;
		Updated?.Invoke(_value);
	}
}
