using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class UIMainViewComponent : Entity, IAwake, IDestroy 
	{
		public Image E_HeadImage
     	{
     		get
     		{
     			if (uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}

     			if( m_E_HeadImage == null )
     			{
		    		m_E_HeadImage = UIFindHelper.FindDeepChild<Image>(uiTransform.gameObject,"Head/E_Head");
     			}

     			return m_E_HeadImage;
     		}
     	}

		public void DestroyWidget()
		{
			m_E_HeadImage = null;
			uiTransform = null;
		}

		private Image m_E_HeadImage = null;
		public Transform uiTransform = null;
	}
}
