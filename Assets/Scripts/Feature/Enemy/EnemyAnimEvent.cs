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
        private SphereCollider collider;

        private void Start()
        {
            collider = GetComponentInChildren<SphereCollider>();
        }

        public void ActionOver(string name)
        {
            collider.isTrigger = false;
            OnActionOver?.Invoke(name);
        }

        private void OnTriggerEnter(Collider other)
        {
            // Debug.Log("Trigger: " + other.name);
            if (other.CompareTag("PlayerModel"))
            {
                collider.isTrigger = false;
                other.transform.parent.GetComponent<PlayerController>().Hurt();
            }
        }

        public void BeginCalTrigger()
        {
            collider.isTrigger = true;
        }
    }
}


