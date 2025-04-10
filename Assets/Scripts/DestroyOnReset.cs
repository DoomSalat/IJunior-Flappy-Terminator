using UnityEngine;

public class DestroyOnReset : MonoBehaviour
{
	private void OnEnable()
	{
		GameLive.Reseted += Destroy;
	}

	private void OnDisable()
	{
		GameLive.Reseted -= Destroy;
	}

	public void Destroy()
	{
		Destroy(gameObject);
	}
}
