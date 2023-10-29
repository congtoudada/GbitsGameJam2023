using QFramework;

namespace GJFramework
{
    public class PlayerAbsorbInplace : AbsorbState<PlayerState>
    {
        private new PlayerController mTarget;
        protected bool isOver = false;
        public PlayerAbsorbInplace(FSM<PlayerState> fsm, PawnController target) : base(fsm, target)
        {
            this.mTarget = target as PlayerController;;
        }

        protected override bool OnOverCondition()
        {
            return isOver;
        }

        protected override void OnEnter()
        {
            base.OnEnter();
            isOver = false;
            mTarget.animController.SetBool(PawnController.IS_ABSORB_INPLACE, true);
            mTarget.playerAnimEvent.OnActionOver += AbsorbInpalceOver;
            ActionKit.Delay(0.5f, () =>
            {
                mTarget.CanDoNextState();
            }).Start(mTarget);
        }

        private void AbsorbInpalceOver(string name)
        {
            if (name == "absorb_inplace")
            {
                mTarget.playerAnimEvent.OnActionOver -= AbsorbInpalceOver;
                isOver = true;
            }
        }

        protected override void OnExit()
        {
            mTarget.animController.SetBool(PawnController.IS_ABSORB_INPLACE, false);
        }
    }
}