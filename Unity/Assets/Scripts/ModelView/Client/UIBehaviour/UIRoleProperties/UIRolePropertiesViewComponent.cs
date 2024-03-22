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

		public ExtendText ELabel_NickNameExtendText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabel_NickNameExtendText == null )
     			{
		    		this.m_ELabel_NickNameExtendText = UIFindHelper.FindDeepChild<ExtendText>(this.uiTransform.gameObject,"EGBackGround/PropertiesBg/NickNameBg/ELabel_NickName");
     			}
     			return this.m_ELabel_NickNameExtendText;
     		}
     	}

		public ExtendText ELabel_LevelExtendText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabel_LevelExtendText == null )
     			{
		    		this.m_ELabel_LevelExtendText = UIFindHelper.FindDeepChild<ExtendText>(this.uiTransform.gameObject,"EGBackGround/PropertiesBg/LevelBg/ELabel_Level");
     			}
     			return this.m_ELabel_LevelExtendText;
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

		public ExtendText ELabel_QuestExtendText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabel_QuestExtendText == null )
     			{
		    		this.m_ELabel_QuestExtendText = UIFindHelper.FindDeepChild<ExtendText>(this.uiTransform.gameObject,"EGBackGround/PropertiesBg/TransferBtn/EButton_Quest/ELabel_Quest");
     			}
     			return this.m_ELabel_QuestExtendText;
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

		public ExtendText ELabel_InventoryExtendText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabel_InventoryExtendText == null )
     			{
		    		this.m_ELabel_InventoryExtendText = UIFindHelper.FindDeepChild<ExtendText>(this.uiTransform.gameObject,"EGBackGround/PropertiesBg/TransferBtn/EButton_Inventory/ELabel_Inventory");
     			}
     			return this.m_ELabel_InventoryExtendText;
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

		public ExtendText ELabel_MapExtendText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabel_MapExtendText == null )
     			{
		    		this.m_ELabel_MapExtendText = UIFindHelper.FindDeepChild<ExtendText>(this.uiTransform.gameObject,"EGBackGround/PropertiesBg/TransferBtn/EButton_Map/ELabel_Map");
     			}
     			return this.m_ELabel_MapExtendText;
     		}
     	}

		public ExtendText ELabel_DamageExtendText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabel_DamageExtendText == null )
     			{
		    		this.m_ELabel_DamageExtendText = UIFindHelper.FindDeepChild<ExtendText>(this.uiTransform.gameObject,"EGBackGround/PropertiesBg/PropertiesInfo/Bg/ELabel_Damage");
     			}
     			return this.m_ELabel_DamageExtendText;
     		}
     	}

		public ExtendText ELabel_HealthExtendText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabel_HealthExtendText == null )
     			{
		    		this.m_ELabel_HealthExtendText = UIFindHelper.FindDeepChild<ExtendText>(this.uiTransform.gameObject,"EGBackGround/PropertiesBg/PropertiesInfo/Bg/ELabel_Health");
     			}
     			return this.m_ELabel_HealthExtendText;
     		}
     	}

		public ExtendText ELabel_ManaExtendText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabel_ManaExtendText == null )
     			{
		    		this.m_ELabel_ManaExtendText = UIFindHelper.FindDeepChild<ExtendText>(this.uiTransform.gameObject,"EGBackGround/PropertiesBg/PropertiesInfo/Bg/ELabel_Mana");
     			}
     			return this.m_ELabel_ManaExtendText;
     		}
     	}

		public ExtendText ELabel_ArmorExtendText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabel_ArmorExtendText == null )
     			{
		    		this.m_ELabel_ArmorExtendText = UIFindHelper.FindDeepChild<ExtendText>(this.uiTransform.gameObject,"EGBackGround/PropertiesBg/PropertiesInfo/Bg/ELabel_Armor");
     			}
     			return this.m_ELabel_ArmorExtendText;
     		}
     	}

		public ExtendText ELabel_CriticalChanceExtendText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabel_CriticalChanceExtendText == null )
     			{
		    		this.m_ELabel_CriticalChanceExtendText = UIFindHelper.FindDeepChild<ExtendText>(this.uiTransform.gameObject,"EGBackGround/PropertiesBg/PropertiesInfo/Bg/ELabel_CriticalChance");
     			}
     			return this.m_ELabel_CriticalChanceExtendText;
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

		public ExtendText ELabel_ArmorChangeExtendText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabel_ArmorChangeExtendText == null )
     			{
		    		this.m_ELabel_ArmorChangeExtendText = UIFindHelper.FindDeepChild<ExtendText>(this.uiTransform.gameObject,"EGBackGround/PropertiesBg/PropertiesInfo/Bg/ELabel_ArmorChange");
     			}
     			return this.m_ELabel_ArmorChangeExtendText;
     		}
     	}

		public ExtendText ELabel_PointExtendText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabel_PointExtendText == null )
     			{
		    		this.m_ELabel_PointExtendText = UIFindHelper.FindDeepChild<ExtendText>(this.uiTransform.gameObject,"EGBackGround/PropertiesBg/PropertiesInfo/Bg/ELabel_Point");
     			}
     			return this.m_ELabel_PointExtendText;
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

		public ExtendText ELabel_UpLevelExtendText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabel_UpLevelExtendText == null )
     			{
		    		this.m_ELabel_UpLevelExtendText = UIFindHelper.FindDeepChild<ExtendText>(this.uiTransform.gameObject,"EGBackGround/PropertiesBg/EButton_UpLevel/ELabel_UpLevel");
     			}
     			return this.m_ELabel_UpLevelExtendText;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EGBackGroundRectTransform = null;
			this.m_EButton_CloseButton = null;
			this.m_ELabel_NickNameExtendText = null;
			this.m_ELabel_LevelExtendText = null;
			this.m_es_equipitem_head = null;
			this.m_es_equipitem_cloths = null;
			this.m_es_equipitem_ring = null;
			this.m_es_equipitem_shoes = null;
			this.m_es_equipitem_weapon = null;
			this.m_es_equipitem_shield = null;
			this.m_EButton_QuestButton = null;
			this.m_ELabel_QuestExtendText = null;
			this.m_EButton_InventoryButton = null;
			this.m_ELabel_InventoryExtendText = null;
			this.m_EButton_MapButton = null;
			this.m_ELabel_MapExtendText = null;
			this.m_ELabel_DamageExtendText = null;
			this.m_ELabel_HealthExtendText = null;
			this.m_ELabel_ManaExtendText = null;
			this.m_ELabel_ArmorExtendText = null;
			this.m_ELabel_CriticalChanceExtendText = null;
			this.m_EButton_AddStrengthButton = null;
			this.m_EButton_ReduceStrengthButton = null;
			this.m_EButton_AddAgilityButton = null;
			this.m_EButton_ReduceAgilityButton = null;
			this.m_EButton_AddIntelligenceButton = null;
			this.m_EButton_ReduceIntelligenceButton = null;
			this.m_EButton_AddWillButton = null;
			this.m_EButton_ReduceWillButton = null;
			this.m_ELabel_ArmorChangeExtendText = null;
			this.m_ELabel_PointExtendText = null;
			this.m_esaddpoint = null;
			this.m_esaddpoint1 = null;
			this.m_esaddpoint2 = null;
			this.m_esaddpoint3 = null;
			this.m_EButton_UpLevelButton = null;
			this.m_ELabel_UpLevelExtendText = null;
			uiTransform = null;
		}

		private RectTransform m_EGBackGroundRectTransform = null;
		private Button m_EButton_CloseButton = null;
		private ExtendText m_ELabel_NickNameExtendText = null;
		private ExtendText m_ELabel_LevelExtendText = null;
		private EntityRef<ES_EquipItem> m_es_equipitem_head = null;
		private EntityRef<ES_EquipItem> m_es_equipitem_cloths = null;
		private EntityRef<ES_EquipItem> m_es_equipitem_ring = null;
		private EntityRef<ES_EquipItem> m_es_equipitem_shoes = null;
		private EntityRef<ES_EquipItem> m_es_equipitem_weapon = null;
		private EntityRef<ES_EquipItem> m_es_equipitem_shield = null;
		private Button m_EButton_QuestButton = null;
		private ExtendText m_ELabel_QuestExtendText = null;
		private Button m_EButton_InventoryButton = null;
		private ExtendText m_ELabel_InventoryExtendText = null;
		private Button m_EButton_MapButton = null;
		private ExtendText m_ELabel_MapExtendText = null;
		private ExtendText m_ELabel_DamageExtendText = null;
		private ExtendText m_ELabel_HealthExtendText = null;
		private ExtendText m_ELabel_ManaExtendText = null;
		private ExtendText m_ELabel_ArmorExtendText = null;
		private ExtendText m_ELabel_CriticalChanceExtendText = null;
		private Button m_EButton_AddStrengthButton = null;
		private Button m_EButton_ReduceStrengthButton = null;
		private Button m_EButton_AddAgilityButton = null;
		private Button m_EButton_ReduceAgilityButton = null;
		private Button m_EButton_AddIntelligenceButton = null;
		private Button m_EButton_ReduceIntelligenceButton = null;
		private Button m_EButton_AddWillButton = null;
		private Button m_EButton_ReduceWillButton = null;
		private ExtendText m_ELabel_ArmorChangeExtendText = null;
		private ExtendText m_ELabel_PointExtendText = null;
		private EntityRef<ESAddPoint> m_esaddpoint = null;
		private EntityRef<ESAddPoint> m_esaddpoint1 = null;
		private EntityRef<ESAddPoint> m_esaddpoint2 = null;
		private EntityRef<ESAddPoint> m_esaddpoint3 = null;
		private Button m_EButton_UpLevelButton = null;
		private ExtendText m_ELabel_UpLevelExtendText = null;
		public Transform uiTransform = null;
	}
}
