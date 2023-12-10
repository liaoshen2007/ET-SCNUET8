using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class UIChatViewComponent : Entity, IAwake, IDestroy 
	{
		public RectTransform EG_ChatRectTransform
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EG_ChatRectTransform == null )
     			{
		    		this.m_EG_ChatRectTransform = UIFindHelper.FindDeepChild<RectTransform>(this.uiTransform.gameObject,"EG_Chat");
     			}
     			return this.m_EG_ChatRectTransform;
     		}
     	}

		public ExtendText E_TitleExtendText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_TitleExtendText == null )
     			{
		    		this.m_E_TitleExtendText = UIFindHelper.FindDeepChild<ExtendText>(this.uiTransform.gameObject,"EG_Chat/Banner/E_Title");
     			}
     			return this.m_E_TitleExtendText;
     		}
     	}

		public Button E_BackBtnButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BackBtnButton == null )
     			{
		    		this.m_E_BackBtnButton = UIFindHelper.FindDeepChild<Button>(this.uiTransform.gameObject,"EG_Chat/Banner/E_BackBtn");
     			}
     			return this.m_E_BackBtnButton;
     		}
     	}

		public ExtendImage E_BackBtnExtendImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BackBtnExtendImage == null )
     			{
		    		this.m_E_BackBtnExtendImage = UIFindHelper.FindDeepChild<ExtendImage>(this.uiTransform.gameObject,"EG_Chat/Banner/E_BackBtn");
     			}
     			return this.m_E_BackBtnExtendImage;
     		}
     	}

		public Button E_SettingBtnButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_SettingBtnButton == null )
     			{
		    		this.m_E_SettingBtnButton = UIFindHelper.FindDeepChild<Button>(this.uiTransform.gameObject,"EG_Chat/Banner/E_SettingBtn");
     			}
     			return this.m_E_SettingBtnButton;
     		}
     	}

		public ExtendImage E_SettingBtnExtendImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_SettingBtnExtendImage == null )
     			{
		    		this.m_E_SettingBtnExtendImage = UIFindHelper.FindDeepChild<ExtendImage>(this.uiTransform.gameObject,"EG_Chat/Banner/E_SettingBtn");
     			}
     			return this.m_E_SettingBtnExtendImage;
     		}
     	}

		public LoopHorizontalScrollRect E_MenuListLoopHorizontalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_MenuListLoopHorizontalScrollRect == null )
     			{
		    		this.m_E_MenuListLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<LoopHorizontalScrollRect>(this.uiTransform.gameObject,"EG_Chat/E_MenuList");
     			}
     			return this.m_E_MenuListLoopHorizontalScrollRect;
     		}
     	}

		public LoopVerticalScrollRect E_MsgListLoopVerticalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_MsgListLoopVerticalScrollRect == null )
     			{
		    		this.m_E_MsgListLoopVerticalScrollRect = UIFindHelper.FindDeepChild<LoopVerticalScrollRect>(this.uiTransform.gameObject,"EG_Chat/E_MsgList");
     			}
     			return this.m_E_MsgListLoopVerticalScrollRect;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EG_ChatRectTransform = null;
			this.m_E_TitleExtendText = null;
			this.m_E_BackBtnButton = null;
			this.m_E_BackBtnExtendImage = null;
			this.m_E_SettingBtnButton = null;
			this.m_E_SettingBtnExtendImage = null;
			this.m_E_MenuListLoopHorizontalScrollRect = null;
			this.m_E_MsgListLoopVerticalScrollRect = null;
			uiTransform = null;
		}

		private RectTransform m_EG_ChatRectTransform = null;
		private ExtendText m_E_TitleExtendText = null;
		private Button m_E_BackBtnButton = null;
		private ExtendImage m_E_BackBtnExtendImage = null;
		private Button m_E_SettingBtnButton = null;
		private ExtendImage m_E_SettingBtnExtendImage = null;
		private LoopHorizontalScrollRect m_E_MenuListLoopHorizontalScrollRect = null;
		private LoopVerticalScrollRect m_E_MsgListLoopVerticalScrollRect = null;
		public Transform uiTransform = null;
	}
}
