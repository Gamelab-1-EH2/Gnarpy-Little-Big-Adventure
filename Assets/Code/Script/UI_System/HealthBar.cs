using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Sprite fullHealt;
    [SerializeField] private Sprite emptyHealt;
    [SerializeField] private List<Image> _healts = new List<Image>();
    
    public void SetHealth(int hp)
    {
        hp = Mathf.Clamp(hp, 0, _healts.Count);

        for (int i = 0; i < hp; i++)
            _healts[i].sprite = fullHealt;

        for(int i = hp; i < _healts.Count; i++)
            _healts[i].sprite = emptyHealt;
    }
}
