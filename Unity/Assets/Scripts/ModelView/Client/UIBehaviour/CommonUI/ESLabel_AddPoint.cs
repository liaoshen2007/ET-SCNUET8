using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[ChildOf]
	[EnableMethod]
	public partial class ESLabel_AddPoint : Entity, ET.IAwake<Transform>, IDestroy 
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
		    		this.m_EButton_AddPointButton = UIFindHelper.FindDeepChild<Button>(this.uiTransform.gameObject,"EButton_AddPoint");
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
		    		this.m_EButton_ReducePointButton = UIFindHelper.FindDeepChild<Button>(this.uiTransform.gameObject,"EButton_ReducePoint");
     			}
     			return this.m_EButton_ReducePointButton;
     		}
     	}

		public ExtendText E_AddPointExtendText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AddPointExtendText == null )
     			{
		    		this.m_E_AddPointExtendText = UIFindHelper.FindDeepChild<ExtendText>(this.uiTransform.gameObject,"E_AddPoint");
     			}
     			return this.m_E_AddPointExtendText;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EButton_AddPointButton = null;
			this.m_EButton_ReducePointButton = null;
			this.m_E_AddPointExtendText = null;
			uiTransform = null;
		}

		private Button m_EButton_AddPointButton = null;
		private Button m_EButton_ReducePointButton = null;
		private ExtendText m_E_AddPointExtendText = null;
		public Transform uiTransform = null;
	}
}
