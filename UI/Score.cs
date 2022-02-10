using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Score : MonoBehaviour
{
    [SerializeField] private Game _game;
    private Text _text;

    private void Awake()
    {
        _game.ScoreChanged += UpdateText;
        _text = GetComponent<Text>();
    }

    private void UpdateText(int value)
    {
        _text.text = $"{value}";
    }

    private void OnDestroy()
    {
        _game.ScoreChanged -= UpdateText;
    }

}
