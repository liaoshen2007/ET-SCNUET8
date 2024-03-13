using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[EnableMethod]
	public partial class Scroll_Item_Role : Entity, IAwake, IDestroy, IUIScrollItem 
	{
		public long DataId {get;set;}
		private bool isCacheNode = false;

		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Button E_RoleImgButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_E_RoleImgButton == null )
     				{
		    			this.m_E_RoleImgButton = UIFindHelper.FindDeepChild<Button>(this.uiTransform.gameObject,"E_RoleImg");
     				}
     				return this.m_E_RoleImgButton;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<Button>(this.uiTransform.gameObject,"E_RoleImg");
     			}
     		}
     	}

		public ExtendImage E_RoleImgExtendImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_E_RoleImgExtendImage == null )
     				{
		    			this.m_E_RoleImgExtendImage = UIFindHelper.FindDeepChild<ExtendImage>(this.uiTransform.gameObject,"E_RoleImg");
     				}
     				return this.m_E_RoleImgExtendImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<ExtendImage>(this.uiTransform.gameObject,"E_RoleImg");
     			}
     		}
     	}

		public ExtendText E_NameExtendText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_E_NameExtendText == null )
     				{
		    			this.m_E_NameExtendText = UIFindHelper.FindDeepChild<ExtendText>(this.uiTransform.gameObject,"E_Name");
     				}
     				return this.m_E_NameExtendText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<ExtendText>(this.uiTransform.gameObject,"E_Name");
     			}
     		}
     	}

		public ExtendImage E_TagExtendImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_E_TagExtendImage == null )
     				{
		    			this.m_E_TagExtendImage = UIFindHelper.FindDeepChild<ExtendImage>(this.uiTransform.gameObject,"E_Tag");
     				}
     				return this.m_E_TagExtendImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<ExtendImage>(this.uiTransform.gameObject,"E_Tag");
     			}
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_RoleImgButton = null;
			this.m_E_RoleImgExtendImage = null;
			this.m_E_NameExtendText = null;
			this.m_E_TagExtendImage = null;
			uiTransform = null;
			this.DataId = 0;
		}

		private Button m_E_RoleImgButton = null;
		private ExtendImage m_E_RoleImgExtendImage = null;
		private ExtendText m_E_NameExtendText = null;
		private ExtendImage m_E_TagExtendImage = null;
		public Transform uiTransform = null;
	}
}
