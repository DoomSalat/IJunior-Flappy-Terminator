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
		NewValue(_score.Value);

		_score.Updated += NewValue;
	}

	private void OnDisable()
	{
		_score.Updated -= NewValue;
	}

	public void NewValue(int value)
	{
		_textMeshPro.text = value.ToString();
	}
}
