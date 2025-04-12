using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreUI : MonoBehaviour
{
	[SerializeField] private Score _score;

	private TextMeshProUGUI _textMeshPro;

	private void Awake()
	{
		_textMeshPro = GetComponent<TextMeshProUGUI>();
	}

	private void OnEnable()
	{
		OnChange(_score.Value);

		_score.Updated += OnChange;
	}

	private void OnDisable()
	{
		_score.Updated -= OnChange;
	}

	public void OnChange(int value)
	{
		_textMeshPro.text = value.ToString();
	}
}
