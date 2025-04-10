using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
	private MainActionSystem _inputActions;

	public event System.Action Jumped;

	private void Awake()
	{
		_inputActions = new MainActionSystem();
	}

	private void OnEnable()
	{
		_inputActions.Player.Jump.performed += OnJumpPerformed;
		Enable();
	}

	private void OnDisable()
	{
		Disable();
		_inputActions.Player.Jump.performed -= OnJumpPerformed;
	}

	public void Enable()
	{
		_inputActions.Enable();
	}

	public void Disable()
	{
		_inputActions.Disable();
	}

	private void OnJumpPerformed(InputAction.CallbackContext context)
	{
		Jumped?.Invoke();
	}
}
