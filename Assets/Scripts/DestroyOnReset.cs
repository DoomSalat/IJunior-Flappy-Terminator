using UnityEngine;

public class DestroyOnReset : MonoBehaviour
{
	private void OnEnable()
	{
		GameLive.Restarted += Destroy;
	}

	private void OnDisable()
	{
		GameLive.Restarted -= Destroy;
	}

	public void Destroy()
	{
		Destroy(gameObject);
	}
}
