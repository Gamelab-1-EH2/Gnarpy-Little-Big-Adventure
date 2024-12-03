using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CollectibleBar : MonoBehaviour
{
    [SerializeField] private List<Image> _collectibleImages = new List<Image>();
    [SerializeField] private Color _collectedColor = Color.white;
    [SerializeField] private Color _yetToCollectColor = Color.white;
    
    public void SetCollected(int amount)
    {
        amount = Mathf.Clamp(amount, 0, _collectibleImages.Count);

        for (int i = 0; i < amount; i++)
            _collectibleImages[i].color = _collectedColor;

        for (int i = amount; i < _collectibleImages.Count; i++)
            _collectibleImages[i].color = _yetToCollectColor;
    }
}
