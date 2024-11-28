using UnityEngine;
using System.Collections.Generic;

using Player;

namespace Turret_System
{
    [System.Serializable]
    public class TurretManager
    {
        [SerializeField] private GameObject _turretProjectile;
        [SerializeField] private float _projectileSpeed;

        private List<Turret> _turretList;
        private ObjectPooler _projectilePooler;

        private Transform _turretCommonTarget;

        public void Start(Transform spawnParent)
        {
            _projectilePooler = new ObjectPooler(_turretProjectile, 25, spawnParent);

            _turretList = new List<Turret>();
            _turretList.AddRange(MonoBehaviour.FindObjectsOfType<Turret>());

            _turretCommonTarget = MonoBehaviour.FindObjectOfType<PlayerController>().transform;

            for (int i = 0; i < _turretList.Count; i++)
            {
                _turretList[i].SetTarget(_turretCommonTarget);
                _turretList[i].OnDestroy += RemoveTurret;
                _turretList[i].OnShoot += Shoot;
                _turretList[i].Index = i;
            }
        }

        public void Process()
        {
            for(int i = 0; i < _turretList.Count; i++)
                _turretList[i].Tick();
        }

        public void RemoveTurret(DestroyableObject destroyable)
        {
            Turret turretToRemove = (Turret)destroyable;
            _turretList.RemoveAt(turretToRemove.Index);

            //Update all Indexes
            for (int i = 0; i < _turretList.Count; i++)
                _turretList[i].Index = i;
        }

        private void Shoot(Transform turretTransform, float shootAngle)
        {
            Projectile projectile = _projectilePooler.PoolObject().GetComponent<Projectile>();
            projectile.transform.position = turretTransform.position;
            projectile.Shoot(new Vector3(shootAngle, -90f, 0f), _projectileSpeed);
            projectile.gameObject.SetActive(true);
        }
    }
}