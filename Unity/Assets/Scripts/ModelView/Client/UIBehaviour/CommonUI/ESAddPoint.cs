using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[ChildOf]
	[EnableMethod]
	public partial class ESAddPoint : Entity, ET.IAwake<Transform>, IDestroy 
	{
		public Button EButton_AddPointButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_AddPointButton == null )
     			{
		    		this.m_EButton_AddPointButton = UIFindHelper.FindDeepChild<Button>(this.uiTransform.gameObject,"ELabel_AddPoint/EButton_AddPoint");
     			}
     			return this.m_EButton_AddPointButton;
     		}
     	}

		public Button EButton_ReducePointButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_ReducePointButton == null )
     			{
		    		this.m_EButton_ReducePointButton = UIFindHelper.FindDeepChild<Button>(this.uiTransform.gameObject,"ELabel_AddPoint/EButton_ReducePoint");
     			}
     			return this.m_EButton_ReducePointButton;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EButton_AddPointButton = null;
			this.m_EButton_ReducePointButton = null;
			uiTransform = null;
		}

		private Button m_EButton_AddPointButton = null;
		private Button m_EButton_ReducePointButton = null;
		public Transform uiTransform = null;
	}
}
