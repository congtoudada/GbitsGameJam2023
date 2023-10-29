using QFramework;
using UnityEngine;

namespace GJFramework
{
    public class PlayerAbsorb : AbsorbState<PlayerState>
    {
        private new PlayerController mTarget;
        protected bool isOver = false;
        public PlayerAbsorb(FSM<PlayerState> fsm, PawnController target) : base(fsm, target)
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
            mTarget.animController.SetBool(PawnController.IS_ABSORB, true);
            mTarget.playerAnimEvent.OnActionOver += AbsorbOver;
            ActionKit.Delay(0.3f, () =>
            {
                mTarget.CanDoNextState();
            }).Start(mTarget);
        }

        private void AbsorbOver(string name)
        {
            if (name == "absorb")
            {
                for (int i = 0; i < mTarget.raycastGroup.Length; i++)
                {
                    Ray ray = new Ray(mTarget.raycastGroup[i].position, mTarget.raycastGroup[i].forward);
                    RaycastHit hit;
                    // Debug.DrawLine(mTarget.raycastGroup[i].position,
                    //     mTarget.raycastGroup[i].position + mTarget.raycastGroup[i].forward * 7.0f, Color.red, 3.0f);
                    if (Physics.Raycast(ray, out hit, 7.0f, 1 << LayerMask.NameToLayer("Enemy"), QueryTriggerInteraction.Collide))
                    {
                        var item = hit.transform.GetComponent<ICanAbsorbed>();
                        if (item != null)
                        {
                            mTarget.ProcessNumberOrOp(hit.transform.GetComponent<ICanAbsorbed>().BeAbsorbed()); 
                        }
                        break;
                    }
                }
                mTarget.playerAnimEvent.OnActionOver -= AbsorbOver;
                isOver = true;
                mTarget.animController.SetBool(PawnController.IS_ABSORB, false);
            }
        }

    }
}