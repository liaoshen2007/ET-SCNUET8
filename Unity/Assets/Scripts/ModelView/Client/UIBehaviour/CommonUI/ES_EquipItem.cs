using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[ChildOf]
	[EnableMethod]
	public partial class ES_EquipItem : Entity, ET.IAwake<Transform>, IDestroy 
	{
		public Button E_SelectButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_SelectButton == null )
     			{
		    		this.m_E_SelectButton = UIFindHelper.FindDeepChild<Button>(this.uiTransform.gameObject,"E_Select");
     			}
     			return this.m_E_SelectButton;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_SelectButton = null;
			uiTransform = null;
		}

		private Button m_E_SelectButton = null;
		public Transform uiTransform = null;
	}
}
