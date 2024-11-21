using Player.Model;

using UnityEngine;

namespace Collectible_System.PowerUp
{
    public class Red_PowerUp : PowerUp
    {

        public Red_PowerUp(PlayerModel model) : base(model)
        {

        }
        
        public override void Start()
        {
            if(!base._isUnlocked || base._isBeingUsed)
                return;

            base._isBeingUsed = true;
        }

        public override void Update()
        {
            if (!base._isUnlocked || !base._isBeingUsed)
                return;

            Vector3 worldPos = base._playerModel.Movement.RigidBody.transform.position;
            float radious = base._playerModel.PowerUp.RedPowerUpRadious;

            Collider[] colliders = Physics.OverlapSphere(worldPos, radious, 1<<8);
            for(int i = 0; i < colliders.Length; i++)
            {
                if(colliders[i].TryGetComponent<MovableObject>(out MovableObject movable))
                    movable.ApplyGravity(Vector3.up, base._playerModel.PowerUp.RedPowerUpStrenght);
            }
        }

    }
}
