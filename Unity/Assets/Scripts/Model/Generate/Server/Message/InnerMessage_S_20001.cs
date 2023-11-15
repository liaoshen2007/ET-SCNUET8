using ET;
using MemoryPack;
using System.Collections.Generic;
namespace ET
{
	/// <summary>
	/// using
	/// </summary>
	[ResponseType(nameof(InnerPingResponse))]
	[Message(InnerMessage.InnerPingRequest)]
	[MemoryPackable]
	public partial class InnerPingRequest: MessageObject, IRequest
	{
		public static InnerPingRequest Create(bool isFromPool = true) 
		{ 
			return !isFromPool? new InnerPingRequest() : ObjectPool.Instance.Fetch(typeof(InnerPingRequest)) as InnerPingRequest; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(InnerMessage.InnerPingResponse)]
	[MemoryPackable]
	public partial class InnerPingResponse: MessageObject, IResponse
	{
		public static InnerPingResponse Create(bool isFromPool = true) 
		{ 
			return !isFromPool? new InnerPingResponse() : ObjectPool.Instance.Fetch(typeof(InnerPingResponse)) as InnerPingResponse; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[ResponseType(nameof(ObjectQueryResponse))]
	[Message(InnerMessage.ObjectQueryRequest)]
	[MemoryPackable]
	public partial class ObjectQueryRequest: MessageObject, IRequest
	{
		public static ObjectQueryRequest Create(bool isFromPool = true) 
		{ 
			return !isFromPool? new ObjectQueryRequest() : ObjectPool.Instance.Fetch(typeof(ObjectQueryRequest)) as ObjectQueryRequest; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(1)]
		public long Key { get; set; }

		[MemoryPackOrder(2)]
		public long InstanceId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Key = default;
			this.InstanceId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[ResponseType(nameof(A2M_Reload))]
	[Message(InnerMessage.M2A_Reload)]
	[MemoryPackable]
	public partial class M2A_Reload: MessageObject, IRequest
	{
		public static M2A_Reload Create(bool isFromPool = true) 
		{ 
			return !isFromPool? new M2A_Reload() : ObjectPool.Instance.Fetch(typeof(M2A_Reload)) as M2A_Reload; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(InnerMessage.A2M_Reload)]
	[MemoryPackable]
	public partial class A2M_Reload: MessageObject, IResponse
	{
		public static A2M_Reload Create(bool isFromPool = true) 
		{ 
			return !isFromPool? new A2M_Reload() : ObjectPool.Instance.Fetch(typeof(A2M_Reload)) as A2M_Reload; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[ResponseType(nameof(G2G_LockResponse))]
	[Message(InnerMessage.G2G_LockRequest)]
	[MemoryPackable]
	public partial class G2G_LockRequest: MessageObject, IRequest
	{
		public static G2G_LockRequest Create(bool isFromPool = true) 
		{ 
			return !isFromPool? new G2G_LockRequest() : ObjectPool.Instance.Fetch(typeof(G2G_LockRequest)) as G2G_LockRequest; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(1)]
		public long Id { get; set; }

		[MemoryPackOrder(2)]
		public string Address { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Id = default;
			this.Address = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(InnerMessage.G2G_LockResponse)]
	[MemoryPackable]
	public partial class G2G_LockResponse: MessageObject, IResponse
	{
		public static G2G_LockResponse Create(bool isFromPool = true) 
		{ 
			return !isFromPool? new G2G_LockResponse() : ObjectPool.Instance.Fetch(typeof(G2G_LockResponse)) as G2G_LockResponse; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[ResponseType(nameof(G2G_LockReleaseResponse))]
	[Message(InnerMessage.G2G_LockReleaseRequest)]
	[MemoryPackable]
	public partial class G2G_LockReleaseRequest: MessageObject, IRequest
	{
		public static G2G_LockReleaseRequest Create(bool isFromPool = true) 
		{ 
			return !isFromPool? new G2G_LockReleaseRequest() : ObjectPool.Instance.Fetch(typeof(G2G_LockReleaseRequest)) as G2G_LockReleaseRequest; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(1)]
		public long Id { get; set; }

		[MemoryPackOrder(2)]
		public string Address { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Id = default;
			this.Address = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(InnerMessage.G2G_LockReleaseResponse)]
	[MemoryPackable]
	public partial class G2G_LockReleaseResponse: MessageObject, IResponse
	{
		public static G2G_LockReleaseResponse Create(bool isFromPool = true) 
		{ 
			return !isFromPool? new G2G_LockReleaseResponse() : ObjectPool.Instance.Fetch(typeof(G2G_LockReleaseResponse)) as G2G_LockReleaseResponse; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[ResponseType(nameof(ObjectAddResponse))]
	[Message(InnerMessage.ObjectAddRequest)]
	[MemoryPackable]
	public partial class ObjectAddRequest: MessageObject, IRequest
	{
		public static ObjectAddRequest Create(bool isFromPool = true) 
		{ 
			return !isFromPool? new ObjectAddRequest() : ObjectPool.Instance.Fetch(typeof(ObjectAddRequest)) as ObjectAddRequest; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(1)]
		public int Type { get; set; }

		[MemoryPackOrder(2)]
		public long Key { get; set; }

		[MemoryPackOrder(3)]
		public ActorId ActorId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Type = default;
			this.Key = default;
			this.ActorId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(InnerMessage.ObjectAddResponse)]
	[MemoryPackable]
	public partial class ObjectAddResponse: MessageObject, IResponse
	{
		public static ObjectAddResponse Create(bool isFromPool = true) 
		{ 
			return !isFromPool? new ObjectAddResponse() : ObjectPool.Instance.Fetch(typeof(ObjectAddResponse)) as ObjectAddResponse; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[ResponseType(nameof(ObjectLockResponse))]
	[Message(InnerMessage.ObjectLockRequest)]
	[MemoryPackable]
	public partial class ObjectLockRequest: MessageObject, IRequest
	{
		public static ObjectLockRequest Create(bool isFromPool = true) 
		{ 
			return !isFromPool? new ObjectLockRequest() : ObjectPool.Instance.Fetch(typeof(ObjectLockRequest)) as ObjectLockRequest; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(1)]
		public int Type { get; set; }

		[MemoryPackOrder(2)]
		public long Key { get; set; }

		[MemoryPackOrder(3)]
		public ActorId ActorId { get; set; }

		[MemoryPackOrder(4)]
		public int Time { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Type = default;
			this.Key = default;
			this.ActorId = default;
			this.Time = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(InnerMessage.ObjectLockResponse)]
	[MemoryPackable]
	public partial class ObjectLockResponse: MessageObject, IResponse
	{
		public static ObjectLockResponse Create(bool isFromPool = true) 
		{ 
			return !isFromPool? new ObjectLockResponse() : ObjectPool.Instance.Fetch(typeof(ObjectLockResponse)) as ObjectLockResponse; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[ResponseType(nameof(ObjectUnLockResponse))]
	[Message(InnerMessage.ObjectUnLockRequest)]
	[MemoryPackable]
	public partial class ObjectUnLockRequest: MessageObject, IRequest
	{
		public static ObjectUnLockRequest Create(bool isFromPool = true) 
		{ 
			return !isFromPool? new ObjectUnLockRequest() : ObjectPool.Instance.Fetch(typeof(ObjectUnLockRequest)) as ObjectUnLockRequest; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(1)]
		public int Type { get; set; }

		[MemoryPackOrder(2)]
		public long Key { get; set; }

		[MemoryPackOrder(3)]
		public ActorId OldActorId { get; set; }

		[MemoryPackOrder(4)]
		public ActorId NewActorId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Type = default;
			this.Key = default;
			this.OldActorId = default;
			this.NewActorId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(InnerMessage.ObjectUnLockResponse)]
	[MemoryPackable]
	public partial class ObjectUnLockResponse: MessageObject, IResponse
	{
		public static ObjectUnLockResponse Create(bool isFromPool = true) 
		{ 
			return !isFromPool? new ObjectUnLockResponse() : ObjectPool.Instance.Fetch(typeof(ObjectUnLockResponse)) as ObjectUnLockResponse; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[ResponseType(nameof(ObjectRemoveResponse))]
	[Message(InnerMessage.ObjectRemoveRequest)]
	[MemoryPackable]
	public partial class ObjectRemoveRequest: MessageObject, IRequest
	{
		public static ObjectRemoveRequest Create(bool isFromPool = true) 
		{ 
			return !isFromPool? new ObjectRemoveRequest() : ObjectPool.Instance.Fetch(typeof(ObjectRemoveRequest)) as ObjectRemoveRequest; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(1)]
		public int Type { get; set; }

		[MemoryPackOrder(2)]
		public long Key { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Type = default;
			this.Key = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(InnerMessage.ObjectRemoveResponse)]
	[MemoryPackable]
	public partial class ObjectRemoveResponse: MessageObject, IResponse
	{
		public static ObjectRemoveResponse Create(bool isFromPool = true) 
		{ 
			return !isFromPool? new ObjectRemoveResponse() : ObjectPool.Instance.Fetch(typeof(ObjectRemoveResponse)) as ObjectRemoveResponse; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[ResponseType(nameof(ObjectGetResponse))]
	[Message(InnerMessage.ObjectGetRequest)]
	[MemoryPackable]
	public partial class ObjectGetRequest: MessageObject, IRequest
	{
		public static ObjectGetRequest Create(bool isFromPool = true) 
		{ 
			return !isFromPool? new ObjectGetRequest() : ObjectPool.Instance.Fetch(typeof(ObjectGetRequest)) as ObjectGetRequest; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(1)]
		public int Type { get; set; }

		[MemoryPackOrder(2)]
		public long Key { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Type = default;
			this.Key = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(InnerMessage.ObjectGetResponse)]
	[MemoryPackable]
	public partial class ObjectGetResponse: MessageObject, IResponse
	{
		public static ObjectGetResponse Create(bool isFromPool = true) 
		{ 
			return !isFromPool? new ObjectGetResponse() : ObjectPool.Instance.Fetch(typeof(ObjectGetResponse)) as ObjectGetResponse; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		[MemoryPackOrder(3)]
		public int Type { get; set; }

		[MemoryPackOrder(4)]
		public ActorId ActorId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			this.Type = default;
			this.ActorId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[ResponseType(nameof(G2R_GetLoginKey))]
	[Message(InnerMessage.R2G_GetLoginKey)]
	[MemoryPackable]
	public partial class R2G_GetLoginKey: MessageObject, IRequest
	{
		public static R2G_GetLoginKey Create(bool isFromPool = true) 
		{ 
			return !isFromPool? new R2G_GetLoginKey() : ObjectPool.Instance.Fetch(typeof(R2G_GetLoginKey)) as R2G_GetLoginKey; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(1)]
		public string Account { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Account = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(InnerMessage.G2R_GetLoginKey)]
	[MemoryPackable]
	public partial class G2R_GetLoginKey: MessageObject, IResponse
	{
		public static G2R_GetLoginKey Create(bool isFromPool = true) 
		{ 
			return !isFromPool? new G2R_GetLoginKey() : ObjectPool.Instance.Fetch(typeof(G2R_GetLoginKey)) as G2R_GetLoginKey; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		[MemoryPackOrder(3)]
		public long Key { get; set; }

		[MemoryPackOrder(4)]
		public long GateId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			this.Key = default;
			this.GateId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(InnerMessage.G2M_SessionDisconnect)]
	[MemoryPackable]
	public partial class G2M_SessionDisconnect: MessageObject, ILocationMessage
	{
		public static G2M_SessionDisconnect Create(bool isFromPool = true) 
		{ 
			return !isFromPool? new G2M_SessionDisconnect() : ObjectPool.Instance.Fetch(typeof(G2M_SessionDisconnect)) as G2M_SessionDisconnect; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[ResponseType(nameof(M2G_RequestEnterGameState))]
	[Message(InnerMessage.G2M_RequestEnterGameState)]
	[MemoryPackable]
	public partial class G2M_RequestEnterGameState: MessageObject, ILocationRequest
	{
		public static G2M_RequestEnterGameState Create(bool isFromPool = true) 
		{ 
			return !isFromPool? new G2M_RequestEnterGameState() : ObjectPool.Instance.Fetch(typeof(G2M_RequestEnterGameState)) as G2M_RequestEnterGameState; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(InnerMessage.M2G_RequestEnterGameState)]
	[MemoryPackable]
	public partial class M2G_RequestEnterGameState: MessageObject, ILocationResponse
	{
		public static M2G_RequestEnterGameState Create(bool isFromPool = true) 
		{ 
			return !isFromPool? new M2G_RequestEnterGameState() : ObjectPool.Instance.Fetch(typeof(M2G_RequestEnterGameState)) as M2G_RequestEnterGameState; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(InnerMessage.ObjectQueryResponse)]
	[MemoryPackable]
	public partial class ObjectQueryResponse: MessageObject, IResponse
	{
		public static ObjectQueryResponse Create(bool isFromPool = true) 
		{ 
			return !isFromPool? new ObjectQueryResponse() : ObjectPool.Instance.Fetch(typeof(ObjectQueryResponse)) as ObjectQueryResponse; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		[MemoryPackOrder(3)]
		public byte[] Entity { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			this.Entity = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[ResponseType(nameof(M2M_UnitTransferResponse))]
	[Message(InnerMessage.M2M_UnitTransferRequest)]
	[MemoryPackable]
	public partial class M2M_UnitTransferRequest: MessageObject, IRequest
	{
		public static M2M_UnitTransferRequest Create(bool isFromPool = true) 
		{ 
			return !isFromPool? new M2M_UnitTransferRequest() : ObjectPool.Instance.Fetch(typeof(M2M_UnitTransferRequest)) as M2M_UnitTransferRequest; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(1)]
		public ActorId OldActorId { get; set; }

		[MemoryPackOrder(2)]
		public byte[] Unit { get; set; }

		[MemoryPackOrder(3)]
		public List<byte[]> Entitys { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.OldActorId = default;
			this.Unit = default;
			this.Entitys.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(InnerMessage.M2M_UnitTransferResponse)]
	[MemoryPackable]
	public partial class M2M_UnitTransferResponse: MessageObject, IResponse
	{
		public static M2M_UnitTransferResponse Create(bool isFromPool = true) 
		{ 
			return !isFromPool? new M2M_UnitTransferResponse() : ObjectPool.Instance.Fetch(typeof(M2M_UnitTransferResponse)) as M2M_UnitTransferResponse; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	/// <summary>
	///添加或更新缓存数据
	/// </summary>
	[ResponseType(nameof(Cache2Other_UpdateCache))]
	[Message(InnerMessage.Other2Cache_UpdateCache)]
	[MemoryPackable]
	public partial class Other2Cache_UpdateCache: MessageObject, IRequest
	{
		public static Other2Cache_UpdateCache Create(bool isFromPool = true) 
		{ 
			return !isFromPool? new Other2Cache_UpdateCache() : ObjectPool.Instance.Fetch(typeof(Other2Cache_UpdateCache)) as Other2Cache_UpdateCache; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(0)]
		public long UnitId { get; set; }

	/// <summary>
	///实体列表
	/// </summary>
		[MemoryPackOrder(1)]
		public List<string> EntityTypeList { get; set; } = new();

		[MemoryPackOrder(2)]
		public List<byte[]> EntityData { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.UnitId = default;
			this.EntityTypeList.Clear();
			this.EntityData.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(InnerMessage.Cache2Other_UpdateCache)]
	[MemoryPackable]
	public partial class Cache2Other_UpdateCache: MessageObject, IResponse
	{
		public static Cache2Other_UpdateCache Create(bool isFromPool = true) 
		{ 
			return !isFromPool? new Cache2Other_UpdateCache() : ObjectPool.Instance.Fetch(typeof(Cache2Other_UpdateCache)) as Cache2Other_UpdateCache; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	/// <summary>
	///获取缓存数据
	/// </summary>
	[ResponseType(nameof(Cache2Other_GetCache))]
	[Message(InnerMessage.Other2Cache_GetCache)]
	[MemoryPackable]
	public partial class Other2Cache_GetCache: MessageObject, IRequest
	{
		public static Other2Cache_GetCache Create(bool isFromPool = true) 
		{ 
			return !isFromPool? new Other2Cache_GetCache() : ObjectPool.Instance.Fetch(typeof(Other2Cache_GetCache)) as Other2Cache_GetCache; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(0)]
		public long UnitId { get; set; }

		[MemoryPackOrder(1)]
		public List<string> ComponentNameList { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.UnitId = default;
			this.ComponentNameList.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(InnerMessage.Cache2Other_GetCache)]
	[MemoryPackable]
	public partial class Cache2Other_GetCache: MessageObject, IResponse
	{
		public static Cache2Other_GetCache Create(bool isFromPool = true) 
		{ 
			return !isFromPool? new Cache2Other_GetCache() : ObjectPool.Instance.Fetch(typeof(Cache2Other_GetCache)) as Cache2Other_GetCache; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		[MemoryPackOrder(0)]
		public List<string> ComponentNameList { get; set; } = new();

		[MemoryPackOrder(1)]
		public List<byte[]> Entitys { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			this.ComponentNameList.Clear();
			this.Entitys.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	/// <summary>
	///删除缓存数据
	/// </summary>
	[ResponseType(nameof(Cache2Other_DeleteCache))]
	[Message(InnerMessage.Other2Cache_DeleteCache)]
	[MemoryPackable]
	public partial class Other2Cache_DeleteCache: MessageObject, IRequest
	{
		public static Other2Cache_DeleteCache Create(bool isFromPool = true) 
		{ 
			return !isFromPool? new Other2Cache_DeleteCache() : ObjectPool.Instance.Fetch(typeof(Other2Cache_DeleteCache)) as Other2Cache_DeleteCache; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(0)]
		public long UnitId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.UnitId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(InnerMessage.Cache2Other_DeleteCache)]
	[MemoryPackable]
	public partial class Cache2Other_DeleteCache: MessageObject, IResponse
	{
		public static Cache2Other_DeleteCache Create(bool isFromPool = true) 
		{ 
			return !isFromPool? new Cache2Other_DeleteCache() : ObjectPool.Instance.Fetch(typeof(Cache2Other_DeleteCache)) as Cache2Other_DeleteCache; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	/// <summary>
	///玩家进入游戏
	/// </summary>
	[ResponseType(nameof(Other2G_EnterResponse))]
	[Message(InnerMessage.G2Other_EnterRequest)]
	[MemoryPackable]
	public partial class G2Other_EnterRequest: MessageObject, IRequest
	{
		public static G2Other_EnterRequest Create(bool isFromPool = true) 
		{ 
			return !isFromPool? new G2Other_EnterRequest() : ObjectPool.Instance.Fetch(typeof(G2Other_EnterRequest)) as G2Other_EnterRequest; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(0)]
		public long PlayerId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.PlayerId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(InnerMessage.Other2G_EnterResponse)]
	[MemoryPackable]
	public partial class Other2G_EnterResponse: MessageObject, IResponse
	{
		public static Other2G_EnterResponse Create(bool isFromPool = true) 
		{ 
			return !isFromPool? new Other2G_EnterResponse() : ObjectPool.Instance.Fetch(typeof(Other2G_EnterResponse)) as Other2G_EnterResponse; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		[MemoryPackOrder(0)]
		public long Id { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			this.Id = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	/// <summary>
	///玩家退出游戏
	/// </summary>
	[ResponseType(nameof(Other2G_LeaveResponse))]
	[Message(InnerMessage.G2Other_LeaveRequest)]
	[MemoryPackable]
	public partial class G2Other_LeaveRequest: MessageObject, IRequest
	{
		public static G2Other_LeaveRequest Create(bool isFromPool = true) 
		{ 
			return !isFromPool? new G2Other_LeaveRequest() : ObjectPool.Instance.Fetch(typeof(G2Other_LeaveRequest)) as G2Other_LeaveRequest; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(0)]
		public long PlayerId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.PlayerId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(InnerMessage.Other2G_LeaveResponse)]
	[MemoryPackable]
	public partial class Other2G_LeaveResponse: MessageObject, IResponse
	{
		public static Other2G_LeaveResponse Create(bool isFromPool = true) 
		{ 
			return !isFromPool? new Other2G_LeaveResponse() : ObjectPool.Instance.Fetch(typeof(Other2G_LeaveResponse)) as Other2G_LeaveResponse; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	/// <summary>
	///守护进程请求保存数据
	/// </summary>
	[ResponseType(nameof(Other2W_SaveDataResponse))]
	[Message(InnerMessage.W2Other_SaveDataRequest)]
	[MemoryPackable]
	public partial class W2Other_SaveDataRequest: MessageObject, IRequest
	{
		public static W2Other_SaveDataRequest Create(bool isFromPool = true) 
		{ 
			return !isFromPool? new W2Other_SaveDataRequest() : ObjectPool.Instance.Fetch(typeof(W2Other_SaveDataRequest)) as W2Other_SaveDataRequest; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(InnerMessage.Other2W_SaveDataResponse)]
	[MemoryPackable]
	public partial class Other2W_SaveDataResponse: MessageObject, IResponse
	{
		public static Other2W_SaveDataResponse Create(bool isFromPool = true) 
		{ 
			return !isFromPool? new Other2W_SaveDataResponse() : ObjectPool.Instance.Fetch(typeof(Other2W_SaveDataResponse)) as Other2W_SaveDataResponse; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	/// <summary>
	///守护进程关闭其他进程
	/// </summary>
	[ResponseType(nameof(Other2W_SaveDataResponse))]
	[Message(InnerMessage.W2Other_CloseRequest)]
	[MemoryPackable]
	public partial class W2Other_CloseRequest: MessageObject, IRequest
	{
		public static W2Other_CloseRequest Create(bool isFromPool = true) 
		{ 
			return !isFromPool? new W2Other_CloseRequest() : ObjectPool.Instance.Fetch(typeof(W2Other_CloseRequest)) as W2Other_CloseRequest; 
		}

	/// <summary>
	///要关闭的进程ID, 小于0时全部关闭
	/// </summary>
		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(0)]
		public int ProcessId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.ProcessId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(InnerMessage.W2Other_CloseResponse)]
	[MemoryPackable]
	public partial class W2Other_CloseResponse: MessageObject, IResponse
	{
		public static W2Other_CloseResponse Create(bool isFromPool = true) 
		{ 
			return !isFromPool? new W2Other_CloseResponse() : ObjectPool.Instance.Fetch(typeof(W2Other_CloseResponse)) as W2Other_CloseResponse; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	/// <summary>
	///守护进程开启其他进程
	/// </summary>
	[ResponseType(nameof(Other2W_SaveDataResponse))]
	[Message(InnerMessage.W2Other_OpenRequest)]
	[MemoryPackable]
	public partial class W2Other_OpenRequest: MessageObject, IRequest
	{
		public static W2Other_OpenRequest Create(bool isFromPool = true) 
		{ 
			return !isFromPool? new W2Other_OpenRequest() : ObjectPool.Instance.Fetch(typeof(W2Other_OpenRequest)) as W2Other_OpenRequest; 
		}

	/// <summary>
	///要关闭的进程ID, 小于0时全部开启
	/// </summary>
		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(0)]
		public int ProcessId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.ProcessId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(InnerMessage.W2Other_OpenResponse)]
	[MemoryPackable]
	public partial class W2Other_OpenResponse: MessageObject, IResponse
	{
		public static W2Other_OpenResponse Create(bool isFromPool = true) 
		{ 
			return !isFromPool? new W2Other_OpenResponse() : ObjectPool.Instance.Fetch(typeof(W2Other_OpenResponse)) as W2Other_OpenResponse; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	public static class InnerMessage
	{
		 public const ushort InnerPingRequest = 20002;
		 public const ushort InnerPingResponse = 20003;
		 public const ushort ObjectQueryRequest = 20004;
		 public const ushort M2A_Reload = 20005;
		 public const ushort A2M_Reload = 20006;
		 public const ushort G2G_LockRequest = 20007;
		 public const ushort G2G_LockResponse = 20008;
		 public const ushort G2G_LockReleaseRequest = 20009;
		 public const ushort G2G_LockReleaseResponse = 20010;
		 public const ushort ObjectAddRequest = 20011;
		 public const ushort ObjectAddResponse = 20012;
		 public const ushort ObjectLockRequest = 20013;
		 public const ushort ObjectLockResponse = 20014;
		 public const ushort ObjectUnLockRequest = 20015;
		 public const ushort ObjectUnLockResponse = 20016;
		 public const ushort ObjectRemoveRequest = 20017;
		 public const ushort ObjectRemoveResponse = 20018;
		 public const ushort ObjectGetRequest = 20019;
		 public const ushort ObjectGetResponse = 20020;
		 public const ushort R2G_GetLoginKey = 20021;
		 public const ushort G2R_GetLoginKey = 20022;
		 public const ushort G2M_SessionDisconnect = 20023;
		 public const ushort G2M_RequestEnterGameState = 20024;
		 public const ushort M2G_RequestEnterGameState = 20025;
		 public const ushort ObjectQueryResponse = 20026;
		 public const ushort M2M_UnitTransferRequest = 20027;
		 public const ushort M2M_UnitTransferResponse = 20028;
		 public const ushort Other2Cache_UpdateCache = 20029;
		 public const ushort Cache2Other_UpdateCache = 20030;
		 public const ushort Other2Cache_GetCache = 20031;
		 public const ushort Cache2Other_GetCache = 20032;
		 public const ushort Other2Cache_DeleteCache = 20033;
		 public const ushort Cache2Other_DeleteCache = 20034;
		 public const ushort G2Other_EnterRequest = 20035;
		 public const ushort Other2G_EnterResponse = 20036;
		 public const ushort G2Other_LeaveRequest = 20037;
		 public const ushort Other2G_LeaveResponse = 20038;
		 public const ushort W2Other_SaveDataRequest = 20039;
		 public const ushort Other2W_SaveDataResponse = 20040;
		 public const ushort W2Other_CloseRequest = 20041;
		 public const ushort W2Other_CloseResponse = 20042;
		 public const ushort W2Other_OpenRequest = 20043;
		 public const ushort W2Other_OpenResponse = 20044;
	}
}
