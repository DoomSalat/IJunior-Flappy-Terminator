using UnityEngine;

public class Vision : MonoBehaviour
{
	[SerializeField] private LayerMask _groundLayer;
	[SerializeField] private LayerMask _playerLayer;
	[SerializeField] private LayerMask _ceilingLayer;
	[SerializeField] private float _groundCheckDistance = 2f;
	[SerializeField] private float _playerCheckDistance = 5f;
	[SerializeField] private float _ceilingCheckDistance = 3f;
	[SerializeField] private Transform _groundCheckPoint;
	[SerializeField] private Transform _playerCheckPoint;
	[SerializeField] private Transform _ceilingCheckPoint;

	public bool CheckGround()
	{
		if (enabled == false)
			return false;

		RaycastHit2D groundHit = Physics2D.Raycast(_groundCheckPoint.position, Vector2.down, _groundCheckDistance, _groundLayer);
		return groundHit.collider != null;
	}

	public bool CheckPlayer()
	{
		if (enabled == false)
			return false;

		RaycastHit2D playerHit = Physics2D.Raycast(_playerCheckPoint.position, Vector2.left, _playerCheckDistance, _playerLayer);
		return playerHit.collider != null;
	}

	public float GetCeilingDistance()
	{
		RaycastHit2D ceilingHit = Physics2D.Raycast(_ceilingCheckPoint.position, Vector2.up, _ceilingCheckDistance, _ceilingLayer);
		return ceilingHit.collider != null ? ceilingHit.distance : _ceilingCheckDistance;
	}

	private void OnDrawGizmos()
	{
		if (_groundCheckPoint != null)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawRay(_groundCheckPoint.position, Vector2.down * _groundCheckDistance);
		}

		if (_playerCheckPoint != null)
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawRay(_playerCheckPoint.position, Vector2.left * _playerCheckDistance);
		}

		if (_ceilingCheckPoint != null)
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawRay(_ceilingCheckPoint.position, Vector2.up * _ceilingCheckDistance);
		}
	}
}