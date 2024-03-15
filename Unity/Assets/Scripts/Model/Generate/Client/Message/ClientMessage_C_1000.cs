using ET;
using MemoryPack;
using System.Collections.Generic;
namespace ET
{
	/// <summary>
	/// using
	/// </summary>
	[ResponseType(nameof(NetClient2Main_Login))]
	[Message(ClientMessage.Main2NetClient_Login)]
	[MemoryPackable]
	public partial class Main2NetClient_Login: MessageObject, IRequest
	{
		public static Main2NetClient_Login Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Main2NetClient_Login), isFromPool) as Main2NetClient_Login; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(1)]
		public int OwnerFiberId { get; set; }

		[MemoryPackOrder(2)]
		public string Account { get; set; }

		[MemoryPackOrder(3)]
		public string Password { get; set; }

		[MemoryPackOrder(4)]
		public long Id { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.OwnerFiberId = default;
			this.Account = default;
			this.Password = default;
			this.Id = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(ClientMessage.NetClient2Main_Login)]
	[MemoryPackable]
	public partial class NetClient2Main_Login: MessageObject, IResponse
	{
		public static NetClient2Main_Login Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(NetClient2Main_Login), isFromPool) as NetClient2Main_Login; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		[MemoryPackOrder(3)]
		public long PlayerId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			this.PlayerId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[ResponseType(nameof(NetClient2Main_LoginGame))]
	[Message(ClientMessage.Main2NetClient_LoginGame)]
	[MemoryPackable]
	public partial class Main2NetClient_LoginGame: MessageObject, IRequest
	{
		public static Main2NetClient_LoginGame Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(Main2NetClient_LoginGame), isFromPool) as Main2NetClient_LoginGame; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(1)]
		public int OwnerFiberId { get; set; }

		[MemoryPackOrder(2)]
		public string Account { get; set; }

		[MemoryPackOrder(3)]
		public string Password { get; set; }

		[MemoryPackOrder(4)]
		public long Id { get; set; }

		[MemoryPackOrder(5)]
		public long RoleId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.OwnerFiberId = default;
			this.Account = default;
			this.Password = default;
			this.Id = default;
			this.RoleId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	[Message(ClientMessage.NetClient2Main_LoginGame)]
	[MemoryPackable]
	public partial class NetClient2Main_LoginGame: MessageObject, IResponse
	{
		public static NetClient2Main_LoginGame Create(bool isFromPool = false) 
		{ 
			return ObjectPool.Instance.Fetch(typeof(NetClient2Main_LoginGame), isFromPool) as NetClient2Main_LoginGame; 
		}

		[MemoryPackOrder(89)]
		public int RpcId { get; set; }

		[MemoryPackOrder(90)]
		public int Error { get; set; }

		[MemoryPackOrder(91)]
		public List<string> Message { get; set; } = new();

		[MemoryPackOrder(3)]
		public long PlayerId { get; set; }

		public override void Dispose() 
		{
			if (!this.IsFromPool) return;
			this.RpcId = default;
			this.Error = default;
			this.Message.Clear();
			this.PlayerId = default;
			
			ObjectPool.Instance.Recycle(this); 
		}

	}

	public static class ClientMessage
	{
		 public const ushort Main2NetClient_Login = 1001;
		 public const ushort NetClient2Main_Login = 1002;
		 public const ushort Main2NetClient_LoginGame = 1003;
		 public const ushort NetClient2Main_LoginGame = 1004;
	}
}
