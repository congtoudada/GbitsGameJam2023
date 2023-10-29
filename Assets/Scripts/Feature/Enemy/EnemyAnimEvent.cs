/****************************************************
  文件：EnemyAnimEvent.cs
  作者：聪头
  邮箱: 1322080797@qq.com
  日期：DateTime
  功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJFramework
{
    public class EnemyAnimEvent : MonoBehaviour
    {
        public event Action<string> OnActionOver;
        public SphereCollider collider;
        
        public void ActionOver(string name)
        {
            collider.isTrigger = false;
            OnActionOver?.Invoke(name);
        }

        public void BeginCalTrigger()
        {
            collider.isTrigger = true;
        }
    }
}


