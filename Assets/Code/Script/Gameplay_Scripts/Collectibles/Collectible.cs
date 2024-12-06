using UnityEngine;
using System.Collections;

namespace Collectible_System
{
    public class Collectible : MonoBehaviour
    {
        [SerializeField] private CollectibleType _type;
        [SerializeField] private bool enableRespawn = false;
        [SerializeField] private float respawnTime = 5f;

        private Collider _collider;
        private GameObject _childObject;

        private void Awake()
        {
            _collider = GetComponent<Collider>();

            if (transform.childCount > 0)
            {
                _childObject = transform.GetChild(0).gameObject;
            }
            else
            {
                Debug.LogError("No child", this);
            }
        }

        public void Collect()
        {
            DisableCollectible();

            if (enableRespawn)
            {
                StartCoroutine(RespawnAfterDelay(respawnTime));
            }
        }

        private void DisableCollectible()
        {
            if (_collider != null)
                _collider.enabled = false;

            if (_childObject != null)
                _childObject.SetActive(false);
        }

        private void EnableCollectible()
        {
            if (_collider != null)
                _collider.enabled = true;

            if (_childObject != null)
                _childObject.SetActive(true);
        }

        private IEnumerator RespawnAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);

            EnableCollectible();
        }

        public CollectibleType CollectibleType => _type;
    }
        //public void Collect()
        //{
        //    this.gameObject.SetActive(false);
        //}

        //public CollectibleType CollectibleType => _type;
}