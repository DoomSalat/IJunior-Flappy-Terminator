using UnityEngine;
using UnityEngine.Pool;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] private Enemy _prefab;
	[SerializeField] private BulletSpawner _bulletSpawner;
	[Space]
	[SerializeField] private int _poolSize = 5;
	[SerializeField] private Transform[] _spawnPoints;
	[SerializeField] private float _spawnDelay = 2f;

	private ObjectPool<Enemy> _pool;
	private bool _isActive;
	private Enemy _currentEnemy;
	private Coroutine _spawnCoroutine;

	public event System.Action Killed;

	private void Awake()
	{
		InitializePool();
		SpawnEnemy();
	}

	private void OnDestroy()
	{
		_pool.Dispose();
	}

	public void Reset()
	{
		if (_currentEnemy != null)
		{
			_pool.Release(_currentEnemy);
			_currentEnemy = null;
		}

		_isActive = false;
		SpawnEnemy();
	}

	private void InitializePool()
	{
		_pool = new ObjectPool<Enemy>(
			createFunc: CreateEnemy,
			actionOnGet: OnEnemyGet,
			actionOnRelease: (obj) => obj.gameObject.SetActive(false),
			actionOnDestroy: OnEnemyDestroy,
			defaultCapacity: _poolSize,
			maxSize: _poolSize * 2
		);
	}

	private Enemy CreateEnemy()
	{
		Enemy enemy = Instantiate(_prefab, _spawnPoints[0].position, Quaternion.identity);
		enemy.Initialization(_bulletSpawner);

		enemy.Died += OnEnemyDied;

		return enemy;
	}

	private Vector3 RandomSpawnPoint()
	{
		return _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
	}

	private void OnEnemyGet(Enemy enemy)
	{
		enemy.gameObject.SetActive(true);
		enemy.transform.position = RandomSpawnPoint();
	}

	private void OnEnemyDestroy(Enemy enemy)
	{
		if (enemy != null)
		{
			enemy.Died -= OnEnemyDied;
			Destroy(enemy.gameObject);
		}
	}

	private void SpawnEnemy()
	{
		if (_isActive)
			return;

		if (_spawnCoroutine != null)
			StopCoroutine(_spawnCoroutine);

		_spawnCoroutine = StartCoroutine(SpawnEnemyWithDelay());
	}

	private IEnumerator SpawnEnemyWithDelay()
	{
		yield return new WaitForSeconds(_spawnDelay);
		_currentEnemy = _pool.Get();
		_isActive = true;
	}

	private void OnEnemyDied()
	{
		_isActive = false;

		if (_currentEnemy != null)
		{
			_pool.Release(_currentEnemy);
			_currentEnemy = null;

			Killed?.Invoke();
		}

		SpawnEnemy();
	}
}
