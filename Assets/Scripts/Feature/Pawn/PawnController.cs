/****************************************************
  文件：PawnController.cs
  作者：聪头
  邮箱: 1322080797@qq.com
  日期：DateTime
  功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace GJFramework
{
    public class PawnController : MonoBehaviour
    {
        public PawnData data;

        public const string IS_RUNNING = "isRunning";
        public const string IS_ATTACK = "isAttack";
        public const string IS_ABSORB = "isAbsorb";
        public const string IS_ABSORB_INPLACE = "isAbsorbInplace";


        protected void Start()
        {
        }
    }
}


