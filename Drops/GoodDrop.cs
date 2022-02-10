using UnityEngine;

public class GoodDrop : Drop
{
    [SerializeField, Min(1)] private int _points = 1;

    public int Points => _points;
}
