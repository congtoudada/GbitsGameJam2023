using DG.Tweening;
using QFramework;
using UnityEngine;

namespace GJFramework
{
    public class EnemyMoveTo : AbstractState<EnemyState,PawnController>
    {
        private GameObject player;
        public Vector3 endValue;
        
        public EnemyMoveTo(FSM<EnemyState> fsm, PawnController target) : base(fsm, target)
        {
            player = GameObject.FindWithTag("Player");
        }

        protected override void OnEnter()
        {
            endValue = player.transform.position;
            mTarget.transform.LookAt(endValue);
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