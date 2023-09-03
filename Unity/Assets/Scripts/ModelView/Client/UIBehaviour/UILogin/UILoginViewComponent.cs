using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class UILoginViewComponent : Entity, IAwake, IDestroy 
	{
		public InputField E_AccountInputInputField
     	{
     		get
     		{
     			if (uiTransform == null)
     			{
     				this.Fiber().Error("uiTransform is null.");
     				return null;
     			}

     			if( m_E_AccountInputInputField == null )
     			{
		    		m_E_AccountInputInputField = UIFindHelper.FindDeepChild<InputField>(uiTransform.gameObject,"Panel/E_AccountInput");
     			}

     			return m_E_AccountInputInputField;
     		}
     	}

		public Image E_AccountInputImage
     	{
     		get
     		{
     			if (uiTransform == null)
     			{
     				this.Fiber().Error("uiTransform is null.");
     				return null;
     			}

     			if( m_E_AccountInputImage == null )
     			{
		    		m_E_AccountInputImage = UIFindHelper.FindDeepChild<Image>(uiTransform.gameObject,"Panel/E_AccountInput");
     			}

     			return m_E_AccountInputImage;
     		}
     	}

		public InputField E_PasswordInputInputField
     	{
     		get
     		{
     			if (uiTransform == null)
     			{
     				this.Fiber().Error("uiTransform is null.");
     				return null;
     			}

     			if( m_E_PasswordInputInputField == null )
     			{
		    		m_E_PasswordInputInputField = UIFindHelper.FindDeepChild<InputField>(uiTransform.gameObject,"Panel/E_PasswordInput");
     			}

     			return m_E_PasswordInputInputField;
     		}
     	}

		public Image E_PasswordInputImage
     	{
     		get
     		{
     			if (uiTransform == null)
     			{
     				this.Fiber().Error("uiTransform is null.");
     				return null;
     			}

     			if( m_E_PasswordInputImage == null )
     			{
		    		m_E_PasswordInputImage = UIFindHelper.FindDeepChild<Image>(uiTransform.gameObject,"Panel/E_PasswordInput");
     			}

     			return m_E_PasswordInputImage;
     		}
     	}

		public Button E_LoginButtonButton
     	{
     		get
     		{
     			if (uiTransform == null)
     			{
     				this.Fiber().Error("uiTransform is null.");
     				return null;
     			}

     			if( m_E_LoginButtonButton == null )
     			{
		    		m_E_LoginButtonButton = UIFindHelper.FindDeepChild<Button>(uiTransform.gameObject,"Panel/E_LoginButton");
     			}

     			return m_E_LoginButtonButton;
     		}
     	}

		public Image E_LoginButtonImage
     	{
     		get
     		{
     			if (uiTransform == null)
     			{
     				this.Fiber().Error("uiTransform is null.");
     				return null;
     			}

     			if( m_E_LoginButtonImage == null )
     			{
		    		m_E_LoginButtonImage = UIFindHelper.FindDeepChild<Image>(uiTransform.gameObject,"Panel/E_LoginButton");
     			}

     			return m_E_LoginButtonImage;
     		}
     	}

		public void DestroyWidget()
		{
			m_E_AccountInputInputField = null;
			m_E_AccountInputImage = null;
			m_E_PasswordInputInputField = null;
			m_E_PasswordInputImage = null;
			m_E_LoginButtonButton = null;
			m_E_LoginButtonImage = null;
			uiTransform = null;
		}

		private InputField m_E_AccountInputInputField = null;
		private Image m_E_AccountInputImage = null;
		private InputField m_E_PasswordInputInputField = null;
		private Image m_E_PasswordInputImage = null;
		private Button m_E_LoginButtonButton = null;
		private Image m_E_LoginButtonImage = null;
		public Transform uiTransform = null;
	}
}
