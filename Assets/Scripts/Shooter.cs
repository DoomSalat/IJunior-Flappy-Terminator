using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;

public class Shooter : MonoBehaviour, IRestartListener
{
	[SerializeField] private Bullet _bullet;

	[SerializeField][MinValue(1)] private int _maxPool = 10;

	private GameLive _gameLive;

	private ObjectPool<Bullet> _pool;
	private List<Bullet> _activeBullets = new List<Bullet>();

	private void Awake()
	{
		_pool = new ObjectPool<Bullet>(
			createFunc: CreateBullet,
			actionOnGet: (obj) => obj.gameObject.SetActive(true),
			actionOnRelease: (obj) => obj.gameObject.SetActive(false),
			actionOnDestroy: DestroyBullet,
			defaultCapacity: _maxPool
		);
	}

	private void Start()
	{
		_gameLive = FindFirstObjectByType<GameLive>();

		if (_gameLive != null)
			_gameLive.RegisterListener(this);
	}

	private void OnDestroy()
	{
		if (_gameLive != null)
			_gameLive.UnregisterListener(this);
	}

	public void GameRestart()
	{
		for (int i = _activeBullets.Count - 1; i >= 0; i--)
		{
			_pool.Release(_activeBullets[i]);
		}

		_activeBullets.Clear();
	}

	public void Shoot()
	{
		var bullet = _pool.Get();
		bullet.transform.position = transform.position;

		if (transform.lossyScale.x < 0 && bullet.transform.localScale.x > 0)
		{
			Flip(bullet.transform);
		}

		_activeBullets.Add(bullet);
	}

	private void Flip(Transform target)
	{
		Vector3 scale = target.localScale;
		scale.x *= -1;
		target.localScale = scale;
	}

	private Bullet CreateBullet()
	{
		var bullet = Instantiate(_bullet);
		bullet.Destroyed += OnBulletDestroyed;
		return bullet;
	}

	private void DestroyBullet(Bullet obj)
	{
		obj.Destroyed -= OnBulletDestroyed;
		Destroy(obj.gameObject);
	}

	private void OnBulletDestroyed(Bullet bullet)
	{
		_activeBullets.Remove(bullet);
		_pool.Release(bullet);
	}
}
