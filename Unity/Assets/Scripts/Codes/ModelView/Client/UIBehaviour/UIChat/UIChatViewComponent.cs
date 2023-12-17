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

		public RectTransform EG_AnimRectTransform
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EG_AnimRectTransform == null )
     			{
		    		this.m_EG_AnimRectTransform = UIFindHelper.FindDeepChild<RectTransform>(this.uiTransform.gameObject,"EG_Chat/EG_Anim");
     			}
     			return this.m_EG_AnimRectTransform;
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
		    		this.m_E_MsgListLoopVerticalScrollRect = UIFindHelper.FindDeepChild<LoopVerticalScrollRect>(this.uiTransform.gameObject,"EG_Chat/EG_Anim/E_MsgList");
     			}
     			return this.m_E_MsgListLoopVerticalScrollRect;
     		}
     	}

		public Button E_CloseEmojButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CloseEmojButton == null )
     			{
		    		this.m_E_CloseEmojButton = UIFindHelper.FindDeepChild<Button>(this.uiTransform.gameObject,"EG_Chat/EG_Anim/E_MsgList/E_CloseEmoj");
     			}
     			return this.m_E_CloseEmojButton;
     		}
     	}

		public InputField E_InputInputField
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_InputInputField == null )
     			{
		    		this.m_E_InputInputField = UIFindHelper.FindDeepChild<InputField>(this.uiTransform.gameObject,"EG_Chat/EG_Anim/Bottom/E_Input");
     			}
     			return this.m_E_InputInputField;
     		}
     	}

		public Button E_EmjoButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_EmjoButton == null )
     			{
		    		this.m_E_EmjoButton = UIFindHelper.FindDeepChild<Button>(this.uiTransform.gameObject,"EG_Chat/EG_Anim/Bottom/E_Emjo");
     			}
     			return this.m_E_EmjoButton;
     		}
     	}

		public Button E_SendButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_SendButton == null )
     			{
		    		this.m_E_SendButton = UIFindHelper.FindDeepChild<Button>(this.uiTransform.gameObject,"EG_Chat/EG_Anim/Bottom/E_Send");
     			}
     			return this.m_E_SendButton;
     		}
     	}

		public ExtendImage EmotionExtendImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EmotionExtendImage == null )
     			{
		    		this.m_EmotionExtendImage = UIFindHelper.FindDeepChild<ExtendImage>(this.uiTransform.gameObject,"EG_Chat/EG_Anim/Emotion");
     			}
     			return this.m_EmotionExtendImage;
     		}
     	}

		public LoopHorizontalScrollRect E_EmotionMeuListLoopHorizontalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_EmotionMeuListLoopHorizontalScrollRect == null )
     			{
		    		this.m_E_EmotionMeuListLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<LoopHorizontalScrollRect>(this.uiTransform.gameObject,"EG_Chat/EG_Anim/Emotion/E_EmotionMeuList");
     			}
     			return this.m_E_EmotionMeuListLoopHorizontalScrollRect;
     		}
     	}

		public LoopHorizontalScrollRect E_HistoryListLoopHorizontalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_HistoryListLoopHorizontalScrollRect == null )
     			{
		    		this.m_E_HistoryListLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<LoopHorizontalScrollRect>(this.uiTransform.gameObject,"EG_Chat/EG_Anim/Emotion/List/View/Content/E_HistoryList");
     			}
     			return this.m_E_HistoryListLoopHorizontalScrollRect;
     		}
     	}

		public ExtendImage E_IconExtendImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_IconExtendImage == null )
     			{
		    		this.m_E_IconExtendImage = UIFindHelper.FindDeepChild<ExtendImage>(this.uiTransform.gameObject,"EG_Chat/EG_Anim/Emotion/List/View/Content/E_HistoryList/Viewport/Content/Item_Emoj/E_Icon");
     			}
     			return this.m_E_IconExtendImage;
     		}
     	}

		public Button E_BtnButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BtnButton == null )
     			{
		    		this.m_E_BtnButton = UIFindHelper.FindDeepChild<Button>(this.uiTransform.gameObject,"EG_Chat/EG_Anim/Emotion/List/View/Content/E_HistoryList/Viewport/Content/Item_Emoj/E_Btn");
     			}
     			return this.m_E_BtnButton;
     		}
     	}

		public LoopHorizontalScrollRect E_EmojListLoopHorizontalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_EmojListLoopHorizontalScrollRect == null )
     			{
		    		this.m_E_EmojListLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<LoopHorizontalScrollRect>(this.uiTransform.gameObject,"EG_Chat/EG_Anim/Emotion/List/View/Content/E_EmojList");
     			}
     			return this.m_E_EmojListLoopHorizontalScrollRect;
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
			this.m_EG_AnimRectTransform = null;
			this.m_E_MsgListLoopVerticalScrollRect = null;
			this.m_E_CloseEmojButton = null;
			this.m_E_InputInputField = null;
			this.m_E_EmjoButton = null;
			this.m_E_SendButton = null;
			this.m_EmotionExtendImage = null;
			this.m_E_EmotionMeuListLoopHorizontalScrollRect = null;
			this.m_E_HistoryListLoopHorizontalScrollRect = null;
			this.m_E_IconExtendImage = null;
			this.m_E_BtnButton = null;
			this.m_E_EmojListLoopHorizontalScrollRect = null;
			uiTransform = null;
		}

		private RectTransform m_EG_ChatRectTransform = null;
		private ExtendText m_E_TitleExtendText = null;
		private Button m_E_BackBtnButton = null;
		private ExtendImage m_E_BackBtnExtendImage = null;
		private Button m_E_SettingBtnButton = null;
		private ExtendImage m_E_SettingBtnExtendImage = null;
		private LoopHorizontalScrollRect m_E_MenuListLoopHorizontalScrollRect = null;
		private RectTransform m_EG_AnimRectTransform = null;
		private LoopVerticalScrollRect m_E_MsgListLoopVerticalScrollRect = null;
		private Button m_E_CloseEmojButton = null;
		private InputField m_E_InputInputField = null;
		private Button m_E_EmjoButton = null;
		private Button m_E_SendButton = null;
		private ExtendImage m_EmotionExtendImage = null;
		private LoopHorizontalScrollRect m_E_EmotionMeuListLoopHorizontalScrollRect = null;
		private LoopHorizontalScrollRect m_E_HistoryListLoopHorizontalScrollRect = null;
		private ExtendImage m_E_IconExtendImage = null;
		private Button m_E_BtnButton = null;
		private LoopHorizontalScrollRect m_E_EmojListLoopHorizontalScrollRect = null;
		public Transform uiTransform = null;
	}
}
