using Sirenix.OdinInspector;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[Required][SerializeField] private FlappyMove _flappyMove;
	[Required][SerializeField] private Shooter _shooter;
	[Required][SerializeField] private Vision _vision;

	[SerializeField] private float _visionDelay = 1f;
	[SerializeField] private float _ceilingOffset = 0.5f;
	[SerializeField] private float _minCeilingDistance = 0.5f;

	public event System.Action Died;

	private void OnEnable()
	{
		_vision.enabled = false;
		Invoke(nameof(EnableVision), _visionDelay);
	}

	private void Update()
	{
		if (CanJump())
		{
			float jumpStrength = CalculateJumpStrength();
			//Debug.Log($"Jump strength: {jumpStrength}, Ceiling distance: {_vision.GetCeilingDistance()}, Predicted height: {PredictJumpHeight(jumpStrength * ImpulseForce, _flappyMove.Rigidbody.linearVelocity.y)}");

			_flappyMove.FlapUp(jumpStrength);
			_shooter.Shoot();
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.TryGetComponent<Danger>(out var danger))
		{
			Death();
		}
	}

	private void EnableVision()
	{
		_vision.enabled = true;
	}

	private float CalculateJumpStrength()
	{
		float ceilingDistance = _vision.GetCeilingDistance();
		float targetHeight = ceilingDistance - _ceilingOffset;

		float currentVelocityY = _flappyMove.Rigidbody.linearVelocity.y;
		float requiredVelocity = Mathf.Sqrt(2 * Physics2D.gravity.magnitude * targetHeight);

		float jumpStrength = requiredVelocity - currentVelocityY;

		return jumpStrength > 0 ? jumpStrength : 0;
	}

	private bool CanJump()
	{
		if (IsCeilingTooClose(_minCeilingDistance))
			return false;

		return _flappyMove.Rigidbody.linearVelocity.y < 0 &&
				(_vision.CheckGround() || _vision.CheckPlayer());
	}

	private bool IsCeilingTooClose(float minSafeDistance)
	{
		return _vision.GetCeilingDistance() < minSafeDistance;
	}

	private void Death()
	{
		_flappyMove.StopMove();
		Died?.Invoke();
	}
}