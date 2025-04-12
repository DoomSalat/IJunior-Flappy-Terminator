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
		_spawner.Killed += Add;
	}

	private void OnDisable()
	{
		_spawner.Killed -= Add;
	}

	public void Reset()
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
