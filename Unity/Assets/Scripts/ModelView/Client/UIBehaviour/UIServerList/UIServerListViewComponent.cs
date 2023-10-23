using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class UIServerListViewComponent : Entity, IAwake, IDestroy 
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

		public LoopVerticalScrollRect E_ServerListLoopVerticalScrollRect
     	{
     		get
     		{
     			if (uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}

     			if( m_E_ServerListLoopVerticalScrollRect == null )
     			{
		    		m_E_ServerListLoopVerticalScrollRect = UIFindHelper.FindDeepChild<LoopVerticalScrollRect>(uiTransform.gameObject,"E_ServerList");
     			}

     			return m_E_ServerListLoopVerticalScrollRect;
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
			m_E_ServerListLoopVerticalScrollRect = null;
			m_E_ConfirmButton = null;
			m_E_ConfirmImage = null;
			uiTransform = null;
		}

		private RectTransform m_EGBackGroundRectTransform = null;
		private LoopVerticalScrollRect m_E_ServerListLoopVerticalScrollRect = null;
		private Button m_E_ConfirmButton = null;
		private Image m_E_ConfirmImage = null;
		public Transform uiTransform = null;
	}
}
