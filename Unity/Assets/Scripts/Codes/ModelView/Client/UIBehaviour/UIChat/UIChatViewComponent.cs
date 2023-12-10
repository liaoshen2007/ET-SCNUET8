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
		    		this.m_E_BackBtnButton = UIFindHelper.FindDeepChild<Button>(this.uiTransform.gameObject,"EG_Chat/E_BackBtn");
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
		    		this.m_E_BackBtnExtendImage = UIFindHelper.FindDeepChild<ExtendImage>(this.uiTransform.gameObject,"EG_Chat/E_BackBtn");
     			}
     			return this.m_E_BackBtnExtendImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EG_ChatRectTransform = null;
			this.m_E_BackBtnButton = null;
			this.m_E_BackBtnExtendImage = null;
			uiTransform = null;
		}

		private RectTransform m_EG_ChatRectTransform = null;
		private Button m_E_BackBtnButton = null;
		private ExtendImage m_E_BackBtnExtendImage = null;
		public Transform uiTransform = null;
	}
}
