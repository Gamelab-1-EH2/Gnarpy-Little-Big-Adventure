using Player.Model;

namespace Collectible_System.PowerUp
{
    public abstract class PowerUp
    {
        protected bool _isUnlocked = false;
        protected bool _isBeingUsed = false;
        protected PlayerModel _playerModel;
        
        public PowerUp(PlayerModel model)
        {
            _playerModel = model;
        }

        public abstract void Process();
        public abstract void Start();

        public bool Unlocked
        {
            get => _isUnlocked;
            set => _isUnlocked = value;
        }
    }
}
