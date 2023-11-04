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

		public Button E_ServerButton
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
     				if( m_E_ServerButton == null )
     				{
		    			m_E_ServerButton = UIFindHelper.FindDeepChild<Button>(uiTransform.gameObject,"E_Server");
     				}

     				return m_E_ServerButton;
     			}
     			else
     			{

		    		return UIFindHelper.FindDeepChild<Button>(uiTransform.gameObject,"E_Server");
     			}
     		}
     	}

		public ExtendImage E_ServerExtendImage
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
     				if( m_E_ServerExtendImage == null )
     				{
		    			m_E_ServerExtendImage = UIFindHelper.FindDeepChild<ExtendImage>(uiTransform.gameObject,"E_Server");
     				}

     				return m_E_ServerExtendImage;
     			}
     			else
     			{

		    		return UIFindHelper.FindDeepChild<ExtendImage>(uiTransform.gameObject,"E_Server");
     			}
     		}
     	}

		public ExtendText E_NameExtendText
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
     				if( m_E_NameExtendText == null )
     				{
		    			m_E_NameExtendText = UIFindHelper.FindDeepChild<ExtendText>(uiTransform.gameObject,"E_Name");
     				}

     				return m_E_NameExtendText;
     			}
     			else
     			{

		    		return UIFindHelper.FindDeepChild<ExtendText>(uiTransform.gameObject,"E_Name");
     			}
     		}
     	}

		public ExtendImage E_TagExtendImage
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
     				if( m_E_TagExtendImage == null )
     				{
		    			m_E_TagExtendImage = UIFindHelper.FindDeepChild<ExtendImage>(uiTransform.gameObject,"E_Tag");
     				}

     				return m_E_TagExtendImage;
     			}
     			else
     			{

		    		return UIFindHelper.FindDeepChild<ExtendImage>(uiTransform.gameObject,"E_Tag");
     			}
     		}
     	}

		public void DestroyWidget()
		{
			m_E_ServerButton = null;
			m_E_ServerExtendImage = null;
			m_E_NameExtendText = null;
			m_E_TagExtendImage = null;
			uiTransform = null;
		}

		private Button m_E_ServerButton = null;
		private ExtendImage m_E_ServerExtendImage = null;
		private ExtendText m_E_NameExtendText = null;
		private ExtendImage m_E_TagExtendImage = null;
		public Transform uiTransform = null;
	}
}
