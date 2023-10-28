using QFramework;

namespace GJFramework
{
    public class PlayerAbsorb : AbsorbState<PlayerState>
    {
        private new PlayerController mTarget;
        public PlayerAbsorb(FSM<PlayerState> fsm, PawnController target) : base(fsm, target)
        {
            this.mTarget = target as PlayerController;;
        }
    }
}