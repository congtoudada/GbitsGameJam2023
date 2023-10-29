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
        
        protected override void OnEnter()
        {
            base.OnEnter();
            mTarget.animController.SetBool(PawnController.IS_RUNNING, true);
            ActionKit.Delay(1.0f / mTarget.data.speed * mTarget.playerData.lock_ratio, () =>
            {
                mTarget.CanDoNextState();
            }).Start(mTarget);
        }
        
        protected override void OnExit()
        {
            mTarget.animController.SetBool(PawnController.IS_RUNNING, false);
        }
    }
}