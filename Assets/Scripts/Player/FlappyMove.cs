using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FlappyMove : MonoBehaviour
{
	[SerializeField][MinValue(0)] private float _impulse = 5;

	private Rigidbody2D _rigidbody;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
	}

	public void FlapUp()
	{
		_rigidbody.AddForce(Vector2.up * _impulse, ForceMode2D.Impulse);
	}

	public void StopMove()
	{
		_rigidbody.linearVelocity = Vector2.zero;
	}
}
