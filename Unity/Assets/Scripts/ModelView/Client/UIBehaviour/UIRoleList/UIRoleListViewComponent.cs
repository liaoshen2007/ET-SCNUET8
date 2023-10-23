using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class UIRoleListViewComponent : Entity, IAwake, IDestroy 
	{
		public RectTransform EGBackGroundRectTransform
     	{
     		get
     		{
     			if (uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}

     			if( m_EGBackGroundRectTransform == null )
     			{
		    		m_EGBackGroundRectTransform = UIFindHelper.FindDeepChild<RectTransform>(uiTransform.gameObject,"EGBackGround");
     			}

     			return m_EGBackGroundRectTransform;
     		}
     	}

		public LoopHorizontalScrollRect E_RolesLoopHorizontalScrollRect
     	{
     		get
     		{
     			if (uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}

     			if( m_E_RolesLoopHorizontalScrollRect == null )
     			{
		    		m_E_RolesLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<LoopHorizontalScrollRect>(uiTransform.gameObject,"E_Roles");
     			}

     			return m_E_RolesLoopHorizontalScrollRect;
     		}
     	}

		public Button E_CreateRoleButton
     	{
     		get
     		{
     			if (uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}

     			if( m_E_CreateRoleButton == null )
     			{
		    		m_E_CreateRoleButton = UIFindHelper.FindDeepChild<Button>(uiTransform.gameObject,"E_CreateRole");
     			}

     			return m_E_CreateRoleButton;
     		}
     	}

		public Image E_CreateRoleImage
     	{
     		get
     		{
     			if (uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}

     			if( m_E_CreateRoleImage == null )
     			{
		    		m_E_CreateRoleImage = UIFindHelper.FindDeepChild<Image>(uiTransform.gameObject,"E_CreateRole");
     			}

     			return m_E_CreateRoleImage;
     		}
     	}

		public Button E_DeleteRoleButton
     	{
     		get
     		{
     			if (uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}

     			if( m_E_DeleteRoleButton == null )
     			{
		    		m_E_DeleteRoleButton = UIFindHelper.FindDeepChild<Button>(uiTransform.gameObject,"E_DeleteRole");
     			}

     			return m_E_DeleteRoleButton;
     		}
     	}

		public Image E_DeleteRoleImage
     	{
     		get
     		{
     			if (uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}

     			if( m_E_DeleteRoleImage == null )
     			{
		    		m_E_DeleteRoleImage = UIFindHelper.FindDeepChild<Image>(uiTransform.gameObject,"E_DeleteRole");
     			}

     			return m_E_DeleteRoleImage;
     		}
     	}

		public InputField E_RoleNameInputField
     	{
     		get
     		{
     			if (uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}

     			if( m_E_RoleNameInputField == null )
     			{
		    		m_E_RoleNameInputField = UIFindHelper.FindDeepChild<InputField>(uiTransform.gameObject,"E_RoleName");
     			}

     			return m_E_RoleNameInputField;
     		}
     	}

		public Image E_RoleNameImage
     	{
     		get
     		{
     			if (uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}

     			if( m_E_RoleNameImage == null )
     			{
		    		m_E_RoleNameImage = UIFindHelper.FindDeepChild<Image>(uiTransform.gameObject,"E_RoleName");
     			}

     			return m_E_RoleNameImage;
     		}
     	}

		public Button E_ConfirmButton
     	{
     		get
     		{
     			if (uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}

     			if( m_E_ConfirmButton == null )
     			{
		    		m_E_ConfirmButton = UIFindHelper.FindDeepChild<Button>(uiTransform.gameObject,"E_Confirm");
     			}

     			return m_E_ConfirmButton;
     		}
     	}

		public Image E_ConfirmImage
     	{
     		get
     		{
     			if (uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}

     			if( m_E_ConfirmImage == null )
     			{
		    		m_E_ConfirmImage = UIFindHelper.FindDeepChild<Image>(uiTransform.gameObject,"E_Confirm");
     			}

     			return m_E_ConfirmImage;
     		}
     	}

		public void DestroyWidget()
		{
			m_EGBackGroundRectTransform = null;
			m_E_RolesLoopHorizontalScrollRect = null;
			m_E_CreateRoleButton = null;
			m_E_CreateRoleImage = null;
			m_E_DeleteRoleButton = null;
			m_E_DeleteRoleImage = null;
			m_E_RoleNameInputField = null;
			m_E_RoleNameImage = null;
			m_E_ConfirmButton = null;
			m_E_ConfirmImage = null;
			uiTransform = null;
		}

		private RectTransform m_EGBackGroundRectTransform = null;
		private LoopHorizontalScrollRect m_E_RolesLoopHorizontalScrollRect = null;
		private Button m_E_CreateRoleButton = null;
		private Image m_E_CreateRoleImage = null;
		private Button m_E_DeleteRoleButton = null;
		private Image m_E_DeleteRoleImage = null;
		private InputField m_E_RoleNameInputField = null;
		private Image m_E_RoleNameImage = null;
		private Button m_E_ConfirmButton = null;
		private Image m_E_ConfirmImage = null;
		public Transform uiTransform = null;
	}
}
