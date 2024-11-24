using System;
using UnityEngine;
using Player.Model;


#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Turret_System
{
    public class Turret: MonoBehaviour
    {
        public Action<Transform, float> OnShoot;

        [SerializeField] private float _delay = 1f; 
        [SerializeField] private float _viewAngle = 90f;
        [SerializeField] private float _viewDistance = 5.0f;

        private float _lastTimeShoot = 0f;
        private Transform _target;
        
        public void Tick()
        {
            float dist = (transform.position - _target.position).magnitude;
            if (dist > _viewDistance)
                return;

            float enlapsed = Time.time - _lastTimeShoot;

            if (enlapsed >= _delay)
            {
                float angle = Vector3.Angle(transform.position - _target.position, transform.right);
                if (angle <= _viewAngle / 2)
                {
                    angle *= _target.position.y < transform.position.y ? 1 : -1;
                    OnShoot?.Invoke(transform, angle);

                    _lastTimeShoot = Time.time;
                }
            }
        }

        public void SetTarget(Transform target) => _target = target;

        private void OnDestroy()
        {
            OnShoot -= OnShoot;
        }

        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Handles.color = Color.white;
            Handles.DrawWireDisc(transform.position, Vector3.forward, _viewDistance);
        }
        #endif
    }
}