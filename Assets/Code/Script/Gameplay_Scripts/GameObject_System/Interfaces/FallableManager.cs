using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FallableManager
{
    private List<IFallable> _fallableList;

    public FallableManager()
    {
        _fallableList = new List<IFallable>();
    }

    public void Start()
    {
        _fallableList.AddRange(MonoBehaviour.FindObjectsOfType<MonoBehaviour>(true).OfType<IFallable>().ToArray() );
    }

    public void FixedUpdate()
    {
        for (int i = 0; i < _fallableList.Count; i++)
            _fallableList[i].UpdateFall();
    }
}
