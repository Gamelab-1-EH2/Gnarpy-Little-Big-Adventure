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

        public SFX DamagedSFX => GetSFX(_damagedSFX);
        public SFX DeathSFX => GetSFX(_deathSFX);
        public SFX JumpSFX => GetSFX(_jumpSFX);
        public SFX ShootSFX => GetSFX(_shootSFX);

        public SFX ShieldOnSFX => GetSFX(_shieldOnSFX);
        public SFX ShieldOffSFX => GetSFX(_shieldOffSFX);
        public SFX GravityOnSFX => GetSFX(_gravityOnSFX);
        public SFX GravityOffSFX => GetSFX(_gravityOffSFX);

        private SFX GetSFX(SFX_SO sfxSO)
        {
            if(sfxSO !=  null)
                return sfxSO.GetSFX();
            return null;
        }
    }
}
