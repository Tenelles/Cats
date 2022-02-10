using System;
using UnityEngine;

public class Game : MonoBehaviour
{
    public event Action<int> ScoreChanged;
    
    [SerializeField] private Cat _cat;
    private int _score;
    
    private void Awake()
    {
        _cat.GotPoints += SetScore;
        _cat.Died += GameOver;
        SetScore(0);
    }
    
    private void SetScore(int score)
    {
        _score += score;
        ScoreChanged?.Invoke(_score);
    }

    private void GameOver()
    {
        throw new NotImplementedException();
    }

    private void OnDestroy()
    {
        _cat.GotPoints -= SetScore;
        _cat.Died -= GameOver;
    }

}
