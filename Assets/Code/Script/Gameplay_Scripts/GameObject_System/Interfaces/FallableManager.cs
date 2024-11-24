using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FallableManager
{
    private List<IFallable> _fallableList;

    public void Start()
    {
        _fallableList = new List<IFallable>();
        _fallableList.AddRange(MonoBehaviour.FindObjectsOfType<MonoBehaviour>().OfType<IFallable>().ToArray() );
    }

    public void FixedUpdate()
    {
        for (int i = 0; i < _fallableList.Count; i++)
            _fallableList[i].UpdateFall();
    }
}
