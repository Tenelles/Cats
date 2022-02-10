using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class ItemPool<T> where T : MonoBehaviour
{
    private List<T> _items;
    private T _prefab;
    private Transform _container;
    private bool _isExpandable;

    public bool IsExpandable => _isExpandable;

    public ItemPool(T itemPrefab, bool isExpandable = false)
    {
        if (itemPrefab == null)
            throw new ArgumentNullException();
        
        _prefab = itemPrefab;
        _isExpandable = isExpandable;
        _items = new List<T>();
    }

    public void Initialize(int capacity, Transform container = null)
    {
        if (capacity < 0)
            throw new ArgumentOutOfRangeException(nameof(capacity));
        
        if (_items.Count > 0)
            Clear();

        _container = container;
        for (int i = 0; i < capacity; i++)
            CreateObject();
    }
    
    public void Clear()
    {
        foreach (T item in _items)
            Object.Destroy(item);
        
        _items.Clear();
    }

    public bool HasFreeElement(out T element)
    {
        foreach (T item in _items)
            if (!item.gameObject.activeInHierarchy)
            {
                item.gameObject.SetActive(true);
                element = item;
                return true;
            }

        element = null;
        return false;
    }

    public T GetFreeElement()
    {
        if (HasFreeElement(out T element))
            return element;

        if (_isExpandable)
            return CreateObject(true);

        throw new StackOverflowException($"There is no free elements in pool of type {typeof(T)}");
    }

    ~ItemPool() => Clear();

    private T CreateObject(bool isActive = false)
    {
        T createdObject = Object.Instantiate(_prefab, _container);
        createdObject.gameObject.SetActive(isActive);
        _items.Add(createdObject);
        return createdObject;
    }

}