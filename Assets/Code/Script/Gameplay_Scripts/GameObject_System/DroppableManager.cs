using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppableManager : MonoBehaviour
{
    [SerializeField] private List<DropData> _dropData = new List<DropData>();

    private List<ObjectPooler> poolers = new List<ObjectPooler>();

    private float[] _probabilityList;

    private void Start()
    {
        if (_dropData.Count <= 0)
            return;

        //Initialize pooler
        for(int i = 0; i < _dropData.Count; i++)
            poolers.Add(new ObjectPooler(_dropData[i].ObjectToSpawn, 15));

        //Weighted random Data
        _probabilityList = new float[_dropData.Count];
        _probabilityList[0] = _dropData[0].Probability;

        for (int i=1; i<_probabilityList.Length; i++)
            _probabilityList[i] = _probabilityList[i-1] + _dropData[i].Probability;
        
        //Connect Events
        DestroyableObject[] destroyableObjects = FindObjectsOfType<DestroyableObject>();
        for(int i = 0; i < destroyableObjects.Length; i++)
        {
            if (destroyableObjects[i].CanDrop)
                destroyableObjects[i].OnDestroy += Drop;
        }
    }

    private void Drop(DestroyableObject destroyable)
    {
        float randomValue = UnityEngine.Random.value;
        for(int i = 0;i < _probabilityList.Length; i++)
        {
            if(randomValue < _probabilityList[i])
            {
                GameObject obj = poolers[i].PoolObject();
                obj.transform.position = destroyable.transform.position;
                obj.gameObject.SetActive(true);
            }
        }
    }
}

[System.Serializable]
public class DropData
{
    [SerializeField] public GameObject ObjectToSpawn;
    [SerializeField, Range(0f, 1f)] public float Probability;
}