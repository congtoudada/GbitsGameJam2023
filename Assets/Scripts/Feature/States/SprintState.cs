using System;
using DG.Tweening;
using QFramework;
using UnityEngine;

namespace GJFramework
{
    public class SprintState<T> : AbstractState<T, PawnController>
    {
        public Vector3 endValue;
        protected bool isOver = false;
        
        public SprintState(FSM<T> fsm, PawnController target) : base(fsm, target)
        {
            
        }

        protected override bool OnOverCondition()
        {
            return isOver;
        }

        protected override void OnEnter()
        {
            bool isSprint = false;
            isOver = false;

            Ray ray = new Ray(mTarget.transform.position, mTarget.transform.forward);
            RaycastHit hit;
            for (int i = mTarget.data.sprintDistance; i > 0; i--)
            {
                if (!Physics.Raycast(ray, out hit, i, 1 << LayerMask.NameToLayer("Obstacle"), QueryTriggerInteraction.Collide))
                {
                    isSprint = true;
                    endValue = mTarget.transform.position + mTarget.transform.forward * i;
                    mTarget.transform.DOMove(endValue, 1.0f / Mathf.Max(mTarget.data.speed, 0.1f))
                        .SetEase(Ease.OutSine)
                        .onComplete = () =>
                        {
                            mTarget.transform.Position(endValue);
                            isOver = true;
                        };
                    break;
                }
            }
            //冲刺失败
            if (!isSprint)
            {
                isOver = true;
            }
        }
    }
}