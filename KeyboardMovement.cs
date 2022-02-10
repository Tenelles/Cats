using System;
using UnityEngine;

public class KeyboardMovement : MonoBehaviour
{
    public event Action Started;
    public event Action Finished; 
    
    [SerializeField, Range(1f, 10f)] private float _speed = 3f;
    [Space]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private bool _isLookingLeft = false;

    private bool _isMoving = false;

    private void Update()
    {
        int xInput = (int)Input.GetAxis("Horizontal");

        if (xInput != 0)
        {
            StartMoving();
            Move(xInput);
        }
        else
            StopMoving();
    }

    private void Move(int xInput)
    {
        _spriteRenderer.flipX = IsNeedFlip(xInput);
        
        transform.Translate(Vector3.right * (_speed * xInput * Time.deltaTime));
        
        bool IsNeedFlip(int x) => (x > 0 && _isLookingLeft) || (x < 0 && !_isLookingLeft);
    }

    private void StopMoving()
    {
        if (!_isMoving) return;
        
        Finished?.Invoke();
        _isMoving = false;
    }

    private void StartMoving()
    {
        if (_isMoving) return;
        
        Started?.Invoke();
        _isMoving = true;
    }
}
