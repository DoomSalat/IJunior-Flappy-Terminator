using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Pool;

public class BulletSpawner : MonoBehaviour
{
	[SerializeField] private Bullet _bullet;

	[SerializeField][MinValue(1)] private int _maxPool = 10;

	private ObjectPool<Bullet> _pool;
	private List<Bullet> _activeBullets = new List<Bullet>();

	public ObjectPool<Bullet> Pool => _pool;

	private void Awake()
	{
		_pool = new ObjectPool<Bullet>(
			createFunc: CreateBullet,
			actionOnGet: (obj) => GetBullet(obj),
			actionOnRelease: (obj) => ReleaseBullet(obj),
			actionOnDestroy: DestroyBullet,
			defaultCapacity: _maxPool
		);
	}

	public void Reset()
	{
		for (int i = _activeBullets.Count - 1; i >= 0; i--)
		{
			_pool.Release(_activeBullets[i]);
		}

		_activeBullets.Clear();
	}

	private Bullet CreateBullet()
	{
		var bullet = Instantiate(_bullet);
		bullet.Destroyed += OnBulletDestroyed;

		return bullet;
	}

	private void GetBullet(Bullet bullet)
	{
		bullet.gameObject.SetActive(true);
		_activeBullets.Add(bullet);
	}

	private void ReleaseBullet(Bullet bullet)
	{
		bullet.gameObject.SetActive(false);
		_activeBullets.Remove(bullet);
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
