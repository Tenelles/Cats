using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(GridLayoutGroup))]
    public class Hp : MonoBehaviour
    {
        [SerializeField] private Sprite _fullHeart;
        [SerializeField] private Sprite _emptyHeart;
        [Space(10)] [SerializeField] private Cat _cat;

        private void Awake()
        {
            _cat.ReceivedDamage += OnCatReceivedDamage;
        }

        private void Start()
        {
            UpdateHearts();
        }

        private void OnCatReceivedDamage() => UpdateHearts();

        private void UpdateHearts()
        {
            ClearChildren();
            for (int i = 0; i < _cat.MaxHp; i++)
                AddHeart(sprite: i < _cat.Hp ? _fullHeart : _emptyHeart);
        }
        
        private void AddHeart(Sprite sprite)
        {
            GameObject heart = new GameObject("Heart");
            heart.transform.SetParent(transform);
            Image image = heart.AddComponent<Image>();
            image.sprite = sprite;
        }

        private void ClearChildren()
        {
            foreach (Transform child in transform)
                Destroy(child.gameObject);
        }

        private void OnDestroy()
        {
            _cat.ReceivedDamage -= OnCatReceivedDamage;
        }
    }
}