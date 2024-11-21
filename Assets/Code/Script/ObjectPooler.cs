using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler
{
    private GameObject _objectToPool;
    private List<GameObject> _objectPool;

    public ObjectPooler(GameObject objectToPool, int initialPool)
    {
        _objectToPool = objectToPool;
        _objectPool = new List<GameObject>();

        for(int i = 0; i < initialPool; i++)
        {
            _objectPool.Add(MonoBehaviour.Instantiate(_objectToPool));
            _objectPool[^1].gameObject.SetActive(false);
        }
    }

    public GameObject PoolObject()
    {
        for (int i = 0; i < _objectPool.Count; i++)
            if (!_objectPool[i].activeInHierarchy)
                return _objectPool[i];

        _objectPool.Add(MonoBehaviour.Instantiate(_objectToPool));
        return _objectPool[^1];
    }
}
