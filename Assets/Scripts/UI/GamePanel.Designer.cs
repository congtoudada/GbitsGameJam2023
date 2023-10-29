using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace GJFramework
{
	// Generate Id:c361096a-77d0-454f-a808-ed939a9f13c1
	public partial class GamePanel
	{
		public const string Name = "GamePanel";
		
		[SerializeField]
		public RectTransform life1;
		[SerializeField]
		public RectTransform life2;
		[SerializeField]
		public RectTransform life3;
		[SerializeField]
		public RectTransform Horizontal;
		[SerializeField]
		public GJFramework.NumberOpItem Number1;
		[SerializeField]
		public GJFramework.NumberOpItem Operation1;
		[SerializeField]
		public GJFramework.NumberOpItem Number2;
		[SerializeField]
		public GJFramework.NumberOpItem Operation2;
		[SerializeField]
		public GJFramework.NumberOpItem Number3;
		[SerializeField]
		public GJFramework.NumberOpItem Operation3;
		[SerializeField]
		public GJFramework.NumberOpItem Number4;
		[SerializeField]
		public GJFramework.NumberOpItem Operation4;
		[SerializeField]
		public GJFramework.NumberOpItem Number5;
		
		private GamePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			life1 = null;
			life2 = null;
			life3 = null;
			Horizontal = null;
			Number1 = null;
			Operation1 = null;
			Number2 = null;
			Operation2 = null;
			Number3 = null;
			Operation3 = null;
			Number4 = null;
			Operation4 = null;
			Number5 = null;
			
			mData = null;
		}
		
		public GamePanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		GamePanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new GamePanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
