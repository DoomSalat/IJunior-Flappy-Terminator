using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Player : MonoBehaviour
{
	[Required][SerializeField] private InputHandler _inputHandler;
	[Required][SerializeField] private BirdAnimator _animator;

	[Required][SerializeField] private FlappyMove _flappyMove;
	[Required][SerializeField] private Shooter _shooter;

	public event System.Action Died;

	private void OnEnable()
	{
		_inputHandler.Jumped += Flap;
	}

	private void OnDisable()
	{
		_inputHandler.Jumped -= Flap;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.TryGetComponent<Danger>(out var danger))
		{
			Death();
		}
	}

	public void ResetState()
	{
		_flappyMove.StopMove();
		_animator.Play();
		_inputHandler.Enable();
	}

	private void Flap()
	{
		_flappyMove.FlapUp();
		_shooter.Shoot();
	}

	private void Death()
	{
		_inputHandler.Disable();
		_animator.Stop();
		Died?.Invoke();
	}
}
