using Player.Model;
using UnityEngine;

namespace Collectible_System.PowerUp
{
    public class Blue_PowerUp : PowerUp
    {

        /*
        La blue strawberry consente di lanciare delle palle di pelo che fungeranno da proiettili.
        Le palle di pelo verranno generate dal personaggio e verranno lanciate seguendo una traiettoria dritta lungo l’asse X verso il quale è girato il character. 
        I bullet generati potranno essere distrutti solo quando collidono.
        I bullet si rompono con la collisione.
        Eventuali oggetti contenuti all’interno degli oggetti distruttibili non saranno distrutti e saranno resi visibili al giocatore.
        Il giocatore potrà sparare il bullet durante tutti gli stati del personaggio, quindi sia in camminata, sia in idle, sia in salto, sia in arrampicata.
        Ha un leggero lancio iniziale a parabola e ha una bounciness che gli permette di rimbalzare sulla piattaforma su cui atterra fino a quando non esce dalla visuale della telecamera o collide con qualsiasi ostacolo.
        La velocità dei bullet deve essere più veloce rispetto al movimento del player in modo tale da non raggiungerlo mai e non collidere contro di esso in fase di camminata.
        Il power up dura per tutta la durata del livello, fino a quando il personaggio non subisce danno o fino ad un game over.
        Il power up è cumulabile.
        */

        private float _lastTimeShoot;
        private PowerUpModel _powerUpModel;
        private ObjectPooler _pooler;

        public Blue_PowerUp(PlayerModel model) : base(model)
        {
            _lastTimeShoot = 0f;
            _powerUpModel = model.PowerUp;
            _pooler = new ObjectPooler(_powerUpModel.BlueProjectile, 15);
        }

        public override void Start()
        {
            if (!base._isUnlocked)
                return;

            float enlapsed = Time.time - _lastTimeShoot;

            if (enlapsed >= _powerUpModel.ShootDelay)
            {
                _lastTimeShoot = Time.time;

                Projectile projectile = _pooler.PoolObject().GetComponent<Projectile>();
                
                Vector3 shootDir = _powerUpModel.ProjectileDirectionOffset;
                shootDir.x *= _playerModel.Movement.LookingRight ? 1f : -1f;

                Vector3 offset = (Vector3)_powerUpModel.ProjectileOffset;
                offset.x *= _playerModel.Movement.LookingRight ? 1f : -1f;

                Vector3 projectilePos = base._playerModel.Movement.RigidBody.transform.position + offset;
                
                projectile.transform.position = projectilePos;
                projectile.Shoot(shootDir, _powerUpModel.ProjectileSpeed * (_playerModel.Movement.LookingRight ? 1f : -1f));
                projectile.gameObject.SetActive(true);
            }
        }

        public override void Update()
        {
            return;
        }
    }
}
