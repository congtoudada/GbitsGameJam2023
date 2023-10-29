using System;
using DG.Tweening;
using QFramework;
using UnityEngine;

namespace GJFramework
{
    public class EnemyChase : AbstractState<EnemyState,EnemyController>
    {
        private Transform player;
        private Vector3 diff;
        private Vector3 endValue;
        public EnemyChase(FSM<EnemyState> fsm, EnemyController target) : base(fsm, target)
        {
            player = GameObject.FindWithTag("Player").transform;
        }

        protected override void OnEnter()
        {
            mTarget.animator.SetBool(PawnController.IS_RUNNING, true);
            
            diff = mTarget.transform.position - player.position;
            if (diff.sqrMagnitude <= mTarget.data.moveDistance * mTarget.data.moveDistance * mTarget.data.scale + 1)
            {
                mFSM.ChangeState(EnemyState.MoveTo);
                return;
            }

            endValue = mTarget.transform.forward;
            if (Math.Abs(diff.x) > Math.Abs(diff.z))
            {
                if (diff.x > 0)
                {
                    endValue = Vector3.left;
                }
                else
                {
                    endValue = Vector3.right;
                }
            }
            else
            {
                if (diff.z > 0)
                {
                    endValue = Vector3.back;
                }
                else
                {
                    endValue = Vector3.forward;
                }
            }
            
            Ray ray = new Ray(mTarget.transform.position, endValue);
            float moveDistance = mTarget.data.moveDistance;
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, moveDistance, 1 << LayerMask.NameToLayer("Obstacle"), QueryTriggerInteraction.Collide))
            {
                if (Vector3.Magnitude(hit.transform.position - mTarget.transform.position) <= 1)
                {
                    endValue = mTarget.transform.position;
                }
                else
                {
                    endValue = mTarget.transform.position + endValue;
                }
            }
            else
            {
                endValue = mTarget.transform.position + endValue * moveDistance;
            }
            
            // mTarget.transform.DOLookAt(endValue, 0.25f);
            mTarget.transform.DOLookAt(player.transform.position, 0.2f);
            mTarget.transform.DOMove(endValue, 1.0f / Mathf.Max(mTarget.data.speed, 0.1f))
                .SetEase(mTarget.data.moveCurve)
                .onComplete = () =>
                {
                    mTarget.transform.position = endValue;
                    mFSM.ChangeState(EnemyState.None);
                };
        }
    }
}