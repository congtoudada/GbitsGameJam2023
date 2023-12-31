/****************************************************
  文件：SPController.cs
  作者：聪头
  邮箱: 1322080797@qq.com
  日期：DateTime
  功能：
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using QFramework;
using UnityEngine;
using UnityEngine.UI;

namespace GJFramework
{
    public class SPController : MonoBehaviour
    {
        public BindableProperty<float> sp = new BindableProperty<float>();

        public Image sp_img_bg;
        public Image sp_img;

        public RawImage showImage;
        // Start is called before the first frame update
        void Start()
        {
            showImage.gameObject.SetActive(false);
            sp.SetValueWithoutEvent(1.0f);
            sp.Register(x =>
            {
                sp_img.DOFillAmount(x, 0.3f);
                sp_img_bg.DOFillAmount(x, 0.6f);
            });
        }

        public void PlayShowAnim(int id)
        {
            if (id < 1 || id > 13) return;
            showImage.gameObject.SetActive(true);
            showImage.texture = Resources.Load<Texture>($"{id}");
            showImage.transform.DOLocalMove(-transform.up * 3.0f, 2.5f).From();
        }
    }
}


