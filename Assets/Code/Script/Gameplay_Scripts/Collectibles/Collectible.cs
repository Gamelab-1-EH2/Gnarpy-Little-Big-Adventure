using UnityEngine;

namespace Collectible_System
{
    public class Collectible : MonoBehaviour
    {
        public void Collect()
        {
            this.gameObject.SetActive(false);
        }
    }
}
