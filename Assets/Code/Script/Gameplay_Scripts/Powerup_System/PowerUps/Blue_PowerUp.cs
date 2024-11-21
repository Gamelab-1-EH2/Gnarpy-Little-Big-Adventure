using Player.Model;

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

        public Blue_PowerUp(PlayerModel model) : base(model)
        {

        }

        public override void Start()
        {
            if (!base._isUnlocked || base._isBeingUsed)
                return;

            base._isBeingUsed = true;
        }

        public override void Update()
        {
            if (!base._isUnlocked || !base._isBeingUsed)
                return;
        }
    }
}
