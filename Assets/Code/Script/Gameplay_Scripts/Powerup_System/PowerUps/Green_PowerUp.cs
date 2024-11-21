using Player.Model;
using UnityEngine;

namespace Collectible_System.PowerUp
{
    /*
    La green strawberry, una volta raccolta, permette al personaggio di richiamare uno scudo circolare attorno a sé che permette di
    Rimbalzare oggetti distruttibili: Se il boss lancia un attacco a distanza e il giocatore attiva lo scudo
    l’oggetto lanciato dal nemico sarà rispedito al mittente e verrà riflesso sempre in modo speculare rispetto alla traiettoria verso cui è stato lanciato.
    Lo scudo può essere richiamato con la pressione di un tasto, è cumulabile e dura per tutta la durata del livello o fino al game over.
    */

    public class Green_PowerUp : PowerUp
    {
        private SphereCollider _shieldCollider;

        public Green_PowerUp(PlayerModel model) : base(model)
        {

        }

        public override void Start()
        {
            if (!base._isUnlocked || base._isBeingUsed)
                return;

            base._isBeingUsed = true;
            base._playerModel.PowerUp.ShieldTransform.gameObject.SetActive(true);
        }

        public override void Update()
        {
            if (!base._isUnlocked || !base._isBeingUsed)
                return;

            Vector3 shieldPos = base._playerModel.Movement.RigidBody.transform.position;
            shieldPos += base._playerModel.PowerUp.GreenPowerUpOffset;

            float radious = base._playerModel.PowerUp.GreenPowerUpRadious;

            Collider[] colliders = Physics.OverlapSphere(shieldPos, radious, 1 << 8);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].TryGetComponent<MovableObject>(out MovableObject movable))
                {
                    Vector3 opposideDirection = movable.transform.position - shieldPos;
                    movable.Deflect(opposideDirection, base._playerModel.PowerUp.GreenPowerUpStrenght);
                }
            }
        }
    }
}
