using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[ChildOf]
	[EnableMethod]
	public partial class ESItem : Entity, ET.IAwake<Transform>, IDestroy 
	{
		public RectTransform EG_ContentRectTransform
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EG_ContentRectTransform == null )
     			{
		    		this.m_EG_ContentRectTransform = UIFindHelper.FindDeepChild<RectTransform>(this.uiTransform.gameObject,"EG_Content");
     			}
     			return this.m_EG_ContentRectTransform;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EG_ContentRectTransform = null;
			uiTransform = null;
		}

		private RectTransform m_EG_ContentRectTransform = null;
		public Transform uiTransform = null;
	}
}
