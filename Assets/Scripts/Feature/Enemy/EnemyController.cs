/****************************************************
  文件：Enemy.cs
  作者：聪头
  邮箱: 1322080797@qq.com
  日期：DateTime
  功能：
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace GJFramework
{
    public enum EnemyState
    {
        None,
        Idle,
        Patrol,
        Chase,
        MoveTo,
        Attack,
        Die
    }
    
    public class EnemyController : PawnController
    {
        public FSM<EnemyState> FSM = new FSM<EnemyState>();
        public Animator animator;
        public EnemyData enemyData;

        protected void Start()
        {
            FSM.OnStateChanged((pre_action, now_action) =>
            {
                if (data.isDebug)
                    Debug.Log($"[ {data.name} ] {pre_action} -> {now_action}");
            });

            transform.localScale = Vector3.one * data.scale;
            animator.speed *= 2.0f / data.scale;

        }

        // Update is called once per frame
        protected void Update()
        {
            FSM.Update();
        }
    }
}


