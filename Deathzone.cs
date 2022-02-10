using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Deathzone : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
}