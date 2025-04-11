using Sirenix.OdinInspector;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Bullet : MonoBehaviour, IDestroyed
{
	[SerializeField][MinValue(0)] private float _speed = 5;

	private Vector2 _direction;

	private Rigidbody2D _rigidbody;

	public event Action<Bullet> Destroyed;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		_rigidbody.linearVelocity = (transform.lossyScale.x > 0 ? transform.right : -transform.right) * _speed;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.TryGetComponent<Danger>(out var danger))
		{
			Destroy();
		}
	}

	public void Initializate(Vector2 direction)
	{
		_direction = direction.normalized;
		float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0, 0, angle);
	}

	public void Destroy()
	{
		Destroyed?.Invoke(this);
	}
}
