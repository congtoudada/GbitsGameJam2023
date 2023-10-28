using QFramework;

namespace GJFramework
{
    public class PlayerMoveRight : MoveRightState<PlayerState>
    {
        private new PlayerController mTarget;
        public PlayerMoveRight(FSM<PlayerState> fsm, PawnController target) : base(fsm, target)
        {
            this.mTarget = target as PlayerController;;
        }
    }
}