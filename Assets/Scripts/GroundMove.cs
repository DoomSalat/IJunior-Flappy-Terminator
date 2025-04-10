using Sirenix.OdinInspector;
using UnityEngine;

public class GroundMove : MonoBehaviour
{
	private const float DestinationThreshold = 0.01f;

	[SerializeField] private Transform _firstTarget;
	[SerializeField] private Transform _secondTarget;
	[Space]
	[SerializeField][MinValue(0)] private float _speed = 5;

	private Vector3 _startPosition;
	private Vector3 _direction;
	private float _totalDistance;
	private Vector3 _currentTargetPosition;

	private void Start()
	{
		_startPosition = transform.localPosition;
		_direction = (_secondTarget.localPosition - _firstTarget.localPosition).normalized;
		_totalDistance = (_secondTarget.localPosition - _firstTarget.localPosition).magnitude;
		_currentTargetPosition = transform.localPosition + (_direction * _totalDistance);
	}

	private void Update()
	{
		transform.localPosition = Vector3.MoveTowards(
			transform.localPosition,
			_currentTargetPosition,
			_speed * Time.deltaTime
		);

		if ((_currentTargetPosition - transform.localPosition).sqrMagnitude < DestinationThreshold * DestinationThreshold)
		{
			transform.localPosition = _startPosition;
		}
	}
}
