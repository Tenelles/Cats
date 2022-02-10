using System;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public event Action Died;
    public event Action ReceivedDamage;
    public event Action<int> GotPoints;

    [SerializeField, Min(1)] private int _maxHp = 10;
    [SerializeField] private KeyboardMovement _movement;
    [SerializeField] private Animator _animator;

    private readonly string _startedMovementTrigger = "StartedMovement";
    private readonly string _finishedMovementTrigger = "FinishedMovement";

    private int _hp;

    public int MaxHp => _maxHp;
    public int Hp => _hp;

    public void Initialize()
    {
        _hp = _maxHp;
    }

    private void Awake()
    {
        _movement.Started += OnMovementStarted;
        _movement.Finished += OnMovementFinished;

        Initialize();
    }

    private void OnDestroy()
    {
        _movement.Started -= OnMovementStarted;
        _movement.Finished -= OnMovementFinished;
    }

    private void ReceiveDamage()
    {
        _hp--;
        ReceivedDamage?.Invoke();
        if (_hp <= 0)
            Died?.Invoke();
    }

    private void GetPoints(int points)
    {
        GotPoints?.Invoke(points);
    }

    private void OnMovementStarted() => _animator.SetTrigger(_startedMovementTrigger);
    
    private void OnMovementFinished() => _animator.SetTrigger(_finishedMovementTrigger);

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.TryGetComponent(out BadDrop _))
            ReceiveDamage();
        if (other.collider.TryGetComponent(out GoodDrop goodDrop))
            GetPoints(goodDrop.Points);
    }
}
