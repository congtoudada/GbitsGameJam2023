/****************************************************
  文件：UIAnimation.cs
  作者：聪头
  邮箱: 1322080797@qq.com
  日期：DateTime
  功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace GJFramework
{
    public class UIAnimation : MonoBehaviour
    {
        private TweenerCore<Vector2, Vector2, VectorOptions> tween;
        // Start is called before the first frame update
        private float endValue = -5;
        void Start()
        {
            var rectTransform = GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                DOTween.Sequence()
                    .Append(rectTransform.DOAnchorPosY(endValue - 15, 0.6f))
                    .Append(rectTransform.DOAnchorPosY(endValue, 0.6f)).SetLoops(-1);
            }
        }

    }
}


