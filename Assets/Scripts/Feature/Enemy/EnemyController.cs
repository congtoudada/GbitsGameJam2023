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
        Idle,
        Patrol,
        Chase,
        Attack,
        Die
    }
    
    public class EnemyController : PawnController
    {
        public FSM<EnemyController> FSM = new FSM<EnemyController>();
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}


