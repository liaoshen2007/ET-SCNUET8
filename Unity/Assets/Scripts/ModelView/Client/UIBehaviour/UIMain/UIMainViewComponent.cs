﻿using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class UIMainViewComponent : Entity, IAwake, IDestroy 
	{
		public Button E_ChatButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ChatButton == null )
     			{
		    		this.m_E_ChatButton = UIFindHelper.FindDeepChild<Button>(this.uiTransform.gameObject,"IconGroup/E_Chat");
     			}
     			return this.m_E_ChatButton;
     		}
     	}

		public ExtendImage E_ChatExtendImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ChatExtendImage == null )
     			{
		    		this.m_E_ChatExtendImage = UIFindHelper.FindDeepChild<ExtendImage>(this.uiTransform.gameObject,"IconGroup/E_Chat");
     			}
     			return this.m_E_ChatExtendImage;
     		}
     	}

		public Button E_RoleInfoButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_RoleInfoButton == null )
     			{
		    		this.m_E_RoleInfoButton = UIFindHelper.FindDeepChild<Button>(this.uiTransform.gameObject,"IconGroup/E_RoleInfo");
     			}
     			return this.m_E_RoleInfoButton;
     		}
     	}

		public ExtendImage E_RoleInfoExtendImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_RoleInfoExtendImage == null )
     			{
		    		this.m_E_RoleInfoExtendImage = UIFindHelper.FindDeepChild<ExtendImage>(this.uiTransform.gameObject,"IconGroup/E_RoleInfo");
     			}
     			return this.m_E_RoleInfoExtendImage;
     		}
     	}

		public Button E_NormHitButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_NormHitButton == null )
     			{
		    		this.m_E_NormHitButton = UIFindHelper.FindDeepChild<Button>(this.uiTransform.gameObject,"ActionGroup/E_NormHit");
     			}
     			return this.m_E_NormHitButton;
     		}
     	}

		public ExtendImage E_NormHitExtendImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_NormHitExtendImage == null )
     			{
		    		this.m_E_NormHitExtendImage = UIFindHelper.FindDeepChild<ExtendImage>(this.uiTransform.gameObject,"ActionGroup/E_NormHit");
     			}
     			return this.m_E_NormHitExtendImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_ChatButton = null;
			this.m_E_ChatExtendImage = null;
			this.m_E_RoleInfoButton = null;
			this.m_E_RoleInfoExtendImage = null;
			this.m_E_NormHitButton = null;
			this.m_E_NormHitExtendImage = null;
			uiTransform = null;
		}

		private Button m_E_ChatButton = null;
		private ExtendImage m_E_ChatExtendImage = null;
		private Button m_E_RoleInfoButton = null;
		private ExtendImage m_E_RoleInfoExtendImage = null;
		private Button m_E_NormHitButton = null;
		private ExtendImage m_E_NormHitExtendImage = null;
		public Transform uiTransform = null;
	}
}
