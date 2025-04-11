using UnityEngine;

public class Menu : MonoBehaviour
{
	[SerializeField] private CanvasGroup _mainGroupUI;

	private void Start()
	{
		CloseMenu();
	}

	public void OpenMenu()
	{
		_mainGroupUI.gameObject.SetActive(true);
		_mainGroupUI.interactable = true;
	}

	public void CloseMenu()
	{
		_mainGroupUI.gameObject.SetActive(false);
		_mainGroupUI.interactable = false;
	}
}
