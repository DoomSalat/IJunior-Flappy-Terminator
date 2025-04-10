using Sirenix.OdinInspector;
using UnityEngine;

public class AnimateRotationY : MonoBehaviour
{
	[Required][SerializeField] private Rigidbody2D _targetRigidbody;
	[Space]
	[SerializeField][MinValue(0)] private float _speedCoefficientUp = 5;
	[SerializeField][MinValue(0)] private float _speedCoefficientDown = 5;
	[SerializeField][MinValue(0)] private float _smoothCoefficient = 10;
	[SerializeField][MinValue(0)] private float _maxAngle = 65;

	private void Update()
	{
		float targetAngle = 0f;
		float verticalVelocity = _targetRigidbody.linearVelocityY;

		if (verticalVelocity > 0)
		{
			targetAngle = Mathf.Min(_maxAngle, verticalVelocity * _speedCoefficientUp);
		}
		else if (verticalVelocity < 0)
		{
			targetAngle = Mathf.Max(-_maxAngle, verticalVelocity * _speedCoefficientDown);
		}

		transform.rotation = Quaternion.Lerp(
			transform.rotation,
			Quaternion.Euler(0, 0, targetAngle),
			Time.deltaTime * _smoothCoefficient
		);
	}
}
