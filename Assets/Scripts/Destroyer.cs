using UnityEngine;

public class Destroyer : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent<IDestroyed>(out var destroyed))
		{
			destroyed.Destroy();
		}
	}
}
