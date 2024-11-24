using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Turret_System
{
    public class Turret: MonoBehaviour
    {
        public Action<Transform, float> OnShoot;

        [SerializeField] private float ViewAngle = 90f;
        [SerializeField] private float ViewDistance = 5.0f;
    
        private Transform _target;
        
        public void Tick()
        {
            float dist = (transform.position - _target.position).magnitude;
            if (dist > ViewDistance)
                return;

            float angle = Vector3.Angle(transform.position - _target.position, transform.right);

            if (angle <= ViewAngle / 2)
            {
                angle *= _target.position.y < transform.position.y ? 1 : -1;
                OnShoot?.Invoke(transform, angle);
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
            Handles.DrawWireDisc(transform.position, Vector3.forward, ViewDistance);
        }
        #endif
    }
}