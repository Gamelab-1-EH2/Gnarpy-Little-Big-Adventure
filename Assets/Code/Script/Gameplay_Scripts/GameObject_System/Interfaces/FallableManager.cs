using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FallableManager : MonoBehaviour
{
    private List<IFallable> _fallableList;

    private void Awake()
    {
        _fallableList = new List<IFallable>();
    }

    private void Start()
    {
        _fallableList.AddRange(FindObjectsOfType<MonoBehaviour>(true).OfType<IFallable>().ToArray() );
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < _fallableList.Count; i++)
            _fallableList[i].UpdateFall();
    }
}
