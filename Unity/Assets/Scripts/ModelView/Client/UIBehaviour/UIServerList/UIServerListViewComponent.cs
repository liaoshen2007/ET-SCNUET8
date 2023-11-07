using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class UIServerListViewComponent : Entity, IAwake, IDestroy 
	{
		public ExtendText E_TitleExtendText
     	{
     		get
     		{
     			if (uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}

     			if( m_E_TitleExtendText == null )
     			{
		    		m_E_TitleExtendText = UIFindHelper.FindDeepChild<ExtendText>(uiTransform.gameObject,"Panel/E_Title");
     			}

     			return m_E_TitleExtendText;
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
		    		m_E_ServerListLoopVerticalScrollRect = UIFindHelper.FindDeepChild<LoopVerticalScrollRect>(uiTransform.gameObject,"Panel/E_ServerList");
     			}

     			return m_E_ServerListLoopVerticalScrollRect;
     		}
     	}

		public Button E_CloseBtnButton
     	{
     		get
     		{
     			if (uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}

     			if( m_E_CloseBtnButton == null )
     			{
		    		m_E_CloseBtnButton = UIFindHelper.FindDeepChild<Button>(uiTransform.gameObject,"Panel/E_CloseBtn");
     			}

     			return m_E_CloseBtnButton;
     		}
     	}

		public ExtendImage E_CloseBtnExtendImage
     	{
     		get
     		{
     			if (uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}

     			if( m_E_CloseBtnExtendImage == null )
     			{
		    		m_E_CloseBtnExtendImage = UIFindHelper.FindDeepChild<ExtendImage>(uiTransform.gameObject,"Panel/E_CloseBtn");
     			}

     			return m_E_CloseBtnExtendImage;
     		}
     	}

		public void DestroyWidget()
		{
			m_E_TitleExtendText = null;
			m_E_ServerListLoopVerticalScrollRect = null;
			m_E_CloseBtnButton = null;
			m_E_CloseBtnExtendImage = null;
			uiTransform = null;
		}

		private ExtendText m_E_TitleExtendText = null;
		private LoopVerticalScrollRect m_E_ServerListLoopVerticalScrollRect = null;
		private Button m_E_CloseBtnButton = null;
		private ExtendImage m_E_CloseBtnExtendImage = null;
		public Transform uiTransform = null;
	}
}
