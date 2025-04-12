using UnityEngine;

public class Menu : MonoBehaviour
{
	[SerializeField] private CanvasGroup _mainGroupUI;

	private void Start()
	{
		Close();
	}

	public void Open()
	{
		_mainGroupUI.gameObject.SetActive(true);
		_mainGroupUI.interactable = true;
	}

	public void Close()
	{
		_mainGroupUI.gameObject.SetActive(false);
		_mainGroupUI.interactable = false;
	}
}
