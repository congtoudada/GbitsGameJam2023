using DG.Tweening;
using QFramework;
using UnityEngine;

namespace GJFramework
{
    public class MoveLeftState<T> : AbstractState<T,PawnController>
    {
        public float endValue;
        protected bool isOver = false;
        public MoveLeftState(FSM<T> fsm, PawnController target) : base(fsm, target)
        {
            
        }
        protected override bool OnOverCondition()
        {
            return isOver;
        }
        
        protected override void OnEnter()
        {
            isOver = false;
            mTarget.transform.LookAt(mTarget.transform.position - Vector3.right * 2.0f);

            Ray ray = new Ray(mTarget.transform.position, Vector3.left);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1.0f, 1 << LayerMask.NameToLayer("Obstacle"), QueryTriggerInteraction.Collide))
            {
                isOver = true;
            }
            else
            {
                endValue = mTarget.transform.position.x - 1;
                mTarget.transform.DOMoveX(endValue, 1.0f / Mathf.Max(mTarget.data.speed, 0.1f))
                    .SetEase(mTarget.data.moveCurve)
                    .onComplete = () =>
                    {
                        mTarget.transform.PositionX(endValue);
                        isOver = true;
                    };
            }
        }
    }
}