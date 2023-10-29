/****************************************************
  文件：PlayerData.cs
  作者：聪头
  邮箱: 1322080797@qq.com
  日期：DateTime
  功能：
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using CT.Tools;
using DG.Tweening;
using UnityEngine;

namespace GJFramework
{
    [CreateAssetMenu(menuName = "CGJFramework/PawnProfile", fileName = "PawnProfile", order = 31)]
    public class PawnData : ScriptableObject
    {
        public string name;
        public float speed = 2.0f;
        public int sprintDistance = 2;
        public float moveDistance = 1.0f;
        public float scale = 1.0f;
        public bool isDebug = false;
        public Ease moveCurve = Ease.Linear;
    }
}