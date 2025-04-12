using UnityEngine;

public class Shooter : MonoBehaviour
{
	[SerializeField] private BulletSpawner _bulletSpawner;

	public void Shoot()
	{
		var bullet = _bulletSpawner.Pool.Get();
		bullet.transform.position = transform.position;

		if (transform.lossyScale.x < 0 && bullet.transform.localScale.x > 0)
		{
			Flip(bullet.transform);
		}
	}

	public void Initialization(BulletSpawner bulletSpawner)
	{
		_bulletSpawner = bulletSpawner;
	}

	private void Flip(Transform target)
	{
		Vector3 scale = target.localScale;
		scale.x *= -1;
		target.localScale = scale;
	}
}
