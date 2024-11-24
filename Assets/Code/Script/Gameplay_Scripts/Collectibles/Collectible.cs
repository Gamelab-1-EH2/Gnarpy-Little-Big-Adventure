using UnityEngine;

namespace Collectible_System
{
    public class Collectible : MonoBehaviour
    {
        [SerializeField] private CollectibleType _type;

        public void Collect()
        {
            this.gameObject.SetActive(false);
        }

        public CollectibleType CollectibleType => _type;
    }
}
