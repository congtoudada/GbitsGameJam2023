using QFramework;

namespace GJFramework
{
    public class PlayerMoveLeft : MoveLeftState<PlayerState>
    {
        private new PlayerController mTarget;
        public PlayerMoveLeft(FSM<PlayerState> fsm, PawnController target) : base(fsm, target)
        {
            this.mTarget = target as PlayerController;;
        }
    }
}