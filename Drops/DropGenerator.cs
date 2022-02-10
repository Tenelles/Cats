using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DropGenerator : MonoBehaviour
{
    [SerializeField, Min(1)] private int _poolSize = 10;
    [SerializeField] private Drop[] _dropPrefabs;
    [SerializeField] private Vector2 _offset;
    [SerializeField, Min(0.1f)] private float _delay;

    private List<ItemPool<Drop>> _dropPools;
    private Coroutine _coroutine;
    
    
    public void Resume()
    {
        if (_coroutine != null)
            throw new Exception("Coroutine is already working");
        _coroutine = StartCoroutine(GenerateCoroutine());
    }

    public void Pause()
    {
        if (_coroutine == null)
            throw new Exception("Coroutine isn`t started");
        StopCoroutine(_coroutine);
        _coroutine = null;
    }
    
    private void Awake()
    {
        _dropPools = new List<ItemPool<Drop>>();
        foreach (Drop drop in _dropPrefabs)
            InitializePool(drop);
    }

    private void Start()
    {
        Resume();
    }

    private void InitializePool(Drop drop)
    {
        ItemPool<Drop> pool = new ItemPool<Drop>(drop, true);
        pool.Initialize(_poolSize, transform);
        _dropPools.Add(pool);
    }

    private IEnumerator GenerateCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_delay);
            Generate();
        }
    }

    private void Generate()
    {
        int poolIndex = Random.Range(0, _dropPools.Count);
        Drop drop = _dropPools[poolIndex].GetFreeElement();
        Vector3 position = GetRandomPositionInOffsetRange();
        drop.transform.localPosition = position;
        drop.transform.Rotate(Vector3.forward, Random.Range(0f, 360f));

        Vector3 GetRandomPositionInOffsetRange()
        {
            float x = Random.Range(-_offset.x, _offset.x);
            float y = Random.Range(-_offset.y, _offset.y);
            return new Vector3(x, y, 0);
        }
    }
}
