using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace GJFramework
{
	// Generate Id:d6c982b0-ba03-4389-ab45-dd7a58b41380
	public partial class GamePanel
	{
		public const string Name = "GamePanel";
		
		[SerializeField]
		public GJFramework.NumberOpItem Number1;
		[SerializeField]
		public GJFramework.NumberOpItem Operation1;
		[SerializeField]
		public GJFramework.NumberOpItem Number2;
		[SerializeField]
		public GJFramework.NumberOpItem Operation;
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
			Number1 = null;
			Operation1 = null;
			Number2 = null;
			Operation = null;
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
