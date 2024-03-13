using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class UIRoleSelectViewComponent : Entity, IAwake, IDestroy 
	{
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
		    		this.m_E_TitleExtendText = UIFindHelper.FindDeepChild<ExtendText>(this.uiTransform.gameObject,"Panel/E_Title");
     			}
     			return this.m_E_TitleExtendText;
     		}
     	}

		public LoopVerticalScrollRect E_RoleListLoopVerticalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_RoleListLoopVerticalScrollRect == null )
     			{
		    		this.m_E_RoleListLoopVerticalScrollRect = UIFindHelper.FindDeepChild<LoopVerticalScrollRect>(this.uiTransform.gameObject,"Panel/E_RoleList");
     			}
     			return this.m_E_RoleListLoopVerticalScrollRect;
     		}
     	}

		public Button E_CloseBtnButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CloseBtnButton == null )
     			{
		    		this.m_E_CloseBtnButton = UIFindHelper.FindDeepChild<Button>(this.uiTransform.gameObject,"Panel/E_CloseBtn");
     			}
     			return this.m_E_CloseBtnButton;
     		}
     	}

		public ExtendImage E_CloseBtnExtendImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CloseBtnExtendImage == null )
     			{
		    		this.m_E_CloseBtnExtendImage = UIFindHelper.FindDeepChild<ExtendImage>(this.uiTransform.gameObject,"Panel/E_CloseBtn");
     			}
     			return this.m_E_CloseBtnExtendImage;
     		}
     	}

		public Button E_DeleteRoleButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_DeleteRoleButton == null )
     			{
		    		this.m_E_DeleteRoleButton = UIFindHelper.FindDeepChild<Button>(this.uiTransform.gameObject,"Panel/E_DeleteRole");
     			}
     			return this.m_E_DeleteRoleButton;
     		}
     	}

		public Button E_EnterGameButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_EnterGameButton == null )
     			{
		    		this.m_E_EnterGameButton = UIFindHelper.FindDeepChild<Button>(this.uiTransform.gameObject,"Panel/E_EnterGame");
     			}
     			return this.m_E_EnterGameButton;
     		}
     	}

		public Button E_CreateRoleButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CreateRoleButton == null )
     			{
		    		this.m_E_CreateRoleButton = UIFindHelper.FindDeepChild<Button>(this.uiTransform.gameObject,"Panel/E_CreateRole");
     			}
     			return this.m_E_CreateRoleButton;
     		}
     	}

		public InputField E_NewRoleNameInputField
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_NewRoleNameInputField == null )
     			{
		    		this.m_E_NewRoleNameInputField = UIFindHelper.FindDeepChild<InputField>(this.uiTransform.gameObject,"Panel/E_NewRoleName");
     			}
     			return this.m_E_NewRoleNameInputField;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_TitleExtendText = null;
			this.m_E_RoleListLoopVerticalScrollRect = null;
			this.m_E_CloseBtnButton = null;
			this.m_E_CloseBtnExtendImage = null;
			this.m_E_DeleteRoleButton = null;
			this.m_E_EnterGameButton = null;
			this.m_E_CreateRoleButton = null;
			this.m_E_NewRoleNameInputField = null;
			uiTransform = null;
		}

		private ExtendText m_E_TitleExtendText = null;
		private LoopVerticalScrollRect m_E_RoleListLoopVerticalScrollRect = null;
		private Button m_E_CloseBtnButton = null;
		private ExtendImage m_E_CloseBtnExtendImage = null;
		private Button m_E_DeleteRoleButton = null;
		private Button m_E_EnterGameButton = null;
		private Button m_E_CreateRoleButton = null;
		private InputField m_E_NewRoleNameInputField = null;
		public Transform uiTransform = null;
	}
}
