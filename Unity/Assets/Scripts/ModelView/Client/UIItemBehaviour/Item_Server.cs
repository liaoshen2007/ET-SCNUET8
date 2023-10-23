using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[EnableMethod]
	public  class Scroll_Item_Server : Entity, IAwake, IDestroy, IUIScrollItem 
	{
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_Item_Server BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
		}

		public Image EI_serverTestImage
     	{
     		get
     		{
     			if (uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}

     			if (isCacheNode)
     			{
     				if( m_EI_serverTestImage == null )
     				{
		    			m_EI_serverTestImage = UIFindHelper.FindDeepChild<Image>(uiTransform.gameObject,"EI_serverTest");
     				}

     				return m_EI_serverTestImage;
     			}
     			else
     			{

		    		return UIFindHelper.FindDeepChild<Image>(uiTransform.gameObject,"EI_serverTest");
     			}
     		}
     	}

		public Button E_SelectButton
     	{
     		get
     		{
     			if (uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}

     			if (isCacheNode)
     			{
     				if( m_E_SelectButton == null )
     				{
		    			m_E_SelectButton = UIFindHelper.FindDeepChild<Button>(uiTransform.gameObject,"E_Select");
     				}

     				return m_E_SelectButton;
     			}
     			else
     			{

		    		return UIFindHelper.FindDeepChild<Button>(uiTransform.gameObject,"E_Select");
     			}
     		}
     	}

		public Image E_SelectImage
     	{
     		get
     		{
     			if (uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}

     			if (isCacheNode)
     			{
     				if( m_E_SelectImage == null )
     				{
		    			m_E_SelectImage = UIFindHelper.FindDeepChild<Image>(uiTransform.gameObject,"E_Select");
     				}

     				return m_E_SelectImage;
     			}
     			else
     			{

		    		return UIFindHelper.FindDeepChild<Image>(uiTransform.gameObject,"E_Select");
     			}
     		}
     	}

		public Text E_serverTestTipText
     	{
     		get
     		{
     			if (uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}

     			if (isCacheNode)
     			{
     				if( m_E_serverTestTipText == null )
     				{
		    			m_E_serverTestTipText = UIFindHelper.FindDeepChild<Text>(uiTransform.gameObject,"E_serverTestTip");
     				}

     				return m_E_serverTestTipText;
     			}
     			else
     			{

		    		return UIFindHelper.FindDeepChild<Text>(uiTransform.gameObject,"E_serverTestTip");
     			}
     		}
     	}

		public void DestroyWidget()
		{
			m_EI_serverTestImage = null;
			m_E_SelectButton = null;
			m_E_SelectImage = null;
			m_E_serverTestTipText = null;
			uiTransform = null;
		}

		private Image m_EI_serverTestImage = null;
		private Button m_E_SelectButton = null;
		private Image m_E_SelectImage = null;
		private Text m_E_serverTestTipText = null;
		public Transform uiTransform = null;
	}
}
