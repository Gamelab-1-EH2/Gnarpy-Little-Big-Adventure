namespace Player.Model
{
    public enum PlayerState
    {
        Idle = 0,
        Move = 1,
        Jump = 2,
        Climb = 4,
        Shoot = 8,
        Fall = 16,
        Dead = 32,
        Landed = 64,
        None = 256,
    }
}
