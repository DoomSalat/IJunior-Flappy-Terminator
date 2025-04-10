using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BirdAnimator : MonoBehaviour
{
	private const float AnimatorSpeedDefault = 1;

	private Animator _animator;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	public void Play()
	{
		_animator.speed = AnimatorSpeedDefault;
	}

	public void Stop()
	{
		_animator.speed = 0f;
	}
}
