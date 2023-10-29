﻿using System.Numerics;
using DG.Tweening;
using QFramework;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace GJFramework
{
    public class MoveUpState<T> : AbstractState<T,PawnController>
    {
        public float endValue;
        protected bool isOver = false;
        private float moveDistance = 1.0f;
        public MoveUpState(FSM<T> fsm, PawnController target) : base(fsm, target)
        {
            
        }
        
        protected override bool OnOverCondition()
        {
            return isOver;
        }
        
        protected override void OnEnter()
        {
            isOver = false;
            moveDistance = mTarget.data.moveDistance;
            mTarget.transform.LookAt(mTarget.transform.position + Vector3.forward * 2.0f);

            Ray ray = new Ray(mTarget.transform.position + Vector3.up * 0.5f, Vector3.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, moveDistance, 1 << LayerMask.NameToLayer("Obstacle"), QueryTriggerInteraction.Collide))
            {
                // Debug.Log("Hit!");
                isOver = true;
            }
            if (!isOver)
            {
                endValue = mTarget.transform.position.z + moveDistance;
                mTarget.transform.DOMoveZ(endValue, 1.0f / Mathf.Max(mTarget.data.speed, 0.1f))
                    .SetEase(mTarget.data.moveCurve)
                    .onComplete = () =>
                    {
                        mTarget.transform.PositionZ(endValue);
                        isOver = true;
                    };
            }
        }
    }
}