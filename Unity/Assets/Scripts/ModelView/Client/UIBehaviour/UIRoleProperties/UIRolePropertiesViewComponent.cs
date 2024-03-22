using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class UIRolePropertiesViewComponent : Entity, IAwake, IDestroy 
	{
		public RectTransform EGBackGroundRectTransform
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EGBackGroundRectTransform == null )
     			{
		    		this.m_EGBackGroundRectTransform = UIFindHelper.FindDeepChild<RectTransform>(this.uiTransform.gameObject,"EGBackGround");
     			}
     			return this.m_EGBackGroundRectTransform;
     		}
     	}

		public Button EButton_CloseButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_CloseButton == null )
     			{
		    		this.m_EButton_CloseButton = UIFindHelper.FindDeepChild<Button>(this.uiTransform.gameObject,"EGBackGround/PropertiesBg/EButton_Close");
     			}
     			return this.m_EButton_CloseButton;
     		}
     	}

		public ES_EquipItem ES_EquipItem_Head
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_es_equipitem_head == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"EGBackGround/PropertiesBg/equipGroup/ES_EquipItem_Head");
		    	   this.m_es_equipitem_head = this.AddChild<ES_EquipItem,Transform>(subTrans);
     			}
     			return this.m_es_equipitem_head;
     		}
     	}

		public ES_EquipItem ES_EquipItem_Cloths
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_es_equipitem_cloths == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"EGBackGround/PropertiesBg/equipGroup/ES_EquipItem_Cloths");
		    	   this.m_es_equipitem_cloths = this.AddChild<ES_EquipItem,Transform>(subTrans);
     			}
     			return this.m_es_equipitem_cloths;
     		}
     	}

		public ES_EquipItem ES_EquipItem_Ring
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_es_equipitem_ring == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"EGBackGround/PropertiesBg/equipGroup/ES_EquipItem_Ring");
		    	   this.m_es_equipitem_ring = this.AddChild<ES_EquipItem,Transform>(subTrans);
     			}
     			return this.m_es_equipitem_ring;
     		}
     	}

		public ES_EquipItem ES_EquipItem_Shoes
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_es_equipitem_shoes == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"EGBackGround/PropertiesBg/equipGroup/ES_EquipItem_Shoes");
		    	   this.m_es_equipitem_shoes = this.AddChild<ES_EquipItem,Transform>(subTrans);
     			}
     			return this.m_es_equipitem_shoes;
     		}
     	}

		public ES_EquipItem ES_EquipItem_Weapon
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_es_equipitem_weapon == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"EGBackGround/PropertiesBg/equipGroup/ES_EquipItem_Weapon");
		    	   this.m_es_equipitem_weapon = this.AddChild<ES_EquipItem,Transform>(subTrans);
     			}
     			return this.m_es_equipitem_weapon;
     		}
     	}

		public ES_EquipItem ES_EquipItem_Shield
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_es_equipitem_shield == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"EGBackGround/PropertiesBg/equipGroup/ES_EquipItem_Shield");
		    	   this.m_es_equipitem_shield = this.AddChild<ES_EquipItem,Transform>(subTrans);
     			}
     			return this.m_es_equipitem_shield;
     		}
     	}

		public Button EButton_QuestButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_QuestButton == null )
     			{
		    		this.m_EButton_QuestButton = UIFindHelper.FindDeepChild<Button>(this.uiTransform.gameObject,"EGBackGround/PropertiesBg/TransferBtn/EButton_Quest");
     			}
     			return this.m_EButton_QuestButton;
     		}
     	}

		public Button EButton_InventoryButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_InventoryButton == null )
     			{
		    		this.m_EButton_InventoryButton = UIFindHelper.FindDeepChild<Button>(this.uiTransform.gameObject,"EGBackGround/PropertiesBg/TransferBtn/EButton_Inventory");
     			}
     			return this.m_EButton_InventoryButton;
     		}
     	}

		public Button EButton_MapButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_MapButton == null )
     			{
		    		this.m_EButton_MapButton = UIFindHelper.FindDeepChild<Button>(this.uiTransform.gameObject,"EGBackGround/PropertiesBg/TransferBtn/EButton_Map");
     			}
     			return this.m_EButton_MapButton;
     		}
     	}

		public Button EButton_AddStrengthButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_AddStrengthButton == null )
     			{
		    		this.m_EButton_AddStrengthButton = UIFindHelper.FindDeepChild<Button>(this.uiTransform.gameObject,"EGBackGround/PropertiesBg/PropertiesInfo/Bg/ELabel_Strength/EButton_AddStrength");
     			}
     			return this.m_EButton_AddStrengthButton;
     		}
     	}

		public Button EButton_ReduceStrengthButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_ReduceStrengthButton == null )
     			{
		    		this.m_EButton_ReduceStrengthButton = UIFindHelper.FindDeepChild<Button>(this.uiTransform.gameObject,"EGBackGround/PropertiesBg/PropertiesInfo/Bg/ELabel_Strength/EButton_ReduceStrength");
     			}
     			return this.m_EButton_ReduceStrengthButton;
     		}
     	}

		public Button EButton_AddAgilityButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_AddAgilityButton == null )
     			{
		    		this.m_EButton_AddAgilityButton = UIFindHelper.FindDeepChild<Button>(this.uiTransform.gameObject,"EGBackGround/PropertiesBg/PropertiesInfo/Bg/ELabel_Agility/EButton_AddAgility");
     			}
     			return this.m_EButton_AddAgilityButton;
     		}
     	}

		public Button EButton_ReduceAgilityButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_ReduceAgilityButton == null )
     			{
		    		this.m_EButton_ReduceAgilityButton = UIFindHelper.FindDeepChild<Button>(this.uiTransform.gameObject,"EGBackGround/PropertiesBg/PropertiesInfo/Bg/ELabel_Agility/EButton_ReduceAgility");
     			}
     			return this.m_EButton_ReduceAgilityButton;
     		}
     	}

		public Button EButton_AddIntelligenceButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_AddIntelligenceButton == null )
     			{
		    		this.m_EButton_AddIntelligenceButton = UIFindHelper.FindDeepChild<Button>(this.uiTransform.gameObject,"EGBackGround/PropertiesBg/PropertiesInfo/Bg/ELabel_Intelligence/EButton_AddIntelligence");
     			}
     			return this.m_EButton_AddIntelligenceButton;
     		}
     	}

		public Button EButton_ReduceIntelligenceButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_ReduceIntelligenceButton == null )
     			{
		    		this.m_EButton_ReduceIntelligenceButton = UIFindHelper.FindDeepChild<Button>(this.uiTransform.gameObject,"EGBackGround/PropertiesBg/PropertiesInfo/Bg/ELabel_Intelligence/EButton_ReduceIntelligence");
     			}
     			return this.m_EButton_ReduceIntelligenceButton;
     		}
     	}

		public Button EButton_AddWillButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_AddWillButton == null )
     			{
		    		this.m_EButton_AddWillButton = UIFindHelper.FindDeepChild<Button>(this.uiTransform.gameObject,"EGBackGround/PropertiesBg/PropertiesInfo/Bg/ELabel_Will/EButton_AddWill");
     			}
     			return this.m_EButton_AddWillButton;
     		}
     	}

		public Button EButton_ReduceWillButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_ReduceWillButton == null )
     			{
		    		this.m_EButton_ReduceWillButton = UIFindHelper.FindDeepChild<Button>(this.uiTransform.gameObject,"EGBackGround/PropertiesBg/PropertiesInfo/Bg/ELabel_Will/EButton_ReduceWill");
     			}
     			return this.m_EButton_ReduceWillButton;
     		}
     	}

		public ESAddPoint ESAddPoint
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_esaddpoint == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"EGBackGround/PropertiesBg/PropertiesInfo/Bg/ESAddPoint");
		    	   this.m_esaddpoint = this.AddChild<ESAddPoint,Transform>(subTrans);
     			}
     			return this.m_esaddpoint;
     		}
     	}

		public ESAddPoint ESAddPoint1
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_esaddpoint1 == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"EGBackGround/PropertiesBg/PropertiesInfo/Bg/ESAddPoint1");
		    	   this.m_esaddpoint1 = this.AddChild<ESAddPoint,Transform>(subTrans);
     			}
     			return this.m_esaddpoint1;
     		}
     	}

		public ESAddPoint ESAddPoint2
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_esaddpoint2 == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"EGBackGround/PropertiesBg/PropertiesInfo/Bg/ESAddPoint2");
		    	   this.m_esaddpoint2 = this.AddChild<ESAddPoint,Transform>(subTrans);
     			}
     			return this.m_esaddpoint2;
     		}
     	}

		public ESAddPoint ESAddPoint3
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_esaddpoint3 == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"EGBackGround/PropertiesBg/PropertiesInfo/Bg/ESAddPoint3");
		    	   this.m_esaddpoint3 = this.AddChild<ESAddPoint,Transform>(subTrans);
     			}
     			return this.m_esaddpoint3;
     		}
     	}

		public Button EButton_UpLevelButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_UpLevelButton == null )
     			{
		    		this.m_EButton_UpLevelButton = UIFindHelper.FindDeepChild<Button>(this.uiTransform.gameObject,"EGBackGround/PropertiesBg/EButton_UpLevel");
     			}
     			return this.m_EButton_UpLevelButton;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EGBackGroundRectTransform = null;
			this.m_EButton_CloseButton = null;
			this.m_es_equipitem_head = null;
			this.m_es_equipitem_cloths = null;
			this.m_es_equipitem_ring = null;
			this.m_es_equipitem_shoes = null;
			this.m_es_equipitem_weapon = null;
			this.m_es_equipitem_shield = null;
			this.m_EButton_QuestButton = null;
			this.m_EButton_InventoryButton = null;
			this.m_EButton_MapButton = null;
			this.m_EButton_AddStrengthButton = null;
			this.m_EButton_ReduceStrengthButton = null;
			this.m_EButton_AddAgilityButton = null;
			this.m_EButton_ReduceAgilityButton = null;
			this.m_EButton_AddIntelligenceButton = null;
			this.m_EButton_ReduceIntelligenceButton = null;
			this.m_EButton_AddWillButton = null;
			this.m_EButton_ReduceWillButton = null;
			this.m_esaddpoint = null;
			this.m_esaddpoint1 = null;
			this.m_esaddpoint2 = null;
			this.m_esaddpoint3 = null;
			this.m_EButton_UpLevelButton = null;
			uiTransform = null;
		}

		private RectTransform m_EGBackGroundRectTransform = null;
		private Button m_EButton_CloseButton = null;
		private EntityRef<ES_EquipItem> m_es_equipitem_head = null;
		private EntityRef<ES_EquipItem> m_es_equipitem_cloths = null;
		private EntityRef<ES_EquipItem> m_es_equipitem_ring = null;
		private EntityRef<ES_EquipItem> m_es_equipitem_shoes = null;
		private EntityRef<ES_EquipItem> m_es_equipitem_weapon = null;
		private EntityRef<ES_EquipItem> m_es_equipitem_shield = null;
		private Button m_EButton_QuestButton = null;
		private Button m_EButton_InventoryButton = null;
		private Button m_EButton_MapButton = null;
		private Button m_EButton_AddStrengthButton = null;
		private Button m_EButton_ReduceStrengthButton = null;
		private Button m_EButton_AddAgilityButton = null;
		private Button m_EButton_ReduceAgilityButton = null;
		private Button m_EButton_AddIntelligenceButton = null;
		private Button m_EButton_ReduceIntelligenceButton = null;
		private Button m_EButton_AddWillButton = null;
		private Button m_EButton_ReduceWillButton = null;
		private EntityRef<ESAddPoint> m_esaddpoint = null;
		private EntityRef<ESAddPoint> m_esaddpoint1 = null;
		private EntityRef<ESAddPoint> m_esaddpoint2 = null;
		private EntityRef<ESAddPoint> m_esaddpoint3 = null;
		private Button m_EButton_UpLevelButton = null;
		public Transform uiTransform = null;
	}
}
