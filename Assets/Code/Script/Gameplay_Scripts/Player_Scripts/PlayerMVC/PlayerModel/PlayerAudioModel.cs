using Audio_System.SFX;

namespace Player.Model
{
    public class PlayerAudioModel
    {
        private SFX_SO _damagedSFX;
        private SFX_SO _deathSFX;
        private SFX_SO _jumpSFX;
        private SFX_SO _shootSFX;

        private SFX_SO _shieldOnSFX;
        private SFX_SO _shieldOffSFX;
        private SFX_SO _gravityOnSFX;
        private SFX_SO _gravityOffSFX;

        public PlayerAudioModel(Player_SO playerSO)
        {
            _damagedSFX = playerSO.DamagedSFX;
            _deathSFX = playerSO.DeathSFX;
            _jumpSFX = playerSO.JumpSFX;
            _shootSFX = playerSO.ShootSFX;

            _shieldOnSFX = playerSO.ShieldOnSFX;
            _shieldOffSFX = playerSO.ShieldOffSFX;
            _gravityOnSFX = playerSO.GravityOnSFX;
            _gravityOffSFX = playerSO.GravityOffSFX;
        }

        public SFX DamagedSFX => _damagedSFX.GetSFX();
        public SFX DeathSFX => _deathSFX.GetSFX();
        public SFX JumpSFX => _jumpSFX.GetSFX();
        public SFX ShootSFX => _shootSFX.GetSFX();

        public SFX ShieldOnSFX => _shieldOnSFX.GetSFX();
        public SFX ShieldOffSFX => _shieldOffSFX.GetSFX();
        public SFX GravityOnSFX => _gravityOnSFX.GetSFX();
        public SFX GravityOffSFX => _gravityOffSFX.GetSFX();
    }
}
