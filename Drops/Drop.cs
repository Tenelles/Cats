using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Drop : MonoBehaviour
{
    [SerializeField, Range(1f, 20f)] private float _velocity = 10f;

    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MoveDown();
    }

    private void MoveDown()
    {
        if (gameObject.activeInHierarchy)
            _rigidbody2D.velocity = Vector2.down * _velocity;
            
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.TryGetComponent(out Cat _))
            Disable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Deathzone _))
            Disable();
    }
    
    private void Disable() => gameObject.SetActive(false);
}
