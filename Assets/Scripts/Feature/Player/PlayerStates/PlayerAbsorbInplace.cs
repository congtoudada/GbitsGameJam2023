using QFramework;
using Unity.VisualScripting;
using UnityEngine;

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
                Ray ray = new Ray(mTarget.transform.position + Vector3.up * 0.5f, Vector3.down);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 3.0f, 1 << LayerMask.NameToLayer("Floor"), QueryTriggerInteraction.Collide))
                {
                    var item = hit.transform.GetComponent<LevelItem>();
                    if (item != null)
                    {
                        mTarget.ProcessNumberOrOp(hit.transform.GetComponent<LevelItem>().BeAbsorbed()); 
                    }
                }
                mTarget.playerAnimEvent.OnActionOver -= AbsorbInpalceOver;
                isOver = true;
                mTarget.animController.SetBool(PawnController.IS_ABSORB_INPLACE, false);
            }
        }
    }
}